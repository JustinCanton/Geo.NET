// <copyright file="HereGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Services
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core;
    using Geo.Here.Abstractions;
    using Geo.Here.Models.Exceptions;
    using Geo.Here.Models.Parameters;
    using Geo.Here.Models.Responses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// A service to call the HERE geocoding API.
    /// </summary>
    public class HereGeocoding : ClientExecutor, IHereGeocoding
    {
        private const string ApiName = "Here";
        private const string GeocodeUri = "https://geocode.search.hereapi.com/v1/geocode";
        private const string ReverseGeocodeUri = "https://revgeocode.search.hereapi.com/v1/revgeocode";
        private const string DiscoverUri = "https://discover.search.hereapi.com/v1/discover";
        private const string AutosuggestUri = "https://autosuggest.search.hereapi.com/v1/autosuggest";
        private const string BrowseUri = "https://browse.search.hereapi.com/v1/browse";
        private const string LookupUri = "https://lookup.search.hereapi.com/v1/lookup";

        private readonly IHereKeyContainer _keyContainer;
        private readonly IGeoNETResourceStringProvider _resourceStringProvider;
        private readonly ILogger<HereGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HereGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the HERE Geocoding API.</param>
        /// <param name="keyContainer">A <see cref="IHereKeyContainer"/> used for fetching the HERE key.</param>
        /// <param name="exceptionProvider">An <see cref="IGeoNETExceptionProvider"/> used to provide exceptions based on an exception type.</param>
        /// <param name="resourceStringProviderFactory">An <see cref="IGeoNETResourceStringProviderFactory"/> used to create a resource string provider for log or exception messages.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public HereGeocoding(
            HttpClient client,
            IHereKeyContainer keyContainer,
            IGeoNETExceptionProvider exceptionProvider,
            IGeoNETResourceStringProviderFactory resourceStringProviderFactory,
            ILoggerFactory loggerFactory = null)
            : base(client, exceptionProvider, resourceStringProviderFactory, loggerFactory)
        {
            _keyContainer = keyContainer ?? throw new ArgumentNullException(nameof(keyContainer));
            _resourceStringProvider = resourceStringProviderFactory?.CreateResourceStringProvider<HereGeocoding>() ?? throw new ArgumentNullException(nameof(resourceStringProviderFactory));
            _logger = loggerFactory?.CreateLogger<HereGeocoding>() ?? NullLogger<HereGeocoding>.Instance;
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(
            GeocodeParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<GeocodeParameters>(parameters, BuildGeocodingRequest);

            return await CallAsync<GeocodingResponse, HereException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ReverseGeocodingResponse> ReverseGeocodingAsync(
            ReverseGeocodeParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<ReverseGeocodeParameters>(parameters, BuildReverseGeocodingRequest);

            return await CallAsync<ReverseGeocodingResponse, HereException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DiscoverResponse> DiscoverAsync(
            DiscoverParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<DiscoverParameters>(parameters, BuildDiscoverRequest);

            return await CallAsync<DiscoverResponse, HereException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<AutosuggestResponse> AutosuggestAsync(
            AutosuggestParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<AutosuggestParameters>(parameters, BuildAutosuggestRequest);

            return await CallAsync<AutosuggestResponse, HereException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<LookupResponse> LookupAsync(
            LookupParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<LookupParameters>(parameters, BuildLookupRequest);

            return await CallAsync<LookupResponse, HereException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<BrowseResponse> BrowseAsync(
            BrowseParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = ValidateAndBuildUri<BrowseParameters>(parameters, BuildBrowseRequest);

            return await CallAsync<BrowseResponse, HereException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
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
                var error = _resourceStringProvider.GetString("Null Parameters");
                _logger.HereError(error);
                throw new HereException(error, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return uriBuilderFunction(parameters);
            }
            catch (ArgumentException ex)
            {
                var error = _resourceStringProvider.GetString("Failed To Create Uri");
                _logger.HereError(error);
                throw new HereException(error, ex);
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
            var uriBuilder = new UriBuilder(GeocodeUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Query) && string.IsNullOrWhiteSpace(parameters.QualifiedQuery))
            {
                var error = _resourceStringProvider.GetString("Invalid Query And Qualified Query");
                _logger.HereError(error);
                throw new ArgumentException(error, nameof(parameters));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Query))
            {
                query = query.Add("q", parameters.Query);
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid Query"));
            }

            if (!string.IsNullOrWhiteSpace(parameters.QualifiedQuery))
            {
                query = query.Add("qq", parameters.QualifiedQuery);
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid Qualified Query"));
            }

            if (parameters.InCountry.Count > 0)
            {
                query = query.Add("in", string.Join(",", parameters.InCountry.Select(x => x.ThreeLetterISORegionName)));
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid In Country"));
            }

            AddLocatingParameters(parameters, ref query);

            AddHereKey(ref query);

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
            var uriBuilder = new UriBuilder(ReverseGeocodeUri);
            var query = QueryString.Empty;

            if (parameters.At is null)
            {
                var error = _resourceStringProvider.GetString("Invalid At");
                _logger.HereError(error);
                throw new ArgumentException(error, nameof(parameters.At));
            }

            AddLocatingParameters(parameters, ref query);

            AddHereKey(ref query);

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
            var uriBuilder = new UriBuilder(DiscoverUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                var error = _resourceStringProvider.GetString("Invalid Query Error");
                _logger.HereError(error);
                throw new ArgumentException(error, nameof(parameters.Query));
            }

            query = query.Add("q", parameters.Query);

            AddBoundingParameters(parameters, ref query);

            AddHereKey(ref query);

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
            var uriBuilder = new UriBuilder(AutosuggestUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Query))
            {
                var error = _resourceStringProvider.GetString("Invalid Query Error");
                _logger.HereError(error);
                throw new ArgumentException(error, nameof(parameters.Query));
            }

            query = query.Add("q", parameters.Query);

            if (parameters.TermsLimit <= 10)
            {
                query = query.Add("termsLimit", parameters.TermsLimit.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.HereWarning(_resourceStringProvider.GetString("Invalid Terms Limit"));
            }

            AddBoundingParameters(parameters, ref query);

            AddHereKey(ref query);

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
            var uriBuilder = new UriBuilder(BrowseUri);
            var query = QueryString.Empty;

            if (parameters.At is null)
            {
                var error = _resourceStringProvider.GetString("Invalid At");
                _logger.HereError(error);
                throw new ArgumentException(error, nameof(parameters.At));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Categories))
            {
                query = query.Add("categories", parameters.Categories);
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid Categories"));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Name))
            {
                query = query.Add("name", parameters.Name);
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid Name"));
            }

            AddBoundingParameters(parameters, ref query);

            AddHereKey(ref query);

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
            var uriBuilder = new UriBuilder(LookupUri);
            var query = QueryString.Empty;

            if (string.IsNullOrWhiteSpace(parameters.Id))
            {
                var error = _resourceStringProvider.GetString("Invalid Id");
                _logger.HereError(error);
                throw new ArgumentException(error, nameof(parameters.Id));
            }

            query = query.Add("id", parameters.Id);

            AddBaseParameters(parameters, ref query);

            AddHereKey(ref query);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the bounding query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="DiscoverParameters"/> with the bounding parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddBoundingParameters(AreaParameters parameters, ref QueryString query)
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
                var error = _resourceStringProvider.GetString("Invalid Bounding Parameters");
                _logger.HereError(error);
                throw new ArgumentException(error, nameof(parameters));
            }

            if (hasAt)
            {
                query = query.Add("at", parameters.At.ToString());
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid At Debug"));
            }

            if (!string.IsNullOrWhiteSpace(parameters.InCountry))
            {
                query = query.Add("in", $"countryCode:{parameters.InCountry}");
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid In Country"));
            }

            if (hasCircle)
            {
                query = query.Add("in", $"circle:{parameters.InCircle}");
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid In Circle"));
            }

            if (hasBoundingBox)
            {
                query = query.Add("in", $"bbox:{parameters.InBoundingBox}");
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid In Bounding Box"));
            }

            if (parameters.FlexiblePolyline != null)
            {
                var route = PolylineEncoderDecoder.Encode(
                    parameters.FlexiblePolyline.Coordinates,
                    parameters.FlexiblePolyline.Precision,
                    parameters.FlexiblePolyline.ThirdDimension,
                    parameters.FlexiblePolyline.ThirdDimensionPrecision);

                query = query.Add("route", route);
            }
            else if (!string.IsNullOrWhiteSpace(parameters.Route))
            {
                query = query.Add("route", parameters.Route);
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid Route"));
            }

            AddLimitingParameters(parameters, ref query);
        }

        /// <summary>
        /// Adds the locating query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseFilterParameters"/> with the base limiting parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddLocatingParameters(BaseFilterParameters parameters, ref QueryString query)
        {
            if (parameters.At != null)
            {
                query = query.Add("at", parameters.At.ToString());
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid At Debug"));
            }

            AddLimitingParameters(parameters, ref query);
        }

        /// <summary>
        /// Adds the base limiting query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseFilterParameters"/> with the base limiting parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddLimitingParameters(BaseFilterParameters parameters, ref QueryString query)
        {
            if (parameters.Limit > 0 && parameters.Limit <= 100)
            {
                query = query.Add("limit", parameters.Limit.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid Limit"));
            }

            AddBaseParameters(parameters, ref query);
        }

        /// <summary>
        /// Adds the base query parameters based on the allowed logic.
        /// </summary>
        /// <param name="parameters">A <see cref="BaseParameters"/> with the base parameters to build the uri with.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddBaseParameters(BaseParameters parameters, ref QueryString query)
        {
            if (parameters.Language != null)
            {
                query = query.Add("lang", parameters.Language.Name);
            }
            else
            {
                _logger.HereDebug(_resourceStringProvider.GetString("Invalid Language"));
            }
        }

        /// <summary>
        /// Adds the HERE key to the query parameters.
        /// </summary>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal void AddHereKey(ref QueryString query)
        {
            query = query.Add("apiKey", _keyContainer.GetKey());
        }
    }
}
