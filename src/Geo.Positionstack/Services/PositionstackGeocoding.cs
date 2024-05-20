// <copyright file="PositionstackGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Services
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Core.Models.Exceptions;
    using Geo.Positionstack.Models;
    using Geo.Positionstack.Models.Parameters;
    using Geo.Positionstack.Models.Responses;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the Positionstack geocoding API.
    /// </summary>
    public class PositionstackGeocoding : GeoClient, IPositionstackGeocoding
    {
        private const string GeocodeUri = "https://api.positionstack.com/v1/forward";
        private const string ReverseGeocodeUri = "https://api.positionstack.com/v1/reverse";

        private readonly IOptions<KeyOptions<IPositionstackGeocoding>> _options;
        private readonly ILogger<PositionstackGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionstackGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Positionstack Geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="KeyOptions{T}"/> containing Positionstack key information.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public PositionstackGeocoding(
            HttpClient client,
            IOptions<KeyOptions<IPositionstackGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<PositionstackGeocoding>() ?? NullLogger<PositionstackGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "Positionstack";

        /// <inheritdoc/>
        public async Task<Response> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await GetAsync<Response>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await GetAsync<Response>(uri, cancellationToken).ConfigureAwait(false);
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
                _logger.PositionstackError(Resources.Services.PositionstackGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.PositionstackGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.PositionstackError(Resources.Services.PositionstackGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.PositionstackGeocoding.Failed_To_Create_Uri, ex);
            }
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Positionstack geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="GeocodingParameters.Query"/> parameter is null or invalid.</exception>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(GeocodeUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                _logger.PositionstackError(Resources.Services.PositionstackGeocoding.Invalid_Query);
                throw new ArgumentException(Resources.Services.PositionstackGeocoding.Invalid_Query, nameof(parameters.Query));
            }

            query = query.Add("query", parameters.Query);

            AddFilterParameters(parameters, ref query);
            AddLocationParameters(parameters, ref query);
            AddPositionstackKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Positionstack reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="ReverseGeocodingParameters.Coordinate"/> parameter is null or invalid.</exception>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(ReverseGeocodeUri);
            var query = QueryString.Empty;

            if (parameters.Coordinate is null)
            {
                _logger.PositionstackError(Resources.Services.PositionstackGeocoding.Invalid_Coordinates);
                throw new ArgumentException(Resources.Services.PositionstackGeocoding.Invalid_Coordinates, nameof(parameters.Coordinate));
            }

            query = query.Add("query", parameters.Coordinate.ToString());

            AddFilterParameters(parameters, ref query);
            AddLocationParameters(parameters, ref query);
            AddPositionstackKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the location information to the query.
        /// </summary>
        /// <param name="locationParameters">The <see cref="ILocationGeocodeParameters"/> used to get the location information from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddLocationParameters(ILocationGeocodeParameters locationParameters, ref QueryString query)
        {
            if (locationParameters.Countries?.Count > 0)
            {
                var countries = string.Join(",", locationParameters.Countries ?? Array.Empty<string>());
                query = query.Add("country", countries);
            }
            else
            {
                _logger.PositionstackWarning(Resources.Services.PositionstackGeocoding.Invalid_Country);
            }

            if (!string.IsNullOrWhiteSpace(locationParameters.Region))
            {
                query = query.Add("region", locationParameters.Region);
            }
            else
            {
                _logger.PositionstackDebug(Resources.Services.PositionstackGeocoding.Invalid_Region);
            }
        }

        /// <summary>
        /// Adds the filter information to the query.
        /// </summary>
        /// <param name="filterParameters">The <see cref="IFilterGeocodeParameters"/> used to get the filter information from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddFilterParameters(IFilterGeocodeParameters filterParameters, ref QueryString query)
        {
            if (!string.IsNullOrWhiteSpace(filterParameters.Language))
            {
                query = query.Add("language", filterParameters.Language);
            }
            else
            {
                _logger.PositionstackDebug(Resources.Services.PositionstackGeocoding.Invalid_Language);
            }

            if (filterParameters.CountryModule)
            {
                query = query.Add("country_module", "1");
            }
            else
            {
                _logger.PositionstackDebug(Resources.Services.PositionstackGeocoding.Invalid_Country_Module);
            }

            if (filterParameters.SunModule)
            {
                query = query.Add("sun_module", "1");
            }
            else
            {
                _logger.PositionstackDebug(Resources.Services.PositionstackGeocoding.Invalid_Sun_Module);
            }

            if (filterParameters.TimezoneModule)
            {
                query = query.Add("timezone_module", "1");
            }
            else
            {
                _logger.PositionstackDebug(Resources.Services.PositionstackGeocoding.Invalid_Timezone_Module);
            }

            if (filterParameters.BoundingBoxModule)
            {
                query = query.Add("bbox_module", "1");
            }
            else
            {
                _logger.PositionstackDebug(Resources.Services.PositionstackGeocoding.Invalid_BoundingBox_Module);
            }

            if (filterParameters.Limit > 0 && filterParameters.Limit <= 80)
            {
                query = query.Add("limit", filterParameters.Limit.ToString());
            }
            else
            {
                _logger.PositionstackWarning(Resources.Services.PositionstackGeocoding.Invalid_Limit);
            }

            if (filterParameters.Fields?.Count > 0)
            {
                var fields = string.Join(",", filterParameters.Fields.Where(x => !string.IsNullOrWhiteSpace(x)) ?? Array.Empty<string>());
                query = query.Add("fields", fields);
            }
            else
            {
                _logger.PositionstackWarning(Resources.Services.PositionstackGeocoding.Invalid_Fields);
            }
        }

        /// <summary>
        /// Adds the Positionstack key to the request.
        /// </summary>
        /// <param name="keyParameter">An <see cref="IKeyParameters"/> to conditionally get the key from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddPositionstackKey(IKeyParameters keyParameter, ref QueryString query)
        {
            var key = _options.Value.Key;

            if (!string.IsNullOrWhiteSpace(keyParameter.Key))
            {
                key = keyParameter.Key;
            }

            query = query.Add("access_key", key);
        }
    }
}
