// <copyright file="OlaGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Ola.Services
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Core.Models.Exceptions;
    using Geo.Ola.Models.Parameters;
    using Geo.Ola.Models.Responses;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the Ola geocoding API.
    /// </summary>
    public class OlaGeocoding : GeoClient, IOlaGeocoding
    {
        private const string GeocodeUri = "https://api.olamaps.io/places/v1/geocode";
        private const string ReverseGeocodeUri = "https://api.olamaps.io/places/v1/reverse-geocode";

        private readonly IOptions<KeyOptions<IOlaGeocoding>> _options;
        private readonly ILogger<OlaGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="OlaGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Radar Geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="KeyOptions{T}"/> containing Radar key information.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public OlaGeocoding(
            HttpClient client,
            IOptions<KeyOptions<IOlaGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<OlaGeocoding>() ?? NullLogger<OlaGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "Radar";

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await GetAsync<GeocodingResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ReverseGeocodingResponse> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await GetAsync<ReverseGeocodingResponse>(uri, cancellationToken).ConfigureAwait(false);
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
                _logger.RadarError(Resources.Services.OlaGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.OlaGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.RadarError(Resources.Services.OlaGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.OlaGeocoding.Failed_To_Create_Uri, ex);
            }
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Radar geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="GeocodingParameters.Address"/> parameter is null or invalid.</exception>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(GeocodeUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Address))
            {
                _logger.RadarError(Resources.Services.OlaGeocoding.Invalid_Address);
                throw new ArgumentException(Resources.Services.OlaGeocoding.Invalid_Address, nameof(parameters.Address));
            }

            query = query.Add("address", parameters.Address);

            AddLanguage(parameters, ref query);
            AddOlaKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Radar reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="ReverseGeocodingParameters.Coordinate"/> parameter is null or invalid.</exception>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(ReverseGeocodeUri);
            var query = QueryString.Empty;

            if (parameters.Coordinate is null)
            {
                _logger.RadarError(Resources.Services.OlaGeocoding.Invalid_Coordinates);
                throw new ArgumentException(Resources.Services.OlaGeocoding.Invalid_Coordinates, nameof(parameters.Coordinate));
            }

            query = query.Add("latlng", parameters.Coordinate.ToString());

            AddOlaKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the language filter information to the query.
        /// </summary>
        /// <param name="parameters">The <see cref="GeocodingParameters"/> used to get the language information from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddLanguage(GeocodingParameters parameters, ref QueryString query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Language))
            {
                query = query.Add("language", parameters.Language);
            }
            else
            {
                _logger.RadarDebug(Resources.Services.OlaGeocoding.Invalid_Language);
            }
        }

        /// <summary>
        /// Adds the Radar key to the request.
        /// </summary>
        /// <param name="keyParameter">An <see cref="IKeyParameters"/> to conditionally get the key from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddOlaKey(IKeyParameters keyParameter, ref QueryString query)
        {
            var key = _options.Value.Key;

            if (!string.IsNullOrWhiteSpace(keyParameter.Key))
            {
                key = keyParameter.Key;
            }

            query = query.Add("apiKey", key);
        }
    }
}
