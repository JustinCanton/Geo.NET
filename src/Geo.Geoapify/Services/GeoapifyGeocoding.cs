// <copyright file="GeoapifyGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Services
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
    using Geo.Geoapify.Models.Parameters;
    using Geo.Geoapify.Models.Responses;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the Geoapify geocoding API.
    /// </summary>
    public class GeoapifyGeocoding : GeoClient, IGeoapifyGeocoding
    {
        private const string GeocodeUri = "https://api.geoapify.com/v1/geocode/search";
        private const string ReverseGeocodeUri = "https://api.geoapify.com/v1/geocode/reverse";
        private const string AutocompleteUri = "https://api.geoapify.com/v1/geocode/autocomplete";

        private readonly IOptions<KeyOptions<IGeoapifyGeocoding>> _options;
        private readonly ILogger<GeoapifyGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoapifyGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Geoapify geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="KeyOptions{T}"/> containing the Geoapify API key.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public GeoapifyGeocoding(
            HttpClient client,
            IOptions<KeyOptions<IGeoapifyGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<GeoapifyGeocoding>() ?? NullLogger<GeoapifyGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "Geoapify";

        /// <inheritdoc/>
        public async Task<FeatureCollection> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await GetAsync<FeatureCollection>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<FeatureCollection> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

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
                _logger.GeoapifyError(Resources.Services.GeoapifyGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.GeoapifyGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.GeoapifyError(Resources.Services.GeoapifyGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.GeoapifyGeocoding.Failed_To_Create_Uri, ex);
            }
        }

        /// <summary>
        /// Builds the forward geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Geoapify geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when neither the text nor any address component is provided.</exception>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.Text) &&
                string.IsNullOrWhiteSpace(parameters.Name) &&
                string.IsNullOrWhiteSpace(parameters.HouseNumber) &&
                string.IsNullOrWhiteSpace(parameters.Street) &&
                string.IsNullOrWhiteSpace(parameters.PostCode) &&
                string.IsNullOrWhiteSpace(parameters.City) &&
                string.IsNullOrWhiteSpace(parameters.State) &&
                string.IsNullOrWhiteSpace(parameters.Country))
            {
                _logger.GeoapifyError(Resources.Services.GeoapifyGeocoding.Invalid_Address);
                throw new ArgumentException(Resources.Services.GeoapifyGeocoding.Invalid_Address, nameof(parameters.Text));
            }

            var uriBuilder = new UriBuilder(GeocodeUri);
            var query = QueryString.Empty;

            if (!string.IsNullOrWhiteSpace(parameters.Text))
            {
                query = query.Add("text", parameters.Text);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Name))
            {
                query = query.Add("name", parameters.Name);
            }

            if (!string.IsNullOrWhiteSpace(parameters.HouseNumber))
            {
                query = query.Add("housenumber", parameters.HouseNumber);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Street))
            {
                query = query.Add("street", parameters.Street);
            }

            if (!string.IsNullOrWhiteSpace(parameters.PostCode))
            {
                query = query.Add("postcode", parameters.PostCode);
            }

            if (!string.IsNullOrWhiteSpace(parameters.City))
            {
                query = query.Add("city", parameters.City);
            }

            if (!string.IsNullOrWhiteSpace(parameters.State))
            {
                query = query.Add("state", parameters.State);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Country))
            {
                query = query.Add("country", parameters.Country);
            }

            AddBaseParameters(parameters, ref query);
            AddGeoapifyKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Geoapify reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="ReverseGeocodingParameters.Coordinate"/> parameter is null.</exception>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters)
        {
            if (parameters.Coordinate is null)
            {
                _logger.GeoapifyError(Resources.Services.GeoapifyGeocoding.Invalid_Coordinate);
                throw new ArgumentException(Resources.Services.GeoapifyGeocoding.Invalid_Coordinate, nameof(parameters.Coordinate));
            }

            var uriBuilder = new UriBuilder(ReverseGeocodeUri);
            var query = QueryString.Empty;

            query = query.Add("lat", parameters.Coordinate.Latitude.ToString(CultureInfo.InvariantCulture));
            query = query.Add("lon", parameters.Coordinate.Longitude.ToString(CultureInfo.InvariantCulture));

            if (parameters.Type.HasValue)
            {
                query = query.Add("type", parameters.Type.Value.ToEnumString());
            }
            else
            {
                _logger.GeoapifyDebug(Resources.Services.GeoapifyGeocoding.Invalid_Type);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Language))
            {
                query = query.Add("lang", parameters.Language);
            }
            else
            {
                _logger.GeoapifyDebug(Resources.Services.GeoapifyGeocoding.Invalid_Language);
            }

            if (parameters.Limit.HasValue)
            {
                query = query.Add("limit", parameters.Limit.Value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.GeoapifyDebug(Resources.Services.GeoapifyGeocoding.Invalid_Limit);
            }

            AddGeoapifyKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the address autocomplete uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">An <see cref="AutocompleteParameters"/> with the autocomplete parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Geoapify autocomplete uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the <see cref="AutocompleteParameters.Text"/> parameter is null or empty.</exception>
        internal Uri BuildAutocompleteRequest(AutocompleteParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.Text))
            {
                _logger.GeoapifyError(Resources.Services.GeoapifyGeocoding.Invalid_Text);
                throw new ArgumentException(Resources.Services.GeoapifyGeocoding.Invalid_Text, nameof(parameters.Text));
            }

            var uriBuilder = new UriBuilder(AutocompleteUri);
            var query = QueryString.Empty;

            query = query.Add("text", parameters.Text);

            AddBaseParameters(parameters, ref query);
            AddGeoapifyKey(parameters, ref query);
            query = query.AddAdditionalParameters(parameters);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the parameters shared by the forward geocoding and autocomplete endpoints to the query string.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseSearchParameters"/> with the base parameters to add.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddBaseParameters(BaseSearchParameters parameters, ref QueryString query)
        {
            if (parameters.Type.HasValue)
            {
                query = query.Add("type", parameters.Type.Value.ToEnumString());
            }
            else
            {
                _logger.GeoapifyDebug(Resources.Services.GeoapifyGeocoding.Invalid_Type);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Language))
            {
                query = query.Add("lang", parameters.Language);
            }
            else
            {
                _logger.GeoapifyDebug(Resources.Services.GeoapifyGeocoding.Invalid_Language);
            }

            if (parameters.Limit.HasValue)
            {
                query = query.Add("limit", parameters.Limit.Value.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.GeoapifyDebug(Resources.Services.GeoapifyGeocoding.Invalid_Limit);
            }

            if (parameters.Filters.Count > 0)
            {
                query = query.Add("filter", string.Join("|", parameters.Filters.Select(x => x.ToString())));
            }
            else
            {
                _logger.GeoapifyDebug(Resources.Services.GeoapifyGeocoding.Invalid_Filters);
            }

            if (parameters.Biases.Count > 0)
            {
                query = query.Add("bias", string.Join("|", parameters.Biases.Select(x => x.ToString())));
            }
            else
            {
                _logger.GeoapifyDebug(Resources.Services.GeoapifyGeocoding.Invalid_Biases);
            }
        }

        /// <summary>
        /// Adds the Geoapify API key to the request as the <c>apiKey</c> query parameter.
        /// </summary>
        /// <param name="keyParameter">An <see cref="IKeyParameters"/> to conditionally get the key from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddGeoapifyKey(IKeyParameters keyParameter, ref QueryString query)
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
