// <copyright file="MapQuestGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Services
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core;
    using Geo.MapQuest.Abstractions;
    using Geo.MapQuest.Enums;
    using Geo.MapQuest.Models.Exceptions;
    using Geo.MapQuest.Models.Parameters;
    using Geo.MapQuest.Models.Responses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// A service to call the MapQuest geocoding API.
    /// </summary>
    public class MapQuestGeocoding : ClientExecutor, IMapQuestGeocoding
    {
        private const string ApiName = "MapQuest";
        private const string OpenGeocodeUri = "http://open.mapquestapi.com/geocoding/v1/address";
        private const string OpenReverseGeocodeUri = "http://open.mapquestapi.com/geocoding/v1/reverse";
        private const string GeocodeUri = "http://www.mapquestapi.com/geocoding/v1/address";
        private const string ReverseGeocodeUri = "http://www.mapquestapi.com/geocoding/v1/reverse";

        private readonly IMapQuestKeyContainer _keyContainer;
        private readonly IMapQuestEndpoint _endpoint;
        private readonly IGeoNETResourceStringProvider _resourceStringProvider;
        private readonly ILogger<MapQuestGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapQuestGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the MapQuest Geocoding API.</param>
        /// <param name="keyContainer">A <see cref="IMapQuestKeyContainer"/> used for fetching the MapQuest key.</param>
        /// <param name="endpoint">A <see cref="IMapQuestEndpoint"/> used for fetching which MapQuest endpoint to use.</param>
        /// <param name="exceptionProvider">An <see cref="IGeoNETExceptionProvider"/> used to provide exceptions based on an exception type.</param>
        /// <param name="resourceStringProviderFactory">An <see cref="IGeoNETResourceStringProviderFactory"/> used to create a resource string provider for log or exception messages.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public MapQuestGeocoding(
            HttpClient client,
            IMapQuestKeyContainer keyContainer,
            IMapQuestEndpoint endpoint,
            IGeoNETExceptionProvider exceptionProvider,
            IGeoNETResourceStringProviderFactory resourceStringProviderFactory,
            ILoggerFactory loggerFactory = null)
            : base(client, exceptionProvider, resourceStringProviderFactory, loggerFactory)
        {
            _keyContainer = keyContainer ?? throw new ArgumentNullException(nameof(keyContainer));
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            _resourceStringProvider = resourceStringProviderFactory?.CreateResourceStringProvider<MapQuestGeocoding>() ?? throw new ArgumentNullException(nameof(resourceStringProviderFactory));
            _logger = loggerFactory?.CreateLogger<MapQuestGeocoding>() ?? NullLogger<MapQuestGeocoding>.Instance;
        }

        /// <inheritdoc/>
        public async Task<Response<GeocodeResult>> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await CallAsync<Response<GeocodeResult>, MapQuestException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response<ReverseGeocodeResult>> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await CallAsync<Response<ReverseGeocodeResult>, MapQuestException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds the base query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal static void AddBaseParameters(BaseParameters parameters, ref QueryString query)
        {
#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("thumbMaps", parameters.IncludeThumbMaps.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase
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
                var error = _resourceStringProvider.GetString("Null Parameters");
                _logger.MapQuestError(error);
                throw new MapQuestException(error, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                var error = _resourceStringProvider.GetString("Failed To Create Uri");
                _logger.MapQuestError(error);
                throw new MapQuestException(error, ex);
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
            var uriBuilder = new UriBuilder(_endpoint.UseLicensedEndpoint() ? GeocodeUri : OpenGeocodeUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Location))
            {
                var error = _resourceStringProvider.GetString("Invalid Location");
                _logger.MapQuestError(error);
                throw new ArgumentException(error, nameof(parameters.Location));
            }

            query = query.Add("location", parameters.Location);

            if (!(parameters.BoundingBox is null) &&
                (parameters.BoundingBox.East != 0 && parameters.BoundingBox.North != 0 &&
                parameters.BoundingBox.West != 0 && parameters.BoundingBox.South != 0))
            {
                query = query.Add("boundingBox", parameters.BoundingBox.ToString());
            }
            else
            {
                _logger.MapQuestWarning(_resourceStringProvider.GetString("Invalid Bounding Box"));
            }

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("ignoreLatLngInput", parameters.IgnoreLatLngInput.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (parameters.MaxResults > 0)
            {
                query = query.Add("maxResults", parameters.MaxResults.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.MapQuestWarning(_resourceStringProvider.GetString("Invalid Maximum Results"));
            }

            if (parameters.IntlMode == InternationalMode.FiveBox)
            {
                query = query.Add("intlMode", "5BOX");
            }
            else if (parameters.IntlMode == InternationalMode.OneBox)
            {
                query = query.Add("intlMode", "1BOX");
            }
            else if (parameters.IntlMode == InternationalMode.Auto)
            {
                query = query.Add("intlMode", "AUTO");
            }
            else
            {
                _logger.MapQuestWarning(_resourceStringProvider.GetString("Invalid Intl Mode"));
            }

            AddBaseParameters(parameters, ref query);

            AddMapQuestKey(ref query);

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
            var uriBuilder = new UriBuilder(_endpoint.UseLicensedEndpoint() ? ReverseGeocodeUri : OpenReverseGeocodeUri);
            var query = QueryString.Empty;

            if (parameters.Location is null)
            {
                var error = _resourceStringProvider.GetString("Invalid Location");
                _logger.MapQuestError(error);
                throw new ArgumentException(error, nameof(parameters.Location));
            }

            query = query.Add("location", parameters.Location.ToString());

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("includeNearestIntersection", parameters.IncludeNearestIntersection.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("includeRoadMetadata", parameters.IncludeRoadMetadata.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            AddBaseParameters(parameters, ref query);

            AddMapQuestKey(ref query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the MapQuest key to the query parameters.
        /// </summary>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddMapQuestKey(ref QueryString query)
        {
            query = query.Add("key", _keyContainer.GetKey());
        }
    }
}
