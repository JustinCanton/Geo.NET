// <copyright file="NominatimGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Services
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Core.Models.Exceptions;
    using Geo.Nominatim.Enums;
    using Geo.Nominatim.Models.Parameters;
    using Geo.Nominatim.Models.Responses;
    using Geo.Nominatim.Settings;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the Nominatim geocoding API.
    /// </summary>
    public class NominatimGeocoding : GeoClient, INominatimGeocoding
    {
        private const string SearchUri = "https://nominatim.openstreetmap.org/search";
        private const string ReverseUri = "https://nominatim.openstreetmap.org/reverse";
        private const string LookupUri = "https://nominatim.openstreetmap.org/lookup";

        private readonly IOptions<NominatimOptions> _options;
        private readonly ILogger<NominatimGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NominatimGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Nominatim Geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="NominatimOptions"/> containing Nominatim information.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public NominatimGeocoding(
            HttpClient client,
            IOptions<NominatimOptions> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<NominatimGeocoding>() ?? NullLogger<NominatimGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "Nominatim";

        /// <inheritdoc/>
        public async Task<SearchResponse> SearchAsync(
            SearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri(parameters, BuildSearchRequest);

            return await GetAsync<SearchResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ReverseGeocodingResponse> ReverseGeocodeAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri(parameters, BuildReverseGeocodeRequest);

            return await GetAsync<ReverseGeocodingResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<LookupResponse> LookupAsync(
            LookupParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri(parameters, BuildLookupRequest);

            return await GetAsync<LookupResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds the base query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="IBaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal static void AddBaseParameters(IBaseParameters parameters, ref QueryString query)
        {
            query = query.Add("format", "jsonv2");

            query = query.Add("addressdetails", (parameters.AddressDetails ?? false) ? "1" : "0");
            query = query.Add("extratags", (parameters.ExtraInfo ?? false) ? "1" : "0");
            query = query.Add("namedetails", (parameters.NameDetails ?? false) ? "1" : "0");

            if (!string.IsNullOrWhiteSpace(parameters.AcceptLanguage))
            {
                query = query.Add("accept-language", parameters.AcceptLanguage);
            }
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
                _logger.NominatimError(Resources.Services.NominatimGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.NominatimGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.NominatimError(Resources.Services.NominatimGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.NominatimGeocoding.Failed_To_Create_Uri, ex);
            }
        }

        /// <summary>
        /// Builds the search request URI for the Nominatim API using the specified parameters.
        /// </summary>
        /// <param name="parameters">
        /// A <see cref="SearchParameters"/> instance containing the search query, structured address fields, and other optional parameters
        /// for the search request.
        /// </param>
        /// <returns>
        /// A <see cref="Uri"/> representing the fully constructed search request to the Nominatim API.
        /// </returns>
        /// <remarks>
        /// This method constructs the query string by adding either a free-form query or structured address fields, as well as any optional
        /// limit, country codes, feature type, excluded place IDs, view box, bounding, layer, polygon, base, and email parameters.
        /// The resulting URI can be used to perform a search lookup.
        /// </remarks>
        internal Uri BuildSearchRequest(SearchParameters parameters)
        {
            var uriBuilder = new UriBuilder(SearchUri);
            var query = QueryString.Empty;

            if (!string.IsNullOrWhiteSpace(parameters.Query) &&
                (
                    !string.IsNullOrWhiteSpace(parameters.Amenity) ||
                    !string.IsNullOrWhiteSpace(parameters.Street) ||
                    !string.IsNullOrWhiteSpace(parameters.City) ||
                    !string.IsNullOrWhiteSpace(parameters.County) ||
                    !string.IsNullOrWhiteSpace(parameters.State) ||
                    !string.IsNullOrWhiteSpace(parameters.PostalCode) ||
                    !string.IsNullOrWhiteSpace(parameters.Country)))
            {
                _logger.NominatimError(Resources.Services.NominatimGeocoding.Invalid_Query);
                throw new ArgumentException(Resources.Services.NominatimGeocoding.Invalid_Query, nameof(parameters.Query));
            }

            if (string.IsNullOrWhiteSpace(parameters.Query) &&
                (
                    string.IsNullOrWhiteSpace(parameters.Amenity) &&
                    string.IsNullOrWhiteSpace(parameters.Street) &&
                    string.IsNullOrWhiteSpace(parameters.City) &&
                    string.IsNullOrWhiteSpace(parameters.County) &&
                    string.IsNullOrWhiteSpace(parameters.State) &&
                    string.IsNullOrWhiteSpace(parameters.PostalCode) &&
                    string.IsNullOrWhiteSpace(parameters.Country)))
            {
                _logger.NominatimError(Resources.Services.NominatimGeocoding.Invalid_Query);
                throw new ArgumentException(Resources.Services.NominatimGeocoding.Invalid_Query, nameof(parameters.Query));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Query))
            {
                query = query.Add("q", parameters.Query);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(parameters.Amenity))
                {
                    query = query.Add("amenity", parameters.Amenity);
                }

                if (!string.IsNullOrWhiteSpace(parameters.Street))
                {
                    query = query.Add("street", parameters.Street);
                }

                if (!string.IsNullOrWhiteSpace(parameters.City))
                {
                    query = query.Add("city", parameters.City);
                }

                if (!string.IsNullOrWhiteSpace(parameters.County))
                {
                    query = query.Add("county", parameters.County);
                }

                if (!string.IsNullOrWhiteSpace(parameters.State))
                {
                    query = query.Add("state", parameters.State);
                }

                if (!string.IsNullOrWhiteSpace(parameters.PostalCode))
                {
                    query = query.Add("postalcode", parameters.PostalCode);
                }

                if (!string.IsNullOrWhiteSpace(parameters.Country))
                {
                    query = query.Add("country", parameters.Country);
                }
            }

            if (parameters.Limit > 0 && parameters.Limit <= 40)
            {
                query = query.Add("limit", parameters.Limit.ToString());
            }
            else
            {
                _logger.NominatimWarning(Resources.Services.NominatimGeocoding.Invalid_Limit);
            }

            if (parameters.CountryCodes.Count > 0)
            {
                query = query.Add("countrycodes", string.Join(",", parameters.CountryCodes));
            }

            if (parameters.FeatureType != FeatureType.None)
            {
                query = query.Add("featureType", parameters.FeatureType.ToEnumString().ToLowerInvariant());
            }

            if (parameters.ExcludePlaceIds.Count > 0)
            {
                query = query.Add("exclude_place_ids", string.Join(",", parameters.ExcludePlaceIds));
            }

            if (parameters.ViewBox != null)
            {
                query = query.Add("viewbox", parameters.ViewBox.ToString());
            }

            query = query.Add("bounded", (parameters.Bounded ?? false) ? "1" : "0");

            AddLayerParameter(parameters, ref query);
            AddPolygonParameters(parameters, ref query);
            AddBaseParameters(parameters, ref query);
            AddEmail(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding request URI for the Nominatim API using the specified parameters.
        /// </summary>
        /// <param name="parameters">
        /// A <see cref="ReverseGeocodingParameters"/> instance containing the latitude, longitude, zoom level, and other optional parameters
        /// for the reverse geocoding request.
        /// </param>
        /// <returns>
        /// A <see cref="Uri"/> representing the fully constructed reverse geocoding request to the Nominatim API.
        /// </returns>
        /// <remarks>
        /// This method constructs the query string by adding latitude, longitude, and zoom parameters, as well as any optional
        /// layer, polygon, base, and email parameters. The resulting URI can be used to perform a reverse geocoding lookup.
        /// </remarks>
        internal Uri BuildReverseGeocodeRequest(ReverseGeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(ReverseUri);
            var query = QueryString.Empty;

            query = query.Add("lat", parameters.Latitude.ToString(CultureInfo.InvariantCulture));
            query = query.Add("lon", parameters.Longitude.ToString(CultureInfo.InvariantCulture));

            if (parameters.Zoom.HasValue)
            {
                query = query.Add("zoom", parameters.Zoom.Value.ToString(CultureInfo.InvariantCulture));
            }

            AddLayerParameter(parameters, ref query);
            AddPolygonParameters(parameters, ref query);
            AddBaseParameters(parameters, ref query);
            AddEmail(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the lookup uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="LookupParameters"/> with the lookup parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Nominatim lookup uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Location' parameter is null or invalid.</exception>
        internal Uri BuildLookupRequest(LookupParameters parameters)
        {
            var uriBuilder = new UriBuilder(LookupUri);
            var query = QueryString.Empty;

            if (parameters.OsmIds.Count > 50 || parameters.OsmIds.Count < 1)
            {
                _logger.NominatimError(Resources.Services.NominatimGeocoding.Invalid_OsmIds);
                throw new ArgumentException(Resources.Services.NominatimGeocoding.Invalid_OsmIds, nameof(parameters.OsmIds));
            }

            query = query.Add("osm_ids", string.Join(",", parameters.OsmIds));

            AddPolygonParameters(parameters, ref query);
            AddBaseParameters(parameters, ref query);
            AddEmail(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the "layer" parameter to the query string for Nominatim requests.
        /// </summary>
        /// <param name="parameters">
        /// An <see cref="ILayerParameter"/> instance containing the layers to include in the request.
        /// If the <c>Layers</c> collection is empty, no "layer" parameter will be added.
        /// </param>
        /// <param name="query">
        /// A <see cref="QueryString"/> containing the current query parameters.
        /// The "layer" parameter will be added if applicable.
        /// </param>
        internal void AddLayerParameter(ILayerParameter parameters, ref QueryString query)
        {
            if (parameters.Layers.Count == 0)
            {
                return;
            }

            query = query.Add("layer", string.Join(",", parameters.Layers.Select(x => x.ToEnumString().ToLowerInvariant())));
        }

        /// <summary>
        /// Adds polygon output parameters to the query string for Nominatim requests.
        /// </summary>
        /// <param name="parameters">
        /// An <see cref="IPolygonParameters"/> instance containing the polygon output format and threshold.
        /// The <c>PolygonOutput</c> property determines which polygon format (GeoJSON, KML, SVG, WKT) to request.
        /// The <c>PolygonThreshold</c> property specifies the allowed deviation for the returned geometry.
        /// </param>
        /// <param name="query">
        /// A <see cref="QueryString"/> containing the current query parameters.
        /// The method will add the appropriate polygon output and threshold parameters if applicable.
        /// </param>
        internal void AddPolygonParameters(IPolygonParameters parameters, ref QueryString query)
        {
            switch (parameters.PolygonOutput)
            {
                case PolygonOutput.GeoJSON:
                    query = query.Add("polygon_geojson", "1");
                    break;
                case PolygonOutput.KML:
                    query = query.Add("polygon_kml", "1");
                    break;
                case PolygonOutput.SVG:
                    query = query.Add("polygon_svg", "1");
                    break;
                case PolygonOutput.WKT:
                    query = query.Add("polygon_text", "1");
                    break;
                default:
                    _logger.NominatimDebug(Resources.Services.NominatimGeocoding.No_Polygon_Output);
                    break;
            }

            if (parameters.PolygonOutput != PolygonOutput.None)
            {
                query = query.Add("polygon_threshold", parameters.PolygonThreshold.ToString(CultureInfo.InvariantCulture));
            }
            else if (parameters.PolygonThreshold != 0)
            {
                _logger.NominatimWarning(Resources.Services.NominatimGeocoding.Invalid_Polygon_Threshold);
            }
        }

        /// <summary>
        /// Adds the email address to the query parameters for Nominatim API requests.
        /// The email is used to identify the requester, as recommended by Nominatim for responsible use.
        /// If the <paramref name="baseParameters"/> contains a non-empty <c>Email</c> property, it will be used;
        /// otherwise, the email from the configured <see cref="NominatimOptions"/> will be used.
        /// If neither is set, the email parameter is not added to the query.
        /// </summary>
        /// <param name="baseParameters">A <see cref="IBaseParameters"/> instance containing request parameters, possibly including an email address.</param>
        /// <param name="query">A <see cref="QueryString"/> containing the current query parameters. The email parameter will be added or updated.</param>
        internal void AddEmail(IBaseParameters baseParameters, ref QueryString query)
        {
            var email = _options.Value.Email;

            if (!string.IsNullOrWhiteSpace(baseParameters.Email))
            {
                email = baseParameters.Email;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            query = query.Add("email", email);
        }
    }
}
