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
    using Geo.Google.Models.Parameters;
    using Geo.Google.Models.Responses;

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
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<GeocodingResponse>(BuildGeocodingRequest(parameters), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<GeocodingResponse>(BuildReverseGeocodingRequest(parameters), cancellationToken).ConfigureAwait(false);
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
        /// <exception cref="ArgumentException">Thrown when the 'Address' parameter is null or invalid.</exception>
        internal Uri BuildFindPlaceRequest(FindPlacesParameters parameters)
        {
            var uriBuilder = new UriBuilder(_geocodingUri);
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

            query.Add("inputtype", parameters.InputType.ToString());

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
        /// Adds the autocomplete parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="AutocompleteParameters"/> with the autocomplete parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddAutocompleteParameters(AutocompleteParameters parameters, NameValueCollection query)
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
