// <copyright file="MapQuestGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Services
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Geo.Core;
    using Geo.MapQuest.Abstractions;
    using Geo.MapQuest.Enums;
    using Geo.MapQuest.Models.Exceptions;
    using Geo.MapQuest.Models.Parameters;
    using Geo.MapQuest.Models.Responses;

    /// <summary>
    /// A service to call the MapQuest geocoding api.
    /// </summary>
    public class MapQuestGeocoding : ClientExecutor, IMapQuestGeocoding
    {
        private const string _apiName = "MapQuest";
        private readonly string _openGeocodeUri = "http://open.mapquestapi.com/geocoding/v1/address";
        private readonly string _openReverseGeocodeUri = "http://open.mapquestapi.com/geocoding/v1/reverse";
        private readonly string _geocodeUri = "http://www.mapquestapi.com/geocoding/v1/address";
        private readonly string _reverseGeocodeUri = "http://www.mapquestapi.com/geocoding/v1/reverse";
        private readonly IMapQuestKeyContainer _keyContainer;
        private readonly IMapQuestEndpoint _endpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapQuestGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the MapQuest Geocoding API.</param>
        /// <param name="keyContainer">A <see cref="IMapQuestKeyContainer"/> used for fetching the MapQuest key.</param>
        /// <param name="endpoint">A <see cref="IMapQuestEndpoint"/> used for fetching which MapQuest endpoint to use.</param>
        public MapQuestGeocoding(
            HttpClient client,
            IMapQuestKeyContainer keyContainer,
            IMapQuestEndpoint endpoint)
            : base(client)
        {
            _keyContainer = keyContainer;
            _endpoint = endpoint;
        }

        /// <inheritdoc/>
        public async Task<Response<GeocodeResult>> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await CallAsync<Response<GeocodeResult>, MapQuestException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response<ReverseGeocodeResult>> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndCraftUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await CallAsync<Response<ReverseGeocodeResult>, MapQuestException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
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
                throw new MapQuestException("The MapQuest parameters are null.", new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                throw new MapQuestException("Failed to create the MapQuest uri.", ex);
            }
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed MapQuest geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Location' parameter is null or invalid.</exception>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(_endpoint.UseLicensedEndpoint() ? _geocodeUri : _openGeocodeUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Location))
            {
                throw new ArgumentException("The location cannot be null or empty.", nameof(parameters.Location));
            }

            query.Add("location", parameters.Location);

            if (!(parameters.BoundingBox is null) &&
                (parameters.BoundingBox.East != 0 && parameters.BoundingBox.North != 0 &&
                parameters.BoundingBox.West != 0 && parameters.BoundingBox.South != 0))
            {
                query.Add("boundingBox", parameters.BoundingBox.ToString());
            }

            query.Add("ignoreLatLngInput", parameters.IgnoreLatLngInput.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());

            if (parameters.MaxResults > 0)
            {
                query.Add("maxResults", parameters.MaxResults.ToString(CultureInfo.InvariantCulture));
            }

            if (parameters.IntlMode == InternationalMode.FiveBox)
            {
                query.Add("intlMode", "5BOX");
            }
            else if (parameters.IntlMode == InternationalMode.OneBox)
            {
                query.Add("intlMode", "1BOX");
            }
            else if (parameters.IntlMode == InternationalMode.Auto)
            {
                query.Add("intlMode", "AUTO");
            }

            AddBaseParameters(parameters, query);

            AddMapQuestKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed MapQuest reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Location' parameter is null or invalid.</exception>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(_endpoint.UseLicensedEndpoint() ? _reverseGeocodeUri : _openReverseGeocodeUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parameters.Location is null)
            {
                throw new ArgumentException("The location cannot be null.", nameof(parameters.Location));
            }

            query.Add("location", parameters.Location.ToString());

            query.Add("includeNearestIntersection", parameters.IncludeNearestIntersection.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());

            query.Add("includeRoadMetadata", parameters.IncludeRoadMetadata.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());

            AddBaseParameters(parameters, query);

            AddMapQuestKey(query);

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
            query.Add("thumbMaps", parameters.IncludeThumbMaps.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
        }

        /// <summary>
        /// Adds the MapQuest key to the query parameters.
        /// </summary>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddMapQuestKey(NameValueCollection query)
        {
            query.Add("key", _keyContainer.GetKey());
        }
    }
}
