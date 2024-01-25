// <copyright file="BingGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Services
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Bing.Models.Parameters;
    using Geo.Bing.Models.Responses;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Core.Models.Exceptions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the Bing geocoding API.
    /// </summary>
    public class BingGeocoding : GeoClient, IBingGeocoding
    {
        private const string BaseUri = "http://dev.virtualearth.net/REST/v1/Locations";

        private readonly IOptions<KeyOptions<IBingGeocoding>> _options;
        private readonly ILogger<BingGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingGeocoding"/> class.
        /// </summary>
        /// <param name="client">An <see cref="HttpClient"/> used for placing calls to the Bing Geocoding API.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="KeyOptions{T}"/> containing Bing key information.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public BingGeocoding(
            HttpClient client,
            IOptions<KeyOptions<IBingGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<BingGeocoding>() ?? NullLogger<BingGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "Bing";

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

        /// <inheritdoc/>
        public async Task<Response> AddressGeocodingAsync(AddressGeocodingParameters parameters, CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<AddressGeocodingParameters>(parameters, BuildAddressGeocodingRequest);

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
                _logger.BingError(Resources.Services.BingGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.BingGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                _logger.BingError(Resources.Services.BingGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.BingGeocoding.Failed_To_Create_Uri, ex);
            }
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Bing geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Query' parameter is null or empty.</exception>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(BaseUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                _logger.BingError(Resources.Services.BingGeocoding.Invalid_Query);
                throw new ArgumentException(Resources.Services.BingGeocoding.Invalid_Query, nameof(parameters.Query));
            }

            query = query.Add("query", parameters.Query);

            BuildLimitedResultQuery(parameters, ref query);

            AddBingKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Bing reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Point' parameter is null or invalid.</exception>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters)
        {
            if (parameters.Point is null)
            {
                _logger.BingError(Resources.Services.BingGeocoding.Invalid_Point);
                throw new ArgumentException(Resources.Services.BingGeocoding.Invalid_Point, nameof(parameters.Point));
            }

            var uriBuilder = new UriBuilder(BaseUri + $"/{parameters.Point}");
            var query = QueryString.Empty;

            var includes = new CommaDelimitedStringCollection();
            if (parameters.IncludeAddress == true)
            {
                includes.Add("Address");
            }

            if (parameters.IncludeAddressNeighbourhood == true)
            {
                includes.Add("Neighborhood");
            }

            if (parameters.IncludePopulatedPlace == true)
            {
                includes.Add("PopulatedPlace");
            }

            if (parameters.IncludePostcode == true)
            {
                includes.Add("Postcode1");
            }

            if (parameters.IncludeAdministrationDivision1 == true)
            {
                includes.Add("AdminDivision1");
            }

            if (parameters.IncludeAdministrationDivision2 == true)
            {
                includes.Add("AdminDivision2");
            }

            if (parameters.IncludeCountryRegion == true)
            {
                includes.Add("CountryRegion");
            }

            if (includes.Count > 0)
            {
                query = query.Add("includeEntityTypes", includes.ToString());
            }
            else
            {
                _logger.BingDebug(Resources.Services.BingGeocoding.Do_Not_Include_Entity_Types);
            }

            BuildBaseQuery(parameters, ref query);

            AddBingKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the address geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="AddressGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Bing reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Point' parameter is null or invalid.</exception>
        internal Uri BuildAddressGeocodingRequest(AddressGeocodingParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.AdministrationDistrict) &&
                string.IsNullOrWhiteSpace(parameters.Locality) &&
                string.IsNullOrWhiteSpace(parameters.PostalCode) &&
                string.IsNullOrWhiteSpace(parameters.AddressLine) &&
                parameters.CountryRegion == null)
            {
                _logger.BingError(Resources.Services.BingGeocoding.Invalid_Address_Information);
                throw new ArgumentException(Resources.Services.BingGeocoding.Invalid_Address_Information, nameof(parameters));
            }

            var uriBuilder = new UriBuilder(BaseUri);
            var query = QueryString.Empty;

            if (!string.IsNullOrWhiteSpace(parameters.AdministrationDistrict))
            {
                query = query.Add("adminDistrict", parameters.AdministrationDistrict);
            }
            else
            {
                _logger.BingDebug(Resources.Services.BingGeocoding.Invalid_Administration_District);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Locality))
            {
                query = query.Add("locality", parameters.Locality);
            }
            else
            {
                _logger.BingDebug(Resources.Services.BingGeocoding.Invalid_Locality);
            }

            if (!string.IsNullOrWhiteSpace(parameters.PostalCode))
            {
                query = query.Add("postalCode", parameters.PostalCode);
            }
            else
            {
                _logger.BingDebug(Resources.Services.BingGeocoding.Invalid_Postal_Code);
            }

            if (!string.IsNullOrWhiteSpace(parameters.AddressLine))
            {
                query = query.Add("addressLine", parameters.AddressLine);
            }
            else
            {
                _logger.BingDebug(Resources.Services.BingGeocoding.Invalid_Address_Line);
            }

            if (parameters.CountryRegion != null)
            {
                query = query.Add("countryRegion", parameters.CountryRegion.TwoLetterISORegionName.ToUpperInvariant());
            }
            else
            {
                _logger.BingDebug(Resources.Services.BingGeocoding.Invalid_Country_Region);
            }

            BuildLimitedResultQuery(parameters, ref query);

            AddBingKey(parameters, ref query);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds up the limited result query parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ResultParameters"/> with the limited result parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the built up query parameters.</param>
        internal void BuildLimitedResultQuery(ResultParameters parameters, ref QueryString query)
        {
            if (parameters.MaximumResults > 0 && parameters.MaximumResults <= 20)
            {
                query = query.Add("maxResults", parameters.MaximumResults.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.BingWarning(Resources.Services.BingGeocoding.Invalid_Maximum_Results);
            }

            BuildBaseQuery(parameters, ref query);
        }

        /// <summary>
        /// Builds up the base query parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the built up query parameters.</param>
        internal void BuildBaseQuery(BaseParameters parameters, ref QueryString query)
        {
            if (parameters.IncludeNeighbourhood == true)
            {
                query = query.Add("includeNeighborhood", "1");
            }
            else
            {
                _logger.BingDebug(Resources.Services.BingGeocoding.Do_Not_Include_Neighbourhood);
            }

            var includes = new CommaDelimitedStringCollection();
            if (parameters.IncludeQueryParse == true)
            {
                includes.Add("queryParse");
            }

            if (parameters.IncludeCiso2 == true)
            {
                includes.Add("ciso2");
            }

            if (includes.Count > 0)
            {
                query = query.Add("include", includes.ToString());
            }
            else
            {
                _logger.BingDebug(Resources.Services.BingGeocoding.Do_Not_Include_Types);
            }

            if (parameters.Culture != null && !string.IsNullOrWhiteSpace(parameters.Culture.Name))
            {
                query = query.Add("culture", parameters.Culture.Name);
            }
            else
            {
                _logger.BingDebug(Resources.Services.BingGeocoding.Invalid_Culture);
            }
        }

        /// <summary>
        /// Adds the Bing key to the query parameters.
        /// </summary>
        /// <param name="keyParameter">An <see cref="IKeyParameters"/> to conditionally get the key from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddBingKey(IKeyParameters keyParameter, ref QueryString query)
        {
            var key = _options.Value.Key;

            if (!string.IsNullOrWhiteSpace(keyParameter.Key))
            {
                key = keyParameter.Key;
            }

            query = query.Add("key", key);
        }
    }
}
