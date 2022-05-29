// <copyright file="BingGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Services
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Geo.Bing.Abstractions;
    using Geo.Bing.Models.Exceptions;
    using Geo.Bing.Models.Parameters;
    using Geo.Bing.Models.Responses;
    using Geo.Core;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// A service to call the Bing geocoding API.
    /// </summary>
    public class BingGeocoding : ClientExecutor, IBingGeocoding
    {
        private const string ApiName = "Bing";
        private const string BaseUri = "http://dev.virtualearth.net/REST/v1/Locations";

        private readonly IBingKeyContainer _keyContainer;
        private readonly IStringLocalizer _localizer;
        private readonly ILogger<BingGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Bing Geocoding API.</param>
        /// <param name="keyContainer">A <see cref="IBingKeyContainer"/> used for fetching the Bing key.</param>
        /// <param name="localizerFactory">A <see cref="IStringLocalizerFactory"/> used to create a localizer for localizing log or exception messages.</param>
        /// <param name="logger">A <see cref="ILogger{T}"/> used for logging information.</param>
        public BingGeocoding(
            HttpClient client,
            IBingKeyContainer keyContainer,
            IStringLocalizerFactory localizerFactory,
            ILogger<BingGeocoding> logger = null)
            : base(client, localizerFactory)
        {
            _keyContainer = keyContainer ?? throw new ArgumentNullException(nameof(keyContainer));
            _localizer = localizerFactory?.Create(typeof(BingGeocoding)) ?? throw new ArgumentNullException(nameof(localizerFactory));
            _logger = logger ?? NullLogger<BingGeocoding>.Instance;
        }

        /// <inheritdoc/>
        public async Task<Response> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest);

            return await CallAsync<Response, BingException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest);

            return await CallAsync<Response, BingException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Response> AddressGeocodingAsync(AddressGeocodingParameters parameters, CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<AddressGeocodingParameters>(parameters, BuildAddressGeocodingRequest);

            return await CallAsync<Response, BingException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
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
                var error = _localizer["Null Parameters"];
                _logger.BingError(error);
                throw new BingException(error, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                var error = _localizer["Failed To Create Uri"];
                _logger.BingError(error);
                throw new BingException(error, ex);
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
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                var error = _localizer["Invalid Query"];
                _logger.BingError(error);
                throw new ArgumentException(error, nameof(parameters.Query));
            }

            query.Add("query", parameters.Query);

            BuildLimitedResultQuery(parameters, ref query);

            AddBingKey(query);

            uriBuilder.Query = query.ToString();

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
                var error = _localizer["Invalid Point"];
                _logger.BingError(error);
                throw new ArgumentException(error, nameof(parameters.Point));
            }

            var uriBuilder = new UriBuilder(BaseUri + $"/{parameters.Point}");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

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
                query.Add("includeEntityTypes", includes.ToString());
            }
            else
            {
                _logger.BingDebug(_localizer["Do Not Include Entity Types"]);
            }

            BuildBaseQuery(parameters, ref query);

            AddBingKey(query);

            uriBuilder.Query = query.ToString();

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
                var error = _localizer["Invalid Address Information"];
                _logger.BingError(error);
                throw new ArgumentException(error, nameof(parameters));
            }

            var uriBuilder = new UriBuilder(BaseUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (!string.IsNullOrWhiteSpace(parameters.AdministrationDistrict))
            {
                query.Add("adminDistrict", parameters.AdministrationDistrict);
            }
            else
            {
                _logger.BingDebug(_localizer["Invalid Administration District"]);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Locality))
            {
                query.Add("locality", parameters.Locality);
            }
            else
            {
                _logger.BingDebug(_localizer["Invalid Locality"]);
            }

            if (!string.IsNullOrWhiteSpace(parameters.PostalCode))
            {
                query.Add("postalCode", parameters.PostalCode);
            }
            else
            {
                _logger.BingDebug(_localizer["Invalid Postal Code"]);
            }

            if (!string.IsNullOrWhiteSpace(parameters.AddressLine))
            {
                query.Add("addressLine", parameters.AddressLine);
            }
            else
            {
                _logger.BingDebug(_localizer["Invalid Address Line"]);
            }

            if (parameters.CountryRegion != null)
            {
                query.Add("countryRegion", parameters.CountryRegion.TwoLetterISORegionName.ToUpperInvariant());
            }
            else
            {
                _logger.BingDebug(_localizer["Invalid Country Region"]);
            }

            BuildLimitedResultQuery(parameters, ref query);

            AddBingKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds up the limited result query parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ResultParameters"/> with the limited result parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the built up query parameters.</param>
        internal void BuildLimitedResultQuery(ResultParameters parameters, ref NameValueCollection query)
        {
            if (parameters.MaximumResults > 0 && parameters.MaximumResults <= 20)
            {
                query.Add("maxResults", parameters.MaximumResults.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.BingWarning(_localizer["Invalid Maximum Results"]);
            }

            BuildBaseQuery(parameters, ref query);
        }

        /// <summary>
        /// Builds up the base query parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the built up query parameters.</param>
        internal void BuildBaseQuery(BaseParameters parameters, ref NameValueCollection query)
        {
            if (parameters.IncludeNeighbourhood == true)
            {
                query.Add("includeNeighborhood", "1");
            }
            else
            {
                _logger.BingDebug(_localizer["Do Not Include Neighbourhood"]);
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
                query.Add("include", includes.ToString());
            }
            else
            {
                _logger.BingDebug(_localizer["Do Not Include Types"]);
            }
        }

        /// <summary>
        /// Adds the Bing key to the query parameters.
        /// </summary>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddBingKey(NameValueCollection query)
        {
            query.Add("key", _keyContainer.GetKey());
        }
    }
}
