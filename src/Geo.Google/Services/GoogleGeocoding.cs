// <copyright file="GoogleGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Geo.Google.Tests")]

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
    using Geo.Google.Abstractions;
    using Geo.Google.Enums;
    using Geo.Google.Extensions;
    using Geo.Google.Models;
    using Geo.Google.Models.Exceptions;
    using Geo.Google.Models.Parameters;
    using Geo.Google.Models.Responses;
    using Newtonsoft.Json;

    /// <summary>
    /// A service to call the Google geocoding api.
    /// </summary>
    public class GoogleGeocoding : ClientExecutor, IGoogleGeocoding
    {
        private readonly string _geocodingUri = "https://maps.googleapis.com/maps/api/geocode/json";
        private readonly string _findPlaceUri = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json";
        private readonly string _nearbySearchUri = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";
        private readonly string _textSearchUri = "https://maps.googleapis.com/maps/api/place/textsearch/json";
        private readonly string _detailsUri = "https://maps.googleapis.com/maps/api/place/details/json";
        private readonly string _placeAutocompleteUri = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
        private readonly string _queryAutocompleteUri = "https://maps.googleapis.com/maps/api/place/queryautocomplete/json";
        private readonly IGoogleKeyContainer _keyContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Google Geocoding API.</param>
        /// <param name="keyContainer">A <see cref="IGoogleKeyContainer"/> used for fetching the Google key.</param>
        public GoogleGeocoding(
            HttpClient client,
            IGoogleKeyContainer keyContainer)
            : base(client)
        {
            _keyContainer = keyContainer;
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await CallGoogleAsync<GeocodingResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> ReverseGeocodingAsync(
        ReverseGeocodingParameters parameters,
        CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await CallGoogleAsync<GeocodingResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<FindPlaceResponse> FindPlacesAsync(
            FindPlacesParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<FindPlacesParameters>(parameters, BuildFindPlaceRequest);

            return await CallGoogleAsync<FindPlaceResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<PlaceResponse> NearbySearchAsync(
            NearbySearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<NearbySearchParameters>(parameters, BuildNearbySearchRequest);

            return await CallGoogleAsync<PlaceResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<PlaceResponse> TextSearchAsync(
            TextSearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<TextSearchParameters>(parameters, BuildTextSearchRequest);

            return await CallGoogleAsync<PlaceResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DetailsResponse> DetailsAsync(
            DetailsParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<DetailsParameters>(parameters, BuildDetailsRequest);

            return await CallGoogleAsync<DetailsResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<AutocompleteResponse<PlaceAutocomplete>> PlaceAutocompleteAsync(
            PlacesAutocompleteParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<PlacesAutocompleteParameters>(parameters, BuildPlaceAutocompleteRequest);

            return await CallGoogleAsync<AutocompleteResponse<PlaceAutocomplete>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<AutocompleteResponse<QueryAutocomplete>> QueryAutocompleteAsync(
            QueryAutocompleteParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<QueryAutocompleteParameters>(parameters, BuildQueryAutocompleteRequest);

            return await CallGoogleAsync<AutocompleteResponse<QueryAutocomplete>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Validates the uri and builds it based on the parameter type.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="parameters">The parameters to validate and create a uri from.</param>
        /// <param name="uriBuilderFunction">The method to use to create the uri.</param>
        /// <returns>A <see cref="Uri"/> with the uri crafted from the parameters.</returns>
        internal Uri ValidateAndCraftUri<TParameters>(TParameters parameters, Func<TParameters, Uri> uriBuilderFunction)
        {
            if (parameters is null)
            {
                throw new GoogleException("The Google parameters are null.", new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                throw new GoogleException("Failed to create the Google uri.", ex);
            }
        }

        /// <summary>
        /// Calls Google with the request information.
        /// </summary>
        /// <typeparam name="TResult">The return type to parse the response into.</typeparam>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <typeparamref name="TResult"/>.</returns>
        internal async Task<TResult> CallGoogleAsync<TResult>(Uri uri, CancellationToken cancellationToken = default)
        {
            try
            {
                return await CallAsync<TResult>(uri, cancellationToken).ConfigureAwait(false);
            }
            catch (ArgumentNullException ex)
            {
                throw new GoogleException("The Google uri is null.", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new GoogleException("The Google request failed.", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new GoogleException("The Google request was cancelled.", ex);
            }
            catch (JsonReaderException ex)
            {
                throw new GoogleException("Failed to parse the Google response properly.", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new GoogleException("Failed to parse the Google response properly.", ex);
            }
            catch (Exception ex)
            {
                throw new GoogleException("The call to Google failed with an exception.", ex);
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
                throw new ArgumentException("The address cannot be null or empty.", nameof(parameters.Address));
            }

            query.Add("address", parameters.Address);

            if (parameters.Components != null)
            {
                query.Add("components", parameters.Components);
            }

            if (parameters.Bounds != null &&
                parameters.Bounds.Southwest != null &&
                parameters.Bounds.Northeast != null)
            {
                query.Add("bounds", parameters.Bounds.ToString());
            }

            if (parameters.Region != null)
            {
                query.Add("region", parameters.Region);
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
                throw new ArgumentException("The coordinates cannot be null.", nameof(parameters.Coordinate));
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
                throw new ArgumentException("The input cannot be null or empty.", nameof(parameters.Input));
            }

            if (parameters.InputType > InputType.PhoneNumber || parameters.InputType < InputType.TextQuery)
            {
                throw new ArgumentException("The input type must be valid.", nameof(parameters.InputType));
            }

            query.Add("input", parameters.Input);

            query.Add("inputtype", parameters.InputType.ToEnumString());

            if (parameters.Fields != null && parameters.Fields.Count > 0)
            {
                query.Add("fields", string.Join(",", parameters.Fields));
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
                else
                {
                    query.Add("locationbias", $"circle:{parameters.LocationBias}");
                }
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
        /// Thrown when the 'Location' parameter is null or invalid.
        /// Thrown when the 'RankBy' is Distance and a 'Radius' is entered or a 'Keyword' or 'Type' is not entered.
        /// Thrown when the 'RankBy' is not Distance and the 'Radius' is not > 0.
        /// </exception>
        internal Uri BuildNearbySearchRequest(NearbySearchParameters parameters)
        {
            var uriBuilder = new UriBuilder(_nearbySearchUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parameters.Location == null)
            {
                throw new ArgumentException("The location cannot be null.", nameof(parameters.Location));
            }

            if (parameters.RankBy == RankType.Distance)
            {
                if (parameters.Radius > 0)
                {
                    throw new ArgumentException("The radius must not be greater than 0 on a rank by distance request.", nameof(parameters.Radius));
                }

                if (string.IsNullOrWhiteSpace(parameters.Keyword) && string.IsNullOrWhiteSpace(parameters.Type))
                {
                    throw new ArgumentException("The keyword or type must be specified when ranking by distance.", nameof(parameters.RankBy));
                }
            }
            else if (parameters.Radius <= 0)
            {
                throw new ArgumentException("The radius must be greater than 0.", nameof(parameters.Radius));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Keyword))
            {
                query.Add("keyword", parameters.Keyword);
            }

            if (parameters.RankBy >= RankType.Prominence && parameters.RankBy <= RankType.Distance)
            {
                query.Add("rankby", parameters.RankBy.ToEnumString());
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
                throw new ArgumentException("The query cannot be null or invalid.", nameof(parameters.Query));
            }

            query.Add("query", parameters.Query);

            if (!string.IsNullOrWhiteSpace(parameters.Region))
            {
                query.Add("region", parameters.Region);
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
                throw new ArgumentException("The place id cannot be null or invalid.", nameof(parameters.PlaceId));
            }

            query.Add("place_id", parameters.PlaceId);

            if (!string.IsNullOrWhiteSpace(parameters.Region))
            {
                query.Add("region", parameters.Region);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SessionToken))
            {
                query.Add("sessiontoken", parameters.SessionToken);
            }

            if (parameters.Fields != null && parameters.Fields.Count > 0)
            {
                query.Add("fields", string.Join(",", parameters.Fields));
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
                throw new ArgumentException("The input cannot be null or invalid.", nameof(parameters.Input));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SessionToken))
            {
                query.Add("sessiontoken", parameters.SessionToken);
            }

            if (parameters.Origin != null)
            {
                query.Add("origin", parameters.Origin.ToString());
            }

            if (parameters.Types != null && parameters.Types.Count > 0)
            {
                query.Add("types", string.Join(",", parameters.Types.Select(x => x.ToEnumString())));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Components))
            {
                query.Add("components", parameters.Components);
            }

            query.Add("strictbounds", parameters.StrictBounds.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());

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
                throw new ArgumentException("The input cannot be null or invalid.", nameof(parameters.Input));
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

            if (!string.IsNullOrWhiteSpace(parameters.Input))
            {
                query.Add("input", parameters.Input);
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

            if (parameters.MaximumPrice >= 0 && parameters.MaximumPrice <= 4 && parameters.MinimumPrice <= parameters.MaximumPrice)
            {
                query.Add("maxprice", parameters.MinimumPrice.ToString(CultureInfo.InvariantCulture));
            }

            query.Add("opennow", parameters.OpenNow.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());

            if (parameters.PageToken > 0)
            {
                query.Add("pagetoken", parameters.PageToken.ToString(CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Type))
            {
                query.Add("type", parameters.Type);
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

            if (parameters.Radius > 0 && parameters.Radius <= 50000)
            {
                query.Add("radius", parameters.Radius.ToString(CultureInfo.InvariantCulture));
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
