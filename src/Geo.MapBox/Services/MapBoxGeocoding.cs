// <copyright file="MapBoxGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Geo.Here.Tests")]

namespace Geo.MapBox.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Geo.Core;
    using Geo.MapBox.Abstractions;
    using Geo.MapBox.Enums;
    using Geo.MapBox.Models;
    using Geo.MapBox.Models.Parameters;
    using Geo.MapBox.Models.Responses;

    /// <summary>
    /// A service to call the MapBox geocoding api.
    /// </summary>
    public class MapBoxGeocoding : ClientExecutor, IMapBoxGeocoding
    {
        private readonly string _geocodeUri = "https://api.mapbox.com/geocoding/v5/{0}/{1}.json";
        private readonly string _reverseGeocodeUri = "https://api.mapbox.com/geocoding/v5/{0}/{1}.json";
        private readonly string _placesEndpoint = "mapbox.places";
        private readonly string _permanentEndpoint = "mapbox.places-permanent";
        private readonly IMapBoxKeyContainer _keyContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapBoxGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the here Geocoding API.</param>
        /// <param name="keyContainer">A <see cref="IMapBoxKeyContainer"/> used for fetching the here key.</param>
        public MapBoxGeocoding(
            HttpClient client,
            IMapBoxKeyContainer keyContainer)
            : base(client)
        {
            _keyContainer = keyContainer;
        }

        /// <inheritdoc/>
        public async Task<Response<List<string>>> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<Response<List<string>>>(BuildGeocodingRequest(parameters), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response<Coordinate>> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<Response<Coordinate>>(BuildReverseGeocodingRequest(parameters), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed MapBox geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Query' parameter and the 'QualifiedQuery' parameter are null or invalid.</exception>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                throw new ArgumentException("The query cannot be null or empty.", nameof(parameters.Query));
            }

            var uriBuilder = new UriBuilder(string.Format(_geocodeUri, parameters.EndpointType == EndpointType.Places ? _placesEndpoint : _permanentEndpoint, parameters.Query));
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query.Add("autocomplete", parameters.ReturnAutocomplete.ToString().ToLowerInvariant());

            if (parameters.BoundingBox != null)
            {
                query.Add("bbox", parameters.BoundingBox.ToString());
            }

            query.Add("fuzzyMatch", parameters.FuzzyMatch.ToString().ToLowerInvariant());

            if (parameters.Proximity != null)
            {
                query.Add("proximity", parameters.Proximity.ToString());
            }

            AddBaseParameters(parameters, query);

            AddMapBoxKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed MapBox reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Coordinate' parameter is null or invalid.</exception>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters)
        {
            if (parameters.Coordinate is null)
            {
                throw new ArgumentException("The coordinates cannot be null.", nameof(parameters.Coordinate));
            }

            var uriBuilder = new UriBuilder(string.Format(_reverseGeocodeUri, parameters.EndpointType == EndpointType.Places ? _placesEndpoint : _permanentEndpoint, parameters.Coordinate));
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            query.Add("reverseMode", parameters.ReverseMode.ToString().ToLowerInvariant());

            AddBaseParameters(parameters, query);

            AddMapBoxKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the base query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddBaseParameters(BaseParameters parameters, NameValueCollection query)
        {
            if (parameters.Countries != null && parameters.Countries.Count > 0)
            {
                query.Add("country", string.Join(",", parameters.Countries));
            }

            if (parameters.Languages != null && parameters.Languages.Count > 0)
            {
                query.Add("language", string.Join(",", parameters.Languages));
            }

            if (parameters.Limit > 0 && parameters.Limit < 6)
            {
                query.Add("limit", parameters.Limit.ToString(CultureInfo.InvariantCulture));
            }

            query.Add("routing", parameters.Routing.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());

            if (parameters.Types != null && parameters.Types.Count > 0)
            {
                query.Add("types", string.Join(",", parameters.Types.ToString().ToLowerInvariant()));
            }
        }

        /// <summary>
        /// Adds the here key to the query parameters.
        /// </summary>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddMapBoxKey(NameValueCollection query)
        {
            query.Add("access_token", _keyContainer.GetKey());
        }
    }
}
