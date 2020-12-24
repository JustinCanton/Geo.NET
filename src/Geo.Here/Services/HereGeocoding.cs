// <copyright file="HereGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Services
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Geo.Core;
    using Geo.Here.Abstractions;
    using Geo.Here.Models.Exceptions;
    using Geo.Here.Models.Parameters;
    using Geo.Here.Models.Responses;

    /// <summary>
    /// A service to call the HERE geocoding API.
    /// </summary>
    public class HereGeocoding : ClientExecutor, IHereGeocoding
    {
        private const string _apiName = "Here";
        private readonly string _geocodeUri = "https://geocode.search.hereapi.com/v1/geocode";
        private readonly string _reverseGeocodeUri = "https://revgeocode.search.hereapi.com/v1/revgeocode";
        private readonly string _discoverUri = "https://discover.search.hereapi.com/v1/discover";
        private readonly string _autosuggestUri = "https://autosuggest.search.hereapi.com/v1/autosuggest";
        private readonly string _browseUri = "https://browse.search.hereapi.com/v1/browse";
        private readonly string _lookupUri = "https://lookup.search.hereapi.com/v1/lookup";
        private readonly IHereKeyContainer _keyContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HereGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the HERE Geocoding API.</param>
        /// <param name="keyContainer">A <see cref="IHereKeyContainer"/> used for fetching the HERE key.</param>
        public HereGeocoding(
            HttpClient client,
            IHereKeyContainer keyContainer)
            : base(client)
        {
            _keyContainer = keyContainer;
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(
            GeocodeParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodeParameters>(parameters, BuildGeocodingRequest);

            return await CallAsync<GeocodingResponse, HereException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ReverseGeocodingResponse> ReverseGeocodingAsync(
            ReverseGeocodeParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodeParameters>(parameters, BuildReverseGeocodingRequest);

            return await CallAsync<ReverseGeocodingResponse, HereException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DiscoverResponse> DiscoverAsync(
            DiscoverParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<DiscoverParameters>(parameters, BuildDiscoverRequest);

            return await CallAsync<DiscoverResponse, HereException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<AutosuggestResponse> AutosuggestAsync(
            AutosuggestParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<AutosuggestParameters>(parameters, BuildAutosuggestRequest);

            return await CallAsync<AutosuggestResponse, HereException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<LookupResponse> LookupAsync(
            LookupParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<LookupParameters>(parameters, BuildLookupRequest);

            return await CallAsync<LookupResponse, HereException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<BrowseResponse> BrowseAsync(
            BrowseParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<BrowseParameters>(parameters, BuildBrowseRequest);

            return await CallAsync<BrowseResponse, HereException>(uri, _apiName, cancellationToken).ConfigureAwait(false);
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
                throw new HereException("The HERE parameters are null.", new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                throw new HereException("Failed to create the HERE uri.", ex);
            }
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodeParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed HERE geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Query' parameter and the 'QualifiedQuery' parameter are null or invalid.</exception>
        internal Uri BuildGeocodingRequest(GeocodeParameters parameters)
        {
            var uriBuilder = new UriBuilder(_geocodeUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Query) && string.IsNullOrWhiteSpace(parameters.QualifiedQuery))
            {
                throw new ArgumentException($"Both query items ({nameof(parameters.Query)}, {nameof(parameters.QualifiedQuery)}) cannot be null or empty.");
            }

            if (!string.IsNullOrWhiteSpace(parameters.Query))
            {
                query.Add("q", parameters.Query);
            }

            if (!string.IsNullOrWhiteSpace(parameters.QualifiedQuery))
            {
                query.Add("qq", parameters.QualifiedQuery);
            }

            if (!string.IsNullOrWhiteSpace(parameters.InCountry))
            {
                query.Add("in", parameters.InCountry);
            }

            AddLocatingParameters(parameters, query);

            AddHereKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodeParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed HERE reverse geocoding uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'At' parameter is null or invalid.</exception>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodeParameters parameters)
        {
            var uriBuilder = new UriBuilder(_reverseGeocodeUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parameters.At is null)
            {
                throw new ArgumentException("The at coordinates cannot be null.", nameof(parameters.At));
            }

            AddLocatingParameters(parameters, query);

            AddHereKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the discover uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="DiscoverParameters"/> with the discover parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed HERE discover uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Query' parameter is null or invalid.</exception>
        internal Uri BuildDiscoverRequest(DiscoverParameters parameters)
        {
            var uriBuilder = new UriBuilder(_discoverUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                throw new ArgumentException("The query cannot be null.", nameof(parameters.Query));
            }

            query.Add("q", parameters.Query);

            AddBoundingParameters(parameters, query);

            AddHereKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the autosuggest uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="AutosuggestParameters"/> with the autosuggest parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed HERE autosuggest uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Query' parameter is null or invalid.</exception>
        internal Uri BuildAutosuggestRequest(AutosuggestParameters parameters)
        {
            var uriBuilder = new UriBuilder(_autosuggestUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                throw new ArgumentException("The query cannot be null.", nameof(parameters.Query));
            }

            query.Add("q", parameters.Query);

            if (parameters.TermsLimit <= 10)
            {
                query.Add("termsLimit", parameters.TermsLimit.ToString(CultureInfo.InvariantCulture));
            }

            AddBoundingParameters(parameters, query);

            AddHereKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the browse uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="BrowseParameters"/> with the browse parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed HERE browse uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'At' parameter is null or invalid.</exception>
        internal Uri BuildBrowseRequest(BrowseParameters parameters)
        {
            var uriBuilder = new UriBuilder(_browseUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parameters.At is null)
            {
                throw new ArgumentException("The at coordinates cannot be null.", nameof(parameters.At));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Categories))
            {
                query.Add("categories", parameters.Categories);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Name))
            {
                query.Add("name", parameters.Name);
            }

            AddBoundingParameters(parameters, query);

            AddHereKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the lookup uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="LookupParameters"/> with the lookup parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed HERE discover uri.</returns>
        /// <exception cref="ArgumentException">Thrown when the 'Id' parameter is null or invalid.</exception>
        internal Uri BuildLookupRequest(LookupParameters parameters)
        {
            var uriBuilder = new UriBuilder(_lookupUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(parameters.Id))
            {
                throw new ArgumentException("The id cannot be null.", nameof(parameters.Id));
            }

            query.Add("id", parameters.Id);

            AddBaseParameters(parameters, query);

            AddHereKey(query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the bounding query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="DiscoverParameters"/> with the bounding parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddBoundingParameters(AreaParameters parameters, NameValueCollection query)
        {
            var hasAt = parameters.At != null && parameters.At.IsValid();
            var hasCircle = parameters.InCircle != null && parameters.InCircle.IsValid();
            var hasBoundingBox = parameters.InBoundingBox != null && parameters.InBoundingBox.IsValid();

            /*
             * Allowed combinations
             * The list of allowed at and in combinations is:
             *
             * at
             * at with in=countryCode
             * in=circle
             * in=circle with in=countryCode
             * in=bbox
             * in=bbox with in=countryCode
            */
            if ((!hasAt && !hasCircle && !hasBoundingBox) || (hasAt && (hasCircle || hasBoundingBox)) || (hasCircle && hasBoundingBox))
            {
                throw new ArgumentException("The combination of bounding parameters is not valid.");
            }

            if (hasAt)
            {
                query.Add("at", parameters.At.ToString());
            }

            if (!string.IsNullOrWhiteSpace(parameters.InCountry))
            {
                query.Add("in", $"countryCode:{parameters.InCountry}");
            }

            if (hasCircle)
            {
                query.Add("in", $"circle:{parameters.InCircle}");
            }

            if (hasBoundingBox)
            {
                query.Add("in", $"bbox:{parameters.InBoundingBox}");
            }

            if (!string.IsNullOrWhiteSpace(parameters.Route))
            {
                query.Add("route", parameters.Route);
            }

            AddLimitingParameters(parameters, query);
        }

        /// <summary>
        /// Adds the locating query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseFilterParameters"/> with the base limiting parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddLocatingParameters(BaseFilterParameters parameters, NameValueCollection query)
        {
            if (parameters.At != null)
            {
                query.Add("at", parameters.At.ToString());
            }

            AddLimitingParameters(parameters, query);
        }

        /// <summary>
        /// Adds the base limiting query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseFilterParameters"/> with the base limiting parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddLimitingParameters(BaseFilterParameters parameters, NameValueCollection query)
        {
            if (parameters.Limit > 0 && parameters.Limit <= 100)
            {
                query.Add("limit", parameters.Limit.ToString(CultureInfo.InvariantCulture));
            }

            AddBaseParameters(parameters, query);
        }

        /// <summary>
        /// Adds the base query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddBaseParameters(BaseParameters parameters, NameValueCollection query)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Language))
            {
                query.Add("lang", parameters.Language);
            }
        }

        /// <summary>
        /// Adds the HERE key to the query parameters.
        /// </summary>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddHereKey(NameValueCollection query)
        {
            query.Add("apiKey", _keyContainer.GetKey());
        }
    }
}
