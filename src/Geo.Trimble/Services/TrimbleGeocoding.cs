// <copyright file="TrimbleGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Core.Models.Exceptions;
    using Geo.Trimble.Models.Enums;
    using Geo.Trimble.Models.Parameters;
    using Geo.Trimble.Models.Responses;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the Trimble Maps geocoding API.
    /// </summary>
    public class TrimbleGeocoding : GeoClient, ITrimbleGeocoding
    {
        private const string GeocodeUri = "https://pcmiler.alk.com/apis/rest/v1.0/service.svc/locations";
        private const string ReverseGeocodeUri = "https://pcmiler.alk.com/apis/rest/v1.0/service.svc/locations/reverse";

        private readonly IOptions<KeyOptions<ITrimbleGeocoding>> _options;
        private readonly ILogger<TrimbleGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrimbleGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Trimble Maps Geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="KeyOptions{T}"/> containing Trimble Maps key information.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public TrimbleGeocoding(
            HttpClient client,
            IOptions<KeyOptions<ITrimbleGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<TrimbleGeocoding>() ?? NullLogger<TrimbleGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "Trimble";

        /// <inheritdoc/>
        public async Task<IList<GeocodeResponse>> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await GetAsync<IList<GeocodeResponse>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<GeocodeResponse> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await GetAsync<GeocodeResponse>(uri, cancellationToken).ConfigureAwait(false);
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
                _logger.TrimbleError(Resources.Services.TrimbleGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.TrimbleGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.TrimbleError(Resources.Services.TrimbleGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.TrimbleGeocoding.Failed_To_Create_Uri, ex);
            }
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Trimble Maps geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when no address fields are provided.</exception>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(GeocodeUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Street) &&
                string.IsNullOrWhiteSpace(parameters.City) &&
                string.IsNullOrWhiteSpace(parameters.State) &&
                string.IsNullOrWhiteSpace(parameters.Zip) &&
                string.IsNullOrWhiteSpace(parameters.County) &&
                string.IsNullOrWhiteSpace(parameters.Country))
            {
                _logger.TrimbleError(Resources.Services.TrimbleGeocoding.Invalid_Address);
                throw new ArgumentException(Resources.Services.TrimbleGeocoding.Invalid_Address, nameof(parameters));
            }

            AddAddressParameters(parameters, ref query);
            AddTrimbleKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Trimble Maps reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="ReverseGeocodingParameters.Coordinate"/> parameter is null.</exception>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(ReverseGeocodeUri);
            var query = QueryString.Empty;

            if (parameters.Coordinate is null)
            {
                _logger.TrimbleError(Resources.Services.TrimbleGeocoding.Invalid_Coordinate);
                throw new ArgumentException(Resources.Services.TrimbleGeocoding.Invalid_Coordinate, nameof(parameters.Coordinate));
            }

            query = query.Add("Coords", parameters.Coordinate.ToString());

            AddReverseParameters(parameters, ref query);
            AddTrimbleKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the structured address fields to the query string.
        /// </summary>
        /// <param name="parameters">The <see cref="GeocodingParameters"/> to read address fields from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddAddressParameters(GeocodingParameters parameters, ref QueryString query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Street))
            {
                query = query.Add("Street", parameters.Street);
            }

            if (!string.IsNullOrWhiteSpace(parameters.City))
            {
                query = query.Add("City", parameters.City);
            }

            if (!string.IsNullOrWhiteSpace(parameters.State))
            {
                query = query.Add("State", parameters.State);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Zip))
            {
                query = query.Add("Zip", parameters.Zip);
            }

            if (!string.IsNullOrWhiteSpace(parameters.County))
            {
                query = query.Add("County", parameters.County);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Country))
            {
                query = query.Add("Country", parameters.Country);
            }

            query = query.Add("Region", ((int)parameters.Region).ToString(CultureInfo.InvariantCulture));

            if (!string.IsNullOrWhiteSpace(parameters.Dataset))
            {
                query = query.Add("Dataset", parameters.Dataset);
            }

            if (parameters.MaxResults.HasValue && parameters.MaxResults.Value > 0)
            {
                query = query.Add("MaxResults", parameters.MaxResults.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (parameters.MatchNamedRoadsOnly)
            {
                query = query.Add("MatchNamedRoadsOnly", "true");
            }
        }

        /// <summary>
        /// Adds the optional reverse geocoding parameters to the query string.
        /// </summary>
        /// <param name="parameters">The <see cref="ReverseGeocodingParameters"/> to read optional fields from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddReverseParameters(ReverseGeocodingParameters parameters, ref QueryString query)
        {
            query = query.Add("Region", ((int)parameters.Region).ToString(CultureInfo.InvariantCulture));

            if (!string.IsNullOrWhiteSpace(parameters.Dataset))
            {
                query = query.Add("Dataset", parameters.Dataset);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Lang))
            {
                query = query.Add("lang", parameters.Lang);
            }

            if (parameters.MatchNamedRoadsOnly)
            {
                query = query.Add("matchNamedRoadsOnly", "true");
            }

            if (parameters.MaxCleanupMiles.HasValue)
            {
                query = query.Add("maxCleanupMiles", parameters.MaxCleanupMiles.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (parameters.IncludePostedSpeedLimit)
            {
                query = query.Add("includePostedSpeedLimit", "true");
            }

            if (!string.IsNullOrWhiteSpace(parameters.VehicleType))
            {
                query = query.Add("vehicleType", parameters.VehicleType);
            }

            if (parameters.Heading.HasValue)
            {
                query = query.Add("heading", parameters.Heading.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (parameters.IncludeLinkInfo)
            {
                query = query.Add("includeLinkInfo", "true");
            }

            if (parameters.CountryAbbrevType.HasValue)
            {
                query = query.Add("countryAbbrevType", parameters.CountryAbbrevType.Value.ToString());
            }

            if (parameters.IncludeTrimblePlaceIds)
            {
                query = query.Add("includeTrimblePlaceIds", "true");
            }
        }

        /// <summary>
        /// Adds the Trimble Maps API key to the request as the <c>authToken</c> query parameter.
        /// </summary>
        /// <param name="keyParameter">An <see cref="IKeyParameters"/> to conditionally get the key from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddTrimbleKey(IKeyParameters keyParameter, ref QueryString query)
        {
            var key = _options.Value.Key;

            if (!string.IsNullOrWhiteSpace(keyParameter.Key))
            {
                key = keyParameter.Key;
            }

            query = query.Add("authToken", key);
        }
    }
}
