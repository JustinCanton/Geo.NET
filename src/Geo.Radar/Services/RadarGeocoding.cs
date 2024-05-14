// <copyright file="RadarGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Services
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Core.Models.Exceptions;
    using Geo.Radar.Models;
    using Geo.Radar.Models.Parameters;
    using Geo.Radar.Models.Responses;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the Radar geocoding API.
    /// </summary>
    public class RadarGeocoding : GeoClient, IRadarGeocoding
    {
        private const string GeocodeUri = "https://api.radar.io/v1/geocode/forward";
        private const string ReverseGeocodeUri = "https://api.radar.io/v1/geocode/reverse";
        private const string AutocompleteUri = "https://api.radar.io/v1/search/autocomplete";

        private readonly IOptions<KeyOptions<IRadarGeocoding>> _options;
        private readonly ILogger<RadarGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadarGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Radar Geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="KeyOptions{T}"/> containing Radar key information.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public RadarGeocoding(
            HttpClient client,
            IOptions<KeyOptions<IRadarGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<RadarGeocoding>() ?? NullLogger<RadarGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "Radar";

        /// <inheritdoc/>
        public async Task<Response<GeocodeAddress>> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await GetAsync<Response<GeocodeAddress>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response<ReverseGeocodeAddress>> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await GetAsync<Response<ReverseGeocodeAddress>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response<ReverseGeocodeAddress>> AutocompleteAsync(
            AutocompleteParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<AutocompleteParameters>(parameters, BuildAutocompleteRequest);

            return await GetAsync<Response<ReverseGeocodeAddress>>(uri, cancellationToken).ConfigureAwait(false);
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
                _logger.RadarError(Resources.Services.RadarGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.RadarGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.RadarError(Resources.Services.RadarGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.RadarGeocoding.Failed_To_Create_Uri, ex);
            }
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Radar geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="GeocodingParameters.Query"/> parameter is null or invalid.</exception>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(GeocodeUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                _logger.RadarError(Resources.Services.RadarGeocoding.Invalid_Query);
                throw new ArgumentException(Resources.Services.RadarGeocoding.Invalid_Query, nameof(parameters.Query));
            }

            query = query.Add("query", parameters.Query);

            AddCountry(parameters, ref query);
            AddLayers(parameters, ref query);
            AddRadarKey(parameters);

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
                _logger.RadarError(Resources.Services.RadarGeocoding.Invalid_Coordinates);
                throw new ArgumentException(Resources.Services.RadarGeocoding.Invalid_Coordinates, nameof(parameters.Coordinate));
            }

            query = query.Add("coordinates", parameters.Coordinate.ToString());

            AddLayers(parameters, ref query);
            AddRadarKey(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the autocomplete uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="AutocompleteParameters"/> with the autocomplete parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Radar autocomplete uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="AutocompleteParameters.Query"/> parameter is null or invalid.</exception>
        internal Uri BuildAutocompleteRequest(AutocompleteParameters parameters)
        {
            var uriBuilder = new UriBuilder(AutocompleteUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                _logger.RadarError(Resources.Services.RadarGeocoding.Invalid_Query);
                throw new ArgumentException(Resources.Services.RadarGeocoding.Invalid_Query, nameof(parameters.Query));
            }

            query = query.Add("query", parameters.Query);

            if (parameters.Near != null)
            {
                query = query.Add("near", parameters.Near.ToString());
            }
            else
            {
                _logger.RadarDebug(Resources.Services.RadarGeocoding.Invalid_Near);
            }

            if (parameters.Limit > 0 || parameters.Limit <= 100)
            {
                query = query.Add("limit", parameters.Limit.ToString());
            }
            else
            {
                _logger.RadarDebug(Resources.Services.RadarGeocoding.Invalid_Limit);
            }

            query = query.Add("mailable", parameters.Mailable.ToString().ToLowerInvariant());

            AddCountry(parameters, ref query);
            AddLayers(parameters, ref query);
            AddRadarKey(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the country filter information to the query.
        /// </summary>
        /// <param name="countryParameter">The <see cref="ICountryParameter"/> used to get the country filter information from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddCountry(ICountryParameter countryParameter, ref QueryString query)
        {
            var countries = string.Join(",", countryParameter.Countries ?? Array.Empty<string>());

            if (!string.IsNullOrWhiteSpace(countries))
            {
                query = query.Add("country", countries);
            }
            else
            {
                _logger.RadarDebug(Resources.Services.RadarGeocoding.Invalid_Country);
            }
        }

        /// <summary>
        /// Adds the layers filter information to the query.
        /// </summary>
        /// <param name="layersParameter">The <see cref="ICountryParameter"/> used to get the country filter information from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddLayers(ILayersParameter layersParameter, ref QueryString query)
        {
            var layers = string.Join(",", layersParameter.Layers.Select(x =>
            {
                var name = x.GetName();
                return char.ToLowerInvariant(name[0]) + name.Substring(1);
            }) ?? Array.Empty<string>());

            if (!string.IsNullOrWhiteSpace(layers))
            {
                query = query.Add("layers", layers);
            }
            else
            {
                _logger.RadarDebug(Resources.Services.RadarGeocoding.Invalid_Layers);
            }
        }

        /// <summary>
        /// Adds the Radar key to the request.
        /// </summary>
        /// <param name="keyParameter">An <see cref="IKeyParameters"/> to conditionally get the key from.</param>
        internal void AddRadarKey(IKeyParameters keyParameter)
        {
            var key = _options.Value.Key;

            if (!string.IsNullOrWhiteSpace(keyParameter.Key))
            {
                key = keyParameter.Key;
            }

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(key);
        }
    }
}
