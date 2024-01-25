// <copyright file="GoogleGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Services
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Core.Models.Exceptions;
    using Geo.Google.Enums;
    using Geo.Google.Models;
    using Geo.Google.Models.Parameters;
    using Geo.Google.Models.Responses;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the Google geocoding API.
    /// </summary>
    public class GoogleGeocoding : GeoClient, IGoogleGeocoding
    {
        private const string GeocodingUri = "https://maps.googleapis.com/maps/api/geocode/json";
        private const string FindPlaceUri = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json";
        private const string NearbySearchUri = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";
        private const string TextSearchUri = "https://maps.googleapis.com/maps/api/place/textsearch/json";
        private const string DetailsUri = "https://maps.googleapis.com/maps/api/place/details/json";
        private const string PlaceAutocompleteUri = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
        private const string QueryAutocompleteUri = "https://maps.googleapis.com/maps/api/place/queryautocomplete/json";

        private readonly IOptions<KeyOptions<IGoogleGeocoding>> _options;
        private readonly ILogger<GoogleGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Google Geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="KeyOptions{T}"/> containing Google key information.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public GoogleGeocoding(
            HttpClient client,
            IOptions<KeyOptions<IGoogleGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<GoogleGeocoding>() ?? NullLogger<GoogleGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "Google";

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await GetAsync<GeocodingResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await GetAsync<GeocodingResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<FindPlaceResponse> FindPlacesAsync(
            FindPlacesParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<FindPlacesParameters>(parameters, BuildFindPlaceRequest);

            return await GetAsync<FindPlaceResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<PlaceResponse> NearbySearchAsync(
            NearbySearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<NearbySearchParameters>(parameters, BuildNearbySearchRequest);

            return await GetAsync<PlaceResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<PlaceResponse> TextSearchAsync(
            TextSearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<TextSearchParameters>(parameters, BuildTextSearchRequest);

            return await GetAsync<PlaceResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DetailsResponse> DetailsAsync(
            DetailsParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<DetailsParameters>(parameters, BuildDetailsRequest);

            return await GetAsync<DetailsResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<AutocompleteResponse<PlaceAutocomplete>> PlaceAutocompleteAsync(
            PlacesAutocompleteParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<PlacesAutocompleteParameters>(parameters, BuildPlaceAutocompleteRequest);

            return await GetAsync<AutocompleteResponse<PlaceAutocomplete>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<AutocompleteResponse<QueryAutocomplete>> QueryAutocompleteAsync(
            QueryAutocompleteParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<QueryAutocompleteParameters>(parameters, BuildQueryAutocompleteRequest);

            return await GetAsync<AutocompleteResponse<QueryAutocomplete>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the ccTLD representation of a <see cref="RegionInfo"/> object.
        /// </summary>
        /// <param name="regionInfo">A <see cref="RegionInfo"/> with the region information to convert.</param>
        /// <returns>A <see cref="string"/> with the ccTLD.</returns>
        internal static string RegionInfoToCCTLD(RegionInfo regionInfo)
        {
            if (regionInfo.GeoId == new RegionInfo("GB").GeoId)
            {
                return "uk";
            }
            else
            {
#pragma warning disable CA1308 // Normalize strings to uppercase
                return regionInfo.TwoLetterISORegionName.ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase
            }
        }

        /// <summary>
        /// Validates the uri and builds it based on the parameter type.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="parameters">The parameters to validate and create a uri from.</param>
        /// <param name="uriBuilderFunction">The method to use to create the uri.</param>
        /// <returns>A <see cref="Uri"/> with the uri crafted from the parameters.</returns>
        internal Uri ValidateAndBuildUri<TParameters>(TParameters parameters, Func<TParameters, Uri> uriBuilderFunction)
            where TParameters : class
        {
            if (parameters is null)
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.GoogleGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.GoogleGeocoding.Failed_To_Create_Uri, ex);
            }
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Address' parameter is null or invalid.</exception>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(GeocodingUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Address))
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_Address);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_Address, nameof(parameters.Address));
            }

            query = query.Add("address", parameters.Address);

            if (parameters.Components != null)
            {
                query = query.Add("components", parameters.Components.ToString());
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Components);
            }

            if (parameters.Bounds != null &&
                parameters.Bounds.Southwest != null &&
                parameters.Bounds.Northeast != null)
            {
                query = query.Add("bounds", parameters.Bounds.ToString());
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Bounds);
            }

            if (parameters.Region != null)
            {
                query = query.Add("region", RegionInfoToCCTLD(parameters.Region));
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Region);
            }

            AddBaseParameters(parameters, ref query);

            AddGoogleKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Coordinate' parameter is null or invalid.</exception>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(GeocodingUri);
            var query = QueryString.Empty;

            if (parameters.Coordinate is null)
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_Coordinates);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_Coordinates, nameof(parameters.Coordinate));
            }

            query = query.Add("latlng", parameters.Coordinate.ToString());

            if (parameters.ResultTypes != null)
            {
                var resultTypesBuilder = new StringBuilder();
                foreach (var resultType in parameters.ResultTypes)
                {
                    if (resultTypesBuilder.Length > 0)
                    {
                        resultTypesBuilder.Append('|');
                    }

                    resultTypesBuilder.Append(resultType.ToEnumString<ResultType>());
                }

                query = query.Add("result_type", resultTypesBuilder.ToString());
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Result_Types);
            }

            if (parameters.LocationTypes != null)
            {
                var locationTypesBuilder = new StringBuilder();
                foreach (var locationType in parameters.LocationTypes)
                {
                    if (locationTypesBuilder.Length > 0)
                    {
                        locationTypesBuilder.Append('|');
                    }

                    locationTypesBuilder.Append(locationType.ToEnumString<LocationType>());
                }

                query = query.Add("location_type", locationTypesBuilder.ToString());
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Location_Types);
            }

            AddBaseParameters(parameters, ref query);

            AddGoogleKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the find place uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="FindPlacesParameters"/> with the find place parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google find place uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Input' parameter is null or invalid or the 'InputType' parameter is invalid.</exception>
        internal Uri BuildFindPlaceRequest(FindPlacesParameters parameters)
        {
            var uriBuilder = new UriBuilder(FindPlaceUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Input))
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_Input);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_Input, nameof(parameters.Input));
            }

            if (parameters.InputType > InputType.PhoneNumber || parameters.InputType < InputType.TextQuery)
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_Input_Type);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_Input_Type, nameof(parameters.InputType));
            }

            query = query.Add("input", parameters.Input);

            query = query.Add("inputtype", parameters.InputType.ToEnumString());

            if (parameters.Fields != null && parameters.Fields.Count > 0)
            {
                query = query.Add("fields", string.Join(",", parameters.Fields));
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Fields);
            }

            if (parameters.LocationBias != null)
            {
                switch (parameters.LocationBias)
                {
                    case Coordinate coordinate:
                        query = query.Add("locationbias", $"point:{coordinate}");
                        break;
                    case Circle circle:
                        query = query.Add("locationbias", $"circle:{circle}");
                        break;
                    case Boundaries boundary:
                        query = query.Add("locationbias", $"rectangle:{boundary}");
                        break;
                    default:
                        _logger.GoogleWarning(Resources.Services.GoogleGeocoding.Invalid_Location_Bias_Type);
                        break;
                }
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Location_Bias);
            }

            AddBaseParameters(parameters, ref query);

            AddGoogleKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the nearby search uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="NearbySearchParameters"/> with the nearby search parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google nearby search uri.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the 'Location' parameter is null.
        /// Thrown when the 'RankBy' is Distance and a 'Radius' is entered or a 'Keyword' or 'Type' is not entered.
        /// Thrown when the 'RankBy' is not Distance and the 'Radius' is not > 0.
        /// </exception>
        internal Uri BuildNearbySearchRequest(NearbySearchParameters parameters)
        {
            var uriBuilder = new UriBuilder(NearbySearchUri);
            var query = QueryString.Empty;

            if (parameters.Location == null)
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_Location);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_Location, nameof(parameters.Location));
            }

            if (parameters.RankBy == RankType.Distance)
            {
                if (parameters.Radius > 0)
                {
                    _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_RankBy_Distance_Radius);
                    throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_RankBy_Distance_Radius, nameof(parameters.Radius));
                }

                if (string.IsNullOrWhiteSpace(parameters.Keyword) && string.IsNullOrWhiteSpace(parameters.Type))
                {
                    _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_RankBy_Distance_Request);
                    throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_RankBy_Distance_Request, nameof(parameters.RankBy));
                }
            }
            else if (parameters.Radius <= 0)
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_Radius);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_Radius, nameof(parameters.Radius));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Keyword))
            {
                query = query.Add("keyword", parameters.Keyword);
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Keyword);
            }

            if (parameters.RankBy >= RankType.Prominence && parameters.RankBy <= RankType.Distance)
            {
                query = query.Add("rankby", parameters.RankBy.ToEnumString());
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_RankBy);
            }

            AddBaseSearchParameters(parameters, ref query);

            AddGoogleKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the text search uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="TextSearchParameters"/> with the text search parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google text search uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Query' parameter is null or invalid.</exception>
        internal Uri BuildTextSearchRequest(TextSearchParameters parameters)
        {
            var uriBuilder = new UriBuilder(TextSearchUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_Query);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_Query, nameof(parameters.Query));
            }

            query = query.Add("query", parameters.Query);

            if (parameters.Region != null)
            {
                query = query.Add("region", RegionInfoToCCTLD(parameters.Region));
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Region);
            }

            AddBaseSearchParameters(parameters, ref query);

            AddGoogleKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the details uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="DetailsParameters"/> with the details parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google details uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'PlaceId' parameter is null or invalid.</exception>
        internal Uri BuildDetailsRequest(DetailsParameters parameters)
        {
            var uriBuilder = new UriBuilder(DetailsUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.PlaceId))
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_PlaceId);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_PlaceId, nameof(parameters.PlaceId));
            }

            query = query.Add("place_id", parameters.PlaceId);

            if (parameters.Region != null)
            {
                query = query.Add("region", RegionInfoToCCTLD(parameters.Region));
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Region);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SessionToken))
            {
                query = query.Add("sessiontoken", parameters.SessionToken);
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Session_Token);
            }

            if (parameters.Fields != null && parameters.Fields.Count > 0)
            {
                query = query.Add("fields", string.Join(",", parameters.Fields));
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Fields);
            }

            AddBaseParameters(parameters, ref query);

            AddGoogleKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the place autocomplete uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="PlacesAutocompleteParameters"/> with the place autocomplete parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google place autocomplete uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Input' parameter is null or invalid.</exception>
        internal Uri BuildPlaceAutocompleteRequest(PlacesAutocompleteParameters parameters)
        {
            var uriBuilder = new UriBuilder(PlaceAutocompleteUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Input))
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_Input);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_Input, nameof(parameters.Input));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SessionToken))
            {
                query = query.Add("sessiontoken", parameters.SessionToken);
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Session_Token);
            }

            if (parameters.Origin != null)
            {
                query = query.Add("origin", parameters.Origin.ToString());
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Origin);
            }

            if (parameters.Types != null && parameters.Types.Count > 0)
            {
                query = query.Add("types", string.Join(",", parameters.Types.Select(x => x.ToEnumString())));
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Types);
            }

            if (parameters.Components != null)
            {
                query = query.Add("components", parameters.Components.ToString());
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Components);
            }

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("strictbounds", parameters.StrictBounds.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            AddAutocompleteParameters(parameters, ref query);

            AddGoogleKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the query autocomplete uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="QueryAutocompleteParameters"/> with the query autocomplete parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google query autocomplete uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Input' parameter is null or invalid.</exception>
        internal Uri BuildQueryAutocompleteRequest(QueryAutocompleteParameters parameters)
        {
            var uriBuilder = new UriBuilder(QueryAutocompleteUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Input))
            {
                _logger.GoogleError(Resources.Services.GoogleGeocoding.Invalid_Input);
                throw new ArgumentException(Resources.Services.GoogleGeocoding.Invalid_Input, nameof(parameters.Input));
            }

            AddAutocompleteParameters(parameters, ref query);

            AddGoogleKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the autocomplete parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="QueryAutocompleteParameters"/> with the autocomplete parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddAutocompleteParameters(QueryAutocompleteParameters parameters, ref QueryString query)
        {
            if (parameters.Offset > 0)
            {
                query = query.Add("offset", parameters.Offset.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.GoogleWarning(Resources.Services.GoogleGeocoding.Invalid_Offset);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Input))
            {
                query = query.Add("input", parameters.Input);
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Input_Info);
            }

            AddCoordinateParameters(parameters, ref query);
        }

        /// <summary>
        /// Adds the base search parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseSearchParameters"/> with the base search parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddBaseSearchParameters(BaseSearchParameters parameters, ref QueryString query)
        {
            if (parameters.MinimumPrice >= 0 && parameters.MinimumPrice <= 4 && parameters.MinimumPrice <= parameters.MaximumPrice)
            {
                query = query.Add("minprice", parameters.MinimumPrice.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.GoogleWarning(Resources.Services.GoogleGeocoding.Invalid_Minimum_Price);
            }

            if (parameters.MaximumPrice >= 0 && parameters.MaximumPrice <= 4 && parameters.MinimumPrice <= parameters.MaximumPrice)
            {
                query = query.Add("maxprice", parameters.MaximumPrice.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.GoogleWarning(Resources.Services.GoogleGeocoding.Invalid_Maximum_Price);
            }

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("opennow", parameters.OpenNow.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (parameters.PageToken > 0)
            {
                query = query.Add("pagetoken", parameters.PageToken.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.GoogleWarning(Resources.Services.GoogleGeocoding.Invalid_Page_Token);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Type))
            {
                query = query.Add("type", parameters.Type);
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Type);
            }

            AddCoordinateParameters(parameters, ref query);
        }

        /// <summary>
        /// Adds the coordinate parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="CoordinateParameters"/> with the coordinate parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddCoordinateParameters(CoordinateParameters parameters, ref QueryString query)
        {
            if (parameters.Location != null)
            {
                query = query.Add("location", parameters.Location.ToString());
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Location);
            }

            if (parameters.Radius > 0 && parameters.Radius <= 50000)
            {
                query = query.Add("radius", parameters.Radius.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.GoogleWarning(Resources.Services.GoogleGeocoding.Invalid_Radius_Value);
            }

            AddBaseParameters(parameters, ref query);
        }

        /// <summary>
        /// Adds the base query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddBaseParameters(BaseParameters parameters, ref QueryString query)
        {
            if (parameters.Language != null && !string.IsNullOrWhiteSpace(parameters.Language.Name))
            {
                query = query.Add("language", parameters.Language.Name);
            }
            else
            {
                _logger.GoogleDebug(Resources.Services.GoogleGeocoding.Invalid_Language);
            }
        }

        /// <summary>
        /// Adds the Google key to the query parameters.
        /// </summary>/// <param name="keyParameter">An <see cref="IKeyParameters"/> to conditionally get the key from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddGoogleKey(IKeyParameters keyParameter, ref QueryString query)
        {
            var key = _options.Value.Key;

            if (!string.IsNullOrWhiteSpace(keyParameter.Key))
            {
                key = keyParameter.Key;
            }

            query = query.Add("key", key);
        }
    }
}
