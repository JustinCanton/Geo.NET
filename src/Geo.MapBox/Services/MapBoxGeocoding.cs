// <copyright file="MapBoxGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Encodings.Web;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Core.Models.Exceptions;
    using Geo.MapBox.Enums;
    using Geo.MapBox.Models;
    using Geo.MapBox.Models.Parameters;
    using Geo.MapBox.Models.Responses;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the MapBox geocoding API.
    /// </summary>
    public class MapBoxGeocoding : GeoClient, IMapBoxGeocoding
    {
        private const string GeocodeUri = "https://api.mapbox.com/geocoding/v5/{0}/{1}.json";
        private const string ReverseGeocodeUri = "https://api.mapbox.com/geocoding/v5/{0}/{1}.json";
        private const string PlacesEndpoint = "mapbox.places";
        private const string PermanentEndpoint = "mapbox.places-permanent";
        private const string GeocodeV6Uri = "https://api.mapbox.com/search/geocode/v6/forward";
        private const string ReverseGeocodeV6Uri = "https://api.mapbox.com/search/geocode/v6/reverse";

        private readonly IOptions<KeyOptions<IMapBoxGeocoding>> _options;
        private readonly ILogger<MapBoxGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapBoxGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the here Geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="KeyOptions{T}"/> containing MapBox key information.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public MapBoxGeocoding(
            HttpClient client,
            IOptions<KeyOptions<IMapBoxGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<MapBoxGeocoding>() ?? NullLogger<MapBoxGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "MapBox";

        /// <inheritdoc/>
        public async Task<Response<List<string>>> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await GetAsync<Response<List<string>>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response<Coordinate>> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await GetAsync<Response<Coordinate>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response<List<string>>> GeocodingV6Async(
            GeocodingV6Parameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingV6Parameters>(parameters, BuildGeocodingV6Request);

            return await GetAsync<Response<List<string>>>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response<Coordinate>> ReverseGeocodingV6Async(
            ReverseGeocodingV6Parameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingV6Parameters>(parameters, BuildReverseGeocodingV6Request);

            return await GetAsync<Response<Coordinate>>(uri, cancellationToken).ConfigureAwait(false);
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
                _logger.MapBoxError(Resources.Services.MapBoxGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.MapBoxGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.MapBoxError(Resources.Services.MapBoxGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.MapBoxGeocoding.Failed_To_Create_Uri, ex);
            }
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
                _logger.MapBoxError(Resources.Services.MapBoxGeocoding.Invalid_Query);
                throw new ArgumentException(Resources.Services.MapBoxGeocoding.Invalid_Query, nameof(parameters.Query));
            }

            var uriBuilder = new UriBuilder(string.Format(CultureInfo.InvariantCulture, GeocodeUri, parameters.EndpointType == EndpointType.Places ? PlacesEndpoint : PermanentEndpoint, UrlEncoder.Default.Encode(parameters.Query)));
            var query = QueryString.Empty;

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("autocomplete", parameters.ReturnAutocomplete.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (parameters.BoundingBox != null)
            {
                query = query.Add("bbox", parameters.BoundingBox.ToString());
            }
            else
            {
                _logger.MapBoxDebug(Resources.Services.MapBoxGeocoding.Invalid_Bounding_Box);
            }

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("fuzzyMatch", parameters.FuzzyMatch.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (parameters.Proximity != null)
            {
                query = query.Add("proximity", parameters.Proximity.ToString());
            }
            else
            {
                _logger.MapBoxDebug(Resources.Services.MapBoxGeocoding.Invalid_Proximity);
            }

            AddBaseParameters(parameters, ref query);

            AddMapBoxKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

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
                _logger.MapBoxError(Resources.Services.MapBoxGeocoding.Invalid_Coordinate);
                throw new ArgumentException(Resources.Services.MapBoxGeocoding.Invalid_Coordinate, nameof(parameters.Coordinate));
            }

            var uriBuilder = new UriBuilder(string.Format(CultureInfo.InvariantCulture, ReverseGeocodeUri, parameters.EndpointType == EndpointType.Places ? PlacesEndpoint : PermanentEndpoint, parameters.Coordinate));
            var query = QueryString.Empty;

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("reverseMode", parameters.ReverseMode.ToString().ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            AddBaseParameters(parameters, ref query);

            AddMapBoxKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the Geocoding API v6 forward geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingV6Parameters"/> with the v6 geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed MapBox v6 geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Query' parameter is null or invalid.</exception>
        internal Uri BuildGeocodingV6Request(GeocodingV6Parameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                _logger.MapBoxError(Resources.Services.MapBoxGeocoding.Invalid_Query);
                throw new ArgumentException(Resources.Services.MapBoxGeocoding.Invalid_Query, nameof(parameters.Query));
            }

            var uriBuilder = new UriBuilder(GeocodeV6Uri);
            var query = QueryString.Empty;

            query = query.Add("q", parameters.Query);

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("permanent", parameters.Permanent.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (parameters.Autocomplete.HasValue)
            {
#pragma warning disable CA1308 // Normalize strings to uppercase
                query = query.Add("autocomplete", parameters.Autocomplete.Value.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase
            }

            if (parameters.BoundingBox != null)
            {
                query = query.Add("bbox", parameters.BoundingBox.ToString());
            }

            if (!string.IsNullOrWhiteSpace(parameters.Format))
            {
                query = query.Add("format", parameters.Format);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Proximity))
            {
                query = query.Add("proximity", parameters.Proximity);
            }

            if (parameters.Entrances.HasValue)
            {
#pragma warning disable CA1308 // Normalize strings to uppercase
                query = query.Add("entrances", parameters.Entrances.Value.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase
            }

            AddBaseV6Parameters(parameters.Countries, parameters.Languages, parameters.Limit, parameters.Types, parameters.Worldview, ref query);

            AddMapBoxKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the Geocoding API v6 reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingV6Parameters"/> with the v6 reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed MapBox v6 reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Coordinate' parameter is null or invalid.</exception>
        internal Uri BuildReverseGeocodingV6Request(ReverseGeocodingV6Parameters parameters)
        {
            if (parameters.Coordinate is null)
            {
                _logger.MapBoxError(Resources.Services.MapBoxGeocoding.Invalid_Coordinate);
                throw new ArgumentException(Resources.Services.MapBoxGeocoding.Invalid_Coordinate, nameof(parameters.Coordinate));
            }

            var uriBuilder = new UriBuilder(ReverseGeocodeV6Uri);
            var query = QueryString.Empty;

            query = query.Add("longitude", parameters.Coordinate.Longitude.ToString(CultureInfo.InvariantCulture));
            query = query.Add("latitude", parameters.Coordinate.Latitude.ToString(CultureInfo.InvariantCulture));

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("permanent", parameters.Permanent.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            AddBaseV6Parameters(parameters.Countries, parameters.Languages, parameters.Limit, parameters.Types, parameters.Worldview, ref query);

            AddMapBoxKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds shared v6 query parameters (countries, languages, limit, types, worldview).
        /// </summary>
        internal void AddBaseV6Parameters(
            IList<RegionInfo> countries,
            IList<CultureInfo> languages,
            uint? limit,
            IList<FeatureType> types,
            string worldview,
            ref QueryString query)
        {
            if (countries != null && countries.Count > 0)
            {
                query = query.Add("country", string.Join(",", countries.Select(x => x.TwoLetterISORegionName)));
            }

            if (languages != null && languages.Count > 0)
            {
                query = query.Add("language", string.Join(",", languages.Select(x => x.Name)));
            }

            if (limit.HasValue && limit.Value > 0)
            {
                query = query.Add("limit", limit.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (types != null && types.Count > 0)
            {
#pragma warning disable CA1308 // Normalize strings to uppercase
                query = query.Add("types", string.Join(",", types.Select(x => x.ToString().ToLowerInvariant())));
#pragma warning restore CA1308 // Normalize strings to uppercase
            }

            if (!string.IsNullOrWhiteSpace(worldview))
            {
                query = query.Add("worldview", worldview);
            }
        }

        /// <summary>
        /// Adds the base query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddBaseParameters(BaseParameters parameters, ref QueryString query)
        {
            if (parameters.Countries.Count > 0)
            {
                query = query.Add("country", string.Join(",", parameters.Countries.Select(x => x.TwoLetterISORegionName)));
            }
            else
            {
                _logger.MapBoxDebug(Resources.Services.MapBoxGeocoding.Invalid_Countries);
            }

            if (parameters.Languages.Count > 0)
            {
                query = query.Add("language", string.Join(",", parameters.Languages.Select(x => x.Name)));
            }
            else
            {
                _logger.MapBoxDebug(Resources.Services.MapBoxGeocoding.Invalid_Languages);
            }

            if (parameters.Limit > 0 && parameters.Limit < 6)
            {
                query = query.Add("limit", parameters.Limit.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.MapBoxDebug(Resources.Services.MapBoxGeocoding.Invalid_Limit);
            }

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("routing", parameters.Routing.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (parameters.Types != null && parameters.Types.Count > 0)
            {
#pragma warning disable CA1308 // Normalize strings to uppercase
                query = query.Add("types", string.Join(",", parameters.Types.Select(x => x.ToString().ToLowerInvariant())));
#pragma warning restore CA1308 // Normalize strings to uppercase
            }
            else
            {
                _logger.MapBoxDebug(Resources.Services.MapBoxGeocoding.Invalid_Types);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Worldview))
            {
                query = query.Add("worldview", parameters.Worldview);
            }
            else
            {
                _logger.MapBoxDebug(Resources.Services.MapBoxGeocoding.Invalid_Worldview);
            }
        }

        /// <summary>
        /// Adds the MapBox key to the query parameters.
        /// </summary>
        /// <param name="keyParameter">An <see cref="IKeyParameters"/> to conditionally get the key from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddMapBoxKey(IKeyParameters keyParameter, ref QueryString query)
        {
            var key = _options.Value.Key;

            if (!string.IsNullOrWhiteSpace(keyParameter.Key))
            {
                key = keyParameter.Key;
            }

            query = query.Add("access_token", key);
        }
    }
}
