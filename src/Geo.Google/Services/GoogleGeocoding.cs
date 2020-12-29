// <copyright file="GoogleGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Services
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Google.Abstractions;
    using Geo.Google.Enums;
    using Geo.Google.Models;
    using Geo.Google.Models.Exceptions;
    using Geo.Google.Models.Parameters;
    using Geo.Google.Models.Responses;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// A service to call the Google geocoding API.
    /// </summary>
    public class GoogleGeocoding : ClientExecutor, IGoogleGeocoding
    {
        private const string _apiName = "Google";
        private readonly string _geocodingUri = "https://maps.googleapis.com/maps/api/geocode/json";
        private readonly string _findPlaceUri = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json";
        private readonly string _nearbySearchUri = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";
        private readonly string _textSearchUri = "https://maps.googleapis.com/maps/api/place/textsearch/json";
        private readonly string _detailsUri = "https://maps.googleapis.com/maps/api/place/details/json";
        private readonly string _placeAutocompleteUri = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
        private readonly string _queryAutocompleteUri = "https://maps.googleapis.com/maps/api/place/queryautocomplete/json";
        private readonly IGoogleKeyContainer _keyContainer;
        private readonly IStringLocalizer<GoogleGeocoding> _localizer;
        private readonly ILogger<GoogleGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Google Geocoding API.</param>
        /// <param name="keyContainer">A <see cref="IGoogleKeyContainer"/> used for fetching the Google key.</param>
        /// <param name="localizer">A <see cref="IStringLocalizer{T}"/> used for localizing log or exception messages.</param>
        /// <param name="coreLocalizer">A <see cref="IStringLocalizer{T}"/> used for localizing core log or exception messages.</param>
        /// <param name="logger">A <see cref="ILogger{T}"/> used for logging information.</param>
        public GoogleGeocoding(
            HttpClient client,
            IGoogleKeyContainer keyContainer,
            IStringLocalizer<GoogleGeocoding> localizer,
            IStringLocalizer<ClientExecutor> coreLocalizer,
            ILogger<GoogleGeocoding> logger = null)
            : base(client, coreLocalizer)
        {
            _keyContainer = keyContainer;
            _localizer = localizer;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await CallAsync<GeocodingResponse, GoogleException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await CallAsync<GeocodingResponse, GoogleException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<FindPlaceResponse> FindPlacesAsync(
            FindPlacesParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<FindPlacesParameters>(parameters, BuildFindPlaceRequest);

            return await CallAsync<FindPlaceResponse, GoogleException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<PlaceResponse> NearbySearchAsync(
            NearbySearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<NearbySearchParameters>(parameters, BuildNearbySearchRequest);

            return await CallAsync<PlaceResponse, GoogleException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<PlaceResponse> TextSearchAsync(
            TextSearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<TextSearchParameters>(parameters, BuildTextSearchRequest);

            return await CallAsync<PlaceResponse, GoogleException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DetailsResponse> DetailsAsync(
            DetailsParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<DetailsParameters>(parameters, BuildDetailsRequest);

            return await CallAsync<DetailsResponse, GoogleException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<AutocompleteResponse<PlaceAutocomplete>> PlaceAutocompleteAsync(
            PlacesAutocompleteParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<PlacesAutocompleteParameters>(parameters, BuildPlaceAutocompleteRequest);

            return await CallAsync<AutocompleteResponse<PlaceAutocomplete>, GoogleException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<AutocompleteResponse<QueryAutocomplete>> QueryAutocompleteAsync(
            QueryAutocompleteParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<QueryAutocompleteParameters>(parameters, BuildQueryAutocompleteRequest);

            return await CallAsync<AutocompleteResponse<QueryAutocomplete>, GoogleException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Validates the uri and builds it based on the parameter type.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="parameters">The parameters to validate and create a uri from.</param>
        /// <param name="uriBuilderFunction">The method to use to create the uri.</param>
        /// <returns>A <see cref="Uri"/> with the uri crafted from the parameters.</returns>
        internal Uri ValidateAndBuildUri<TParameters>(TParameters parameters, Func<TParameters, Uri> uriBuilderFunction)
        {
            if (parameters is null)
            {
                var error = _localizer["Null Parameters"];
                _logger?.LogError(error);
                throw new GoogleException(error, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                var error = _localizer["Failed To Create Uri"];
                _logger?.LogError(error);
                throw new GoogleException(error, ex);
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
            var uriBuilder = new UriBuilder(_geocodingUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Address))
            {
                var error = _localizer["Invalid Address"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.Address));
            }

            query.Add("address", parameters.Address);

            if (parameters.Components != null)
            {
                query.Add("components", parameters.Components);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Components"]);
            }

            if (parameters.Bounds != null &&
                parameters.Bounds.Southwest != null &&
                parameters.Bounds.Northeast != null)
            {
                query.Add("bounds", parameters.Bounds.ToString());
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Bounds"]);
            }

            if (parameters.Region != null)
            {
                query.Add("region", parameters.Region);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Region"]);
            }

            AddBaseParameters(parameters, query);

            AddGoogleKey(query);

            uriBuilder.Query = query.ToString();

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
            var uriBuilder = new UriBuilder(_geocodingUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parameters.Coordinate is null)
            {
                var error = _localizer["Invalid Coordinates"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.Coordinate));
            }

            query.Add("latlng", parameters.Coordinate.ToString());

            if (parameters.ResultTypes != null)
            {
                var resultTypesBuilder = new StringBuilder();
                foreach (var resultType in parameters.ResultTypes)
                {
                    if (resultTypesBuilder.Length > 0)
                    {
                        resultTypesBuilder.Append("|");
                    }

                    resultTypesBuilder.Append(resultType.ToEnumString<ResultType>());
                }

                query.Add("result_type", resultTypesBuilder.ToString());
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Result Types"]);
            }

            if (parameters.LocationTypes != null)
            {
                var locationTypesBuilder = new StringBuilder();
                foreach (var locationType in parameters.LocationTypes)
                {
                    if (locationTypesBuilder.Length > 0)
                    {
                        locationTypesBuilder.Append("|");
                    }

                    locationTypesBuilder.Append(locationType.ToEnumString<LocationType>());
                }

                query.Add("location_type", locationTypesBuilder.ToString());
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Location Types"]);
            }

            AddBaseParameters(parameters, query);

            AddGoogleKey(query);

            uriBuilder.Query = query.ToString();

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
            var uriBuilder = new UriBuilder(_findPlaceUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Input))
            {
                var error = _localizer["Invalid Input"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.Input));
            }

            if (parameters.InputType > InputType.PhoneNumber || parameters.InputType < InputType.TextQuery)
            {
                var error = _localizer["Invalid Input Type"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.InputType));
            }

            query.Add("input", parameters.Input);

            query.Add("inputtype", parameters.InputType.ToEnumString());

            if (parameters.Fields != null && parameters.Fields.Count > 0)
            {
                query.Add("fields", string.Join(",", parameters.Fields));
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Fields"]);
            }

            if (parameters.LocationBias != null)
            {
                if (parameters.LocationBias.GetType() == typeof(Coordinate))
                {
                    query.Add("locationbias", $"point:{parameters.LocationBias}");
                }
                else if (parameters.LocationBias.GetType() == typeof(Boundaries))
                {
                    query.Add("locationbias", $"rectangle:{parameters.LocationBias}");
                }
                else if (parameters.LocationBias.GetType() == typeof(Circle))
                {
                    query.Add("locationbias", $"circle:{parameters.LocationBias}");
                }
                else
                {
                    _logger?.LogWarning(_localizer["Invalid Location Bias Type"]);
                }
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Location Bias"]);
            }

            AddBaseParameters(parameters, query);

            AddGoogleKey(query);

            uriBuilder.Query = query.ToString();

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
            var uriBuilder = new UriBuilder(_nearbySearchUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parameters.Location == null)
            {
                var error = _localizer["Invalid Location"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.Location));
            }

            if (parameters.RankBy == RankType.Distance)
            {
                if (parameters.Radius > 0)
                {
                    var error = _localizer["Invalid RankBy Distance Radius"];
                    _logger?.LogError(error);
                    throw new ArgumentException(error, nameof(parameters.Radius));
                }

                if (string.IsNullOrWhiteSpace(parameters.Keyword) && string.IsNullOrWhiteSpace(parameters.Type))
                {
                    var error = _localizer["Invalid RankBy Distance Request"];
                    _logger?.LogError(error);
                    throw new ArgumentException(error, nameof(parameters.RankBy));
                }
            }
            else if (parameters.Radius <= 0)
            {
                var error = _localizer["Invalid Radius"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.Radius));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Keyword))
            {
                query.Add("keyword", parameters.Keyword);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Keyword"]);
            }

            if (parameters.RankBy >= RankType.Prominence && parameters.RankBy <= RankType.Distance)
            {
                query.Add("rankby", parameters.RankBy.ToEnumString());
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid RankBy"]);
            }

            AddBaseSearchParameters(parameters, query);

            AddGoogleKey(query);

            uriBuilder.Query = query.ToString();

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
            var uriBuilder = new UriBuilder(_textSearchUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                var error = _localizer["Invalid Query"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.Query));
            }

            query.Add("query", parameters.Query);

            if (!string.IsNullOrWhiteSpace(parameters.Region))
            {
                query.Add("region", parameters.Region);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Region"]);
            }

            AddBaseSearchParameters(parameters, query);

            AddGoogleKey(query);

            uriBuilder.Query = query.ToString();

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
            var uriBuilder = new UriBuilder(_detailsUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.PlaceId))
            {
                var error = _localizer["Invalid PlaceId"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.PlaceId));
            }

            query.Add("place_id", parameters.PlaceId);

            if (!string.IsNullOrWhiteSpace(parameters.Region))
            {
                query.Add("region", parameters.Region);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Region"]);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SessionToken))
            {
                query.Add("sessiontoken", parameters.SessionToken);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Session Token"]);
            }

            if (parameters.Fields != null && parameters.Fields.Count > 0)
            {
                query.Add("fields", string.Join(",", parameters.Fields));
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Fields"]);
            }

            AddBaseParameters(parameters, query);

            AddGoogleKey(query);

            uriBuilder.Query = query.ToString();

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
            var uriBuilder = new UriBuilder(_placeAutocompleteUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Input))
            {
                var error = _localizer["Invalid Input"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.Input));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SessionToken))
            {
                query.Add("sessiontoken", parameters.SessionToken);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Session Token"]);
            }

            if (parameters.Origin != null)
            {
                query.Add("origin", parameters.Origin.ToString());
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Origin"]);
            }

            if (parameters.Types != null && parameters.Types.Count > 0)
            {
                query.Add("types", string.Join(",", parameters.Types.Select(x => x.ToEnumString())));
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Types"]);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Components))
            {
                query.Add("components", parameters.Components);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Components"]);
            }

#pragma warning disable CA1308 // Normalize strings to uppercase
            query.Add("strictbounds", parameters.StrictBounds.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            AddAutocompleteParameters(parameters, query);

            AddGoogleKey(query);

            uriBuilder.Query = query.ToString();

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
            var uriBuilder = new UriBuilder(_queryAutocompleteUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Input))
            {
                var error = _localizer["Invalid Input"];
                _logger?.LogError(error);
                throw new ArgumentException(error, nameof(parameters.Input));
            }

            AddAutocompleteParameters(parameters, query);

            AddGoogleKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the autocomplete parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="QueryAutocompleteParameters"/> with the autocomplete parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddAutocompleteParameters(QueryAutocompleteParameters parameters, NameValueCollection query)
        {
            if (parameters.Offset > 0)
            {
                query.Add("offset", parameters.Offset.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger?.LogWarning(_localizer["Invalid Offset"]);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Input))
            {
                query.Add("input", parameters.Input);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Input Info"]);
            }

            AddCoordinateParameters(parameters, query);
        }

        /// <summary>
        /// Adds the base search parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseSearchParameters"/> with the base search parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddBaseSearchParameters(BaseSearchParameters parameters, NameValueCollection query)
        {
            if (parameters.MinimumPrice >= 0 && parameters.MinimumPrice <= 4 && parameters.MinimumPrice <= parameters.MaximumPrice)
            {
                query.Add("minprice", parameters.MinimumPrice.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger?.LogWarning(_localizer["Invalid Minimum Price"]);
            }

            if (parameters.MaximumPrice >= 0 && parameters.MaximumPrice <= 4 && parameters.MinimumPrice <= parameters.MaximumPrice)
            {
                query.Add("maxprice", parameters.MaximumPrice.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger?.LogWarning(_localizer["Invalid Maximum Price"]);
            }

#pragma warning disable CA1308 // Normalize strings to uppercase
            query.Add("opennow", parameters.OpenNow.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (parameters.PageToken > 0)
            {
                query.Add("pagetoken", parameters.PageToken.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger?.LogWarning(_localizer["Invalid Page Token"]);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Type))
            {
                query.Add("type", parameters.Type);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Type"]);
            }

            AddCoordinateParameters(parameters, query);
        }

        /// <summary>
        /// Adds the coordinate parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="CoordinateParameters"/> with the coordinate parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddCoordinateParameters(CoordinateParameters parameters, NameValueCollection query)
        {
            if (parameters.Location != null)
            {
                query.Add("location", parameters.Location.ToString());
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Location"]);
            }

            if (parameters.Radius > 0 && parameters.Radius <= 50000)
            {
                query.Add("radius", parameters.Radius.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger?.LogWarning(_localizer["Invalid Radius Value"]);
            }

            AddBaseParameters(parameters, query);
        }

        /// <summary>
        /// Adds the base query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddBaseParameters(BaseParameters parameters, NameValueCollection query)
        {
            if (parameters.Language != null)
            {
                query.Add("language", parameters.Language);
            }
            else
            {
                _logger?.LogDebug(_localizer["Invalid Language"]);
            }
        }

        /// <summary>
        /// Adds the Google key to the query parameters.
        /// </summary>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddGoogleKey(NameValueCollection query)
        {
            query.Add("key", _keyContainer.GetKey());
        }
    }
}
