// <copyright file="OpenRouteServiceGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Services
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
    using Geo.OpenRouteService.Models.Parameters;
    using Geo.OpenRouteService.Models.Responses;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the OpenRouteService geocoding API.
    /// </summary>
    public class OpenRouteServiceGeocoding : GeoClient, IOpenRouteServiceGeocoding
    {
        private const string SearchUri = "https://api.openrouteservice.org/geocode/search";
        private const string AutocompleteUri = "https://api.openrouteservice.org/geocode/autocomplete";
        private const string StructuredSearchUri = "https://api.openrouteservice.org/geocode/search/structured";
        private const string ReverseUri = "https://api.openrouteservice.org/geocode/reverse";

        private readonly IOptions<KeyOptions<IOpenRouteServiceGeocoding>> _options;
        private readonly ILogger<OpenRouteServiceGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouteServiceGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the OpenRouteService geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="KeyOptions{T}"/> containing the ORS API key.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public OpenRouteServiceGeocoding(
            HttpClient client,
            IOptions<KeyOptions<IOpenRouteServiceGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<OpenRouteServiceGeocoding>() ?? NullLogger<OpenRouteServiceGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "OpenRouteService Geocoding";

        /// <inheritdoc/>
        public async Task<FeatureCollection> SearchAsync(
            SearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<SearchParameters>(parameters, BuildSearchRequest);
            return await GetAsync<FeatureCollection>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<FeatureCollection> AutocompleteAsync(
            AutocompleteParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<AutocompleteParameters>(parameters, BuildAutocompleteRequest);
            return await GetAsync<FeatureCollection>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<FeatureCollection> StructuredSearchAsync(
            StructuredSearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<StructuredSearchParameters>(parameters, BuildStructuredSearchRequest);
            return await GetAsync<FeatureCollection>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<FeatureCollection> ReverseAsync(
            ReverseSearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseSearchParameters>(parameters, BuildReverseRequest);
            return await GetAsync<FeatureCollection>(uri, cancellationToken).ConfigureAwait(false);
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
                _logger.OpenRouteServiceError(Resources.Services.OpenRouteServiceGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.OpenRouteServiceGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.OpenRouteServiceError(Resources.Services.OpenRouteServiceGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.OpenRouteServiceGeocoding.Failed_To_Create_Uri, ex);
            }
        }

        /// <summary>
        /// Builds the forward geocoding search uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="SearchParameters"/> with the search parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed ORS search uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="SearchParameters.Text"/> parameter is null or empty.</exception>
        internal Uri BuildSearchRequest(SearchParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.Text))
            {
                _logger.OpenRouteServiceError(Resources.Services.OpenRouteServiceGeocoding.Invalid_Text);
                throw new ArgumentException(Resources.Services.OpenRouteServiceGeocoding.Invalid_Text, nameof(parameters.Text));
            }

            var uriBuilder = new UriBuilder(SearchUri);
            var query = QueryString.Empty;

            query = query.Add("text", parameters.Text);

            AddBaseParameters(parameters, ref query);
            AddOpenRouteServiceKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the autocomplete uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">An <see cref="AutocompleteParameters"/> with the autocomplete parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed ORS autocomplete uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="AutocompleteParameters.Text"/> parameter is null or empty.</exception>
        internal Uri BuildAutocompleteRequest(AutocompleteParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.Text))
            {
                _logger.OpenRouteServiceError(Resources.Services.OpenRouteServiceGeocoding.Invalid_Text);
                throw new ArgumentException(Resources.Services.OpenRouteServiceGeocoding.Invalid_Text, nameof(parameters.Text));
            }

            var uriBuilder = new UriBuilder(AutocompleteUri);
            var query = QueryString.Empty;

            query = query.Add("text", parameters.Text);

            AddBaseParameters(parameters, ref query);
            AddOpenRouteServiceKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the structured geocoding search uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="StructuredSearchParameters"/> with the address component parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed ORS structured search uri.</returns>
        /// <exception cref="ArgumentException">Thrown when no address component is provided.</exception>
        internal Uri BuildStructuredSearchRequest(StructuredSearchParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.Address) &&
                string.IsNullOrWhiteSpace(parameters.Neighbourhood) &&
                string.IsNullOrWhiteSpace(parameters.Borough) &&
                string.IsNullOrWhiteSpace(parameters.Locality) &&
                string.IsNullOrWhiteSpace(parameters.County) &&
                string.IsNullOrWhiteSpace(parameters.Region) &&
                string.IsNullOrWhiteSpace(parameters.PostalCode) &&
                string.IsNullOrWhiteSpace(parameters.Country))
            {
                _logger.OpenRouteServiceError(Resources.Services.OpenRouteServiceGeocoding.Invalid_Address_Components);
                throw new ArgumentException(Resources.Services.OpenRouteServiceGeocoding.Invalid_Address_Components, nameof(parameters.Address));
            }

            var uriBuilder = new UriBuilder(StructuredSearchUri);
            var query = QueryString.Empty;

            if (!string.IsNullOrWhiteSpace(parameters.Address))
            {
                query = query.Add("address", parameters.Address);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Neighbourhood))
            {
                query = query.Add("neighbourhood", parameters.Neighbourhood);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Borough))
            {
                query = query.Add("borough", parameters.Borough);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Locality))
            {
                query = query.Add("locality", parameters.Locality);
            }

            if (!string.IsNullOrWhiteSpace(parameters.County))
            {
                query = query.Add("county", parameters.County);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Region))
            {
                query = query.Add("region", parameters.Region);
            }

            if (!string.IsNullOrWhiteSpace(parameters.PostalCode))
            {
                query = query.Add("postalcode", parameters.PostalCode);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Country))
            {
                query = query.Add("country", parameters.Country);
            }

            AddBaseParameters(parameters, ref query);
            AddOpenRouteServiceKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseSearchParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed ORS reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="ReverseSearchParameters.Point"/> parameter is null.</exception>
        internal Uri BuildReverseRequest(ReverseSearchParameters parameters)
        {
            if (parameters.Point is null)
            {
                _logger.OpenRouteServiceError(Resources.Services.OpenRouteServiceGeocoding.Invalid_Point);
                throw new ArgumentException(Resources.Services.OpenRouteServiceGeocoding.Invalid_Point, nameof(parameters.Point));
            }

            var uriBuilder = new UriBuilder(ReverseUri);
            var query = QueryString.Empty;

            query = query.Add("point.lat", parameters.Point.Latitude.ToString(CultureInfo.InvariantCulture));
            query = query.Add("point.lon", parameters.Point.Longitude.ToString(CultureInfo.InvariantCulture));

            if (parameters.Size.HasValue)
            {
                query = query.Add("size", parameters.Size.Value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Size);
            }

            if (parameters.BoundaryCircleRadius.HasValue)
            {
                query = query.Add("boundary.circle.radius", parameters.BoundaryCircleRadius.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (parameters.BoundaryCountries.Count > 0)
            {
                query = query.Add("boundary.country", string.Join(",", parameters.BoundaryCountries));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Boundary_Countries);
            }

            if (!string.IsNullOrWhiteSpace(parameters.BoundaryGid))
            {
                query = query.Add("boundary.gid", parameters.BoundaryGid);
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Boundary_Gid);
            }

            if (parameters.Sources.Count > 0)
            {
                query = query.Add("sources", string.Join(",", parameters.Sources.Select(x => x.ToEnumString())));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Sources);
            }

            if (parameters.Layers.Count > 0)
            {
                query = query.Add("layers", string.Join(",", parameters.Layers.Select(x => x.ToEnumString())));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Layers);
            }

            AddOpenRouteServiceKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the shared optional parameters common to search, autocomplete, and structured search.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseSearchParameters"/> with the base parameters to add.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddBaseParameters(BaseSearchParameters parameters, ref QueryString query)
        {
            if (parameters.Size.HasValue)
            {
                query = query.Add("size", parameters.Size.Value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Size);
            }

            if (parameters.FocusPoint != null)
            {
                query = query.Add("focus.point.lat", parameters.FocusPoint.Latitude.ToString(CultureInfo.InvariantCulture));
                query = query.Add("focus.point.lon", parameters.FocusPoint.Longitude.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Focus_Point);
            }

            if (parameters.BoundaryRect != null)
            {
                query = query.Add("boundary.rect.min_lon", parameters.BoundaryRect.MinLongitude.ToString(CultureInfo.InvariantCulture));
                query = query.Add("boundary.rect.max_lon", parameters.BoundaryRect.MaxLongitude.ToString(CultureInfo.InvariantCulture));
                query = query.Add("boundary.rect.min_lat", parameters.BoundaryRect.MinLatitude.ToString(CultureInfo.InvariantCulture));
                query = query.Add("boundary.rect.max_lat", parameters.BoundaryRect.MaxLatitude.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Boundary_Rect);
            }

            if (parameters.BoundaryCircle != null)
            {
                query = query.Add("boundary.circle.lat", parameters.BoundaryCircle.Latitude.ToString(CultureInfo.InvariantCulture));
                query = query.Add("boundary.circle.lon", parameters.BoundaryCircle.Longitude.ToString(CultureInfo.InvariantCulture));

                if (parameters.BoundaryCircle.Radius.HasValue)
                {
                    query = query.Add("boundary.circle.radius", parameters.BoundaryCircle.Radius.Value.ToString(CultureInfo.InvariantCulture));
                }
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Boundary_Circle);
            }

            if (parameters.BoundaryCountries.Count > 0)
            {
                query = query.Add("boundary.country", string.Join(",", parameters.BoundaryCountries));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Boundary_Countries);
            }

            if (!string.IsNullOrWhiteSpace(parameters.BoundaryGid))
            {
                query = query.Add("boundary.gid", parameters.BoundaryGid);
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Boundary_Gid);
            }

            if (parameters.Sources.Count > 0)
            {
                query = query.Add("sources", string.Join(",", parameters.Sources.Select(x => x.ToEnumString())));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Sources);
            }

            if (parameters.Layers.Count > 0)
            {
                query = query.Add("layers", string.Join(",", parameters.Layers.Select(x => x.ToEnumString())));
            }
            else
            {
                _logger.OpenRouteServiceDebug(Resources.Services.OpenRouteServiceGeocoding.Invalid_Layers);
            }
        }

        /// <summary>
        /// Adds the ORS API key to the query parameters.
        /// </summary>
        /// <param name="keyParameter">An <see cref="IKeyParameters"/> to conditionally get the key from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddOpenRouteServiceKey(IKeyParameters keyParameter, ref QueryString query)
        {
            var key = _options.Value.Key;

            if (!string.IsNullOrWhiteSpace(keyParameter.Key))
            {
                key = keyParameter.Key;
            }

            query = query.Add("api_key", key);
        }
    }
}
