// <copyright file="ArcGISGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Enums;
    using Geo.ArcGIS.Models.Exceptions;
    using Geo.ArcGIS.Models.Parameters;
    using Geo.ArcGIS.Models.Responses;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Newtonsoft.Json;

    /// <summary>
    /// A service to call the ArcGIS geocoding API.
    /// </summary>
    public class ArcGISGeocoding : ClientExecutor, IArcGISGeocoding
    {
        private const string ApiName = "ArcGIS";
        private const string CandidatesUri = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates";
        private const string SuggestUri = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/suggest";
        private const string ReverseGeocodingUri = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode";
        private const string GeocodingUri = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/geocodeAddresses";

        private readonly IArcGISTokenContainer _tokenContainer;
        private readonly IStringLocalizer _localizer;
        private readonly ILogger<ArcGISGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for making calls to the ArcGIS system.</param>
        /// <param name="tokenContainer">A <see cref="IArcGISTokenContainer"/> used for retreiving the ArcGIS token.</param>
        /// <param name="exceptionProvider">An <see cref="IGeoNETExceptionProvider"/> used to provide exceptions based on an exception type.</param>
        /// <param name="localizerFactory">An <see cref="IStringLocalizerFactory"/> used to create a localizer for localizing log or exception messages.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public ArcGISGeocoding(
            HttpClient client,
            IArcGISTokenContainer tokenContainer,
            IGeoNETExceptionProvider exceptionProvider,
            IStringLocalizerFactory localizerFactory,
            ILoggerFactory loggerFactory = null)
            : base(client, exceptionProvider, localizerFactory, loggerFactory)
        {
            _tokenContainer = tokenContainer ?? throw new ArgumentNullException(nameof(tokenContainer));
            _localizer = localizerFactory?.Create(typeof(ArcGISGeocoding)) ?? throw new ArgumentNullException(nameof(localizerFactory));
            _logger = loggerFactory?.CreateLogger<ArcGISGeocoding>() ?? NullLogger<ArcGISGeocoding>.Instance;
        }

        /// <inheritdoc/>
        public async Task<CandidateResponse> AddressCandidateAsync(
            AddressCandidateParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<AddressCandidateParameters>(parameters, BuildAddressCandidateRequest, cancellationToken).ConfigureAwait(false);

            return await CallAsync<CandidateResponse, ArcGISException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CandidateResponse> PlaceCandidateAsync(
            PlaceCandidateParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<PlaceCandidateParameters>(parameters, BuildPlaceCandidateRequest, cancellationToken).ConfigureAwait(false);

            return await CallAsync<CandidateResponse, ArcGISException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<SuggestResponse> SuggestAsync(
            SuggestParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<SuggestParameters>(parameters, BuildSuggestRequest, cancellationToken).ConfigureAwait(false);

            return await CallAsync<SuggestResponse, ArcGISException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ReverseGeocodingResponse> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest, cancellationToken).ConfigureAwait(false);

            return await CallAsync<ReverseGeocodingResponse, ArcGISException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest, cancellationToken).ConfigureAwait(false);

            return await CallAsync<GeocodingResponse, ArcGISException>(uri, ApiName, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Validates the uri and builds it based on the parameter type.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameters.</typeparam>
        /// <param name="parameters">The parameters to validate and create a uri from.</param>
        /// <param name="uriBuilderFunction">The method to use to create the uri.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the paramter building.</param>
        /// <returns>A <see cref="Uri"/> with the uri crafted from the parameters.</returns>
        internal async Task<Uri> ValidateAndBuildUri<TParameters>(
            TParameters parameters,
            Func<TParameters, CancellationToken, Task<Uri>> uriBuilderFunction,
            CancellationToken cancellationToken)
        {
            if (parameters is null)
            {
                var error = _localizer["Null Parameters"];
                _logger.ArcGISError(error);
                throw new ArcGISException(error, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return await uriBuilderFunction(parameters, cancellationToken).ConfigureAwait(false);
            }
            catch (ArgumentException ex)
            {
                var error = _localizer["Failed To Create Uri"];
                _logger.ArcGISError(error);
                throw new ArcGISException(error, ex);
            }
        }

        /// <summary>
        /// Builds the address candidate uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="AddressCandidateParameters"/> with the address candidate parameters to build the uri with.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Uri"/> with the completed ArcGIS geocoding uri.</returns>
        internal async Task<Uri> BuildAddressCandidateRequest(AddressCandidateParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(parameters.SingleLineAddress))
            {
                var error = _localizer["Invalid Single Address Line"];
                _logger.ArcGISError(error);
                throw new ArgumentException(error, nameof(parameters.SingleLineAddress));
            }

            var uriBuilder = new UriBuilder(CandidatesUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");
            query.Add("outFields", "Match_addr,Addr_type");

            query.Add("singleLine", parameters.SingleLineAddress);

            AddStorageParameter(parameters, query);

            await AddArcGISToken(query, cancellationToken).ConfigureAwait(false);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the place candidate uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="PlaceCandidateParameters"/> with the place candidate parameters to build the uri with.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Uri"/> with the completed ArcGIS geocoding uri.</returns>
        internal async Task<Uri> BuildPlaceCandidateRequest(PlaceCandidateParameters parameters, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(CandidatesUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");
            query.Add("outFields", "Place_addr,PlaceName");

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                query.Add("category", parameters.Category);
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Category"]);
            }

            if (parameters.Location != null)
            {
                query.Add("location", parameters.Location.ToString());
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Location"]);
            }

            if (parameters.MaximumLocations > 0 && parameters.MaximumLocations <= 50)
            {
                query.Add("maxLocations", parameters.MaximumLocations.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.ArcGISWarning(_localizer["Invalid Maximum Locations"]);
            }

            AddStorageParameter(parameters, query);

            await AddArcGISToken(query, cancellationToken).ConfigureAwait(false);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the suggest uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="SuggestParameters"/> with the suggest parameters to build the uri with.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Uri"/> with the completed ArcGIS geocoding uri.</returns>
        internal Task<Uri> BuildSuggestRequest(SuggestParameters parameters, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(SuggestUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");

            if (string.IsNullOrWhiteSpace(parameters.Text))
            {
                var error = _localizer["Invalid Text"];
                _logger.ArcGISError(error);
                throw new ArgumentException(error, nameof(parameters.Text));
            }

            query.Add("text", parameters.Text);

            if (parameters.Location != null)
            {
                query.Add("location", parameters.Location.ToString());
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Location"]);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                query.Add("category", parameters.Category);
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Category"]);
            }

            if (parameters.SearchExtent != null)
            {
                query.Add("searchExtent", parameters.SearchExtent.ToString());
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Search Extent"]);
            }

            if (parameters.MaximumLocations > 0 && parameters.MaximumLocations < 16)
            {
                query.Add("maxLocations", parameters.MaximumLocations.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.ArcGISWarning(_localizer["Invalid Maximum Locations"]);
            }

            uriBuilder.Query = query.ToString();

            return Task.FromResult<Uri>(uriBuilder.Uri);
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Uri"/> with the completed ArcGIS geocoding uri.</returns>
        internal async Task<Uri> BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(ReverseGeocodingUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");

            if (parameters.Location is null)
            {
                var error = _localizer["Invalid Location Error"];
                _logger.ArcGISError(error);
                throw new ArgumentException(error, nameof(parameters.Location));
            }

            query.Add("location", parameters.Location.ToString());

            if (parameters.OutSpatialReference > 0)
            {
                query.Add("outSR", parameters.OutSpatialReference.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Out Spatial Reference"]);
            }

            if (parameters.LanguageCode != null)
            {
                query.Add("langCode", parameters.LanguageCode.TwoLetterISOLanguageName);
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Language Code"]);
            }

            if (parameters.FeatureTypes != null)
            {
                var featureTypesBuilder = new CommaDelimitedStringCollection();
                foreach (var featureType in parameters.FeatureTypes)
                {
                    featureTypesBuilder.Add(featureType.ToEnumString<FeatureType>());
                }

                query.Add("featureTypes", featureTypesBuilder.ToString());
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Feature Types"]);
            }

            if (parameters.LocationType >= 0)
            {
                query.Add("locationType", parameters.LocationType.ToEnumString<LocationType>());
            }
            else
            {
                _logger.ArcGISWarning(_localizer["Invalid Location Type"]);
            }

            if (parameters.PreferredLabelValue >= 0)
            {
                query.Add("preferredLabelValues", parameters.PreferredLabelValue.ToEnumString<PreferredLabelValue>());
            }
            else
            {
                _logger.ArcGISWarning(_localizer["Invalid Preferred Label Value"]);
            }

            AddStorageParameter(parameters, query);

            await AddArcGISToken(query, cancellationToken).ConfigureAwait(false);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Uri"/> with the completed ArcGIS geocoding uri.</returns>
        internal async Task<Uri> BuildGeocodingRequest(GeocodingParameters parameters, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(GeocodingUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");

            if (parameters.AddressAttributes is null || parameters.AddressAttributes.Count == 0)
            {
                var error = _localizer["Invalid Address Attributes"];
                _logger.ArcGISError(error);
                throw new ArgumentException(error, nameof(parameters.AddressAttributes));
            }

            List<object> attributes = new List<object>();

            foreach (var attribute in parameters.AddressAttributes)
            {
                attributes.Add(new
                {
                    attributes = attribute,
                });
            }

            var addresses = new
            {
                records = attributes,
            };

            query.Add("addresses", JsonConvert.SerializeObject(addresses));

#pragma warning disable CA1308 // Normalize strings to uppercase
            query.Add("matchOutOfRange", parameters.MatchOutOfRange.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                query.Add("category", parameters.Category);
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Category"]);
            }

            if (parameters.SourceCountry.Count != 0)
            {
                query.Add("sourceCountry", string.Join(",", parameters.SourceCountry.Select(x => x.ThreeLetterISORegionName)));
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Source Country"]);
            }

            if (parameters.OutSpatialReference > 0)
            {
                query.Add("outSR", parameters.OutSpatialReference.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Out Spatial Reference"]);
            }

            if (parameters.SearchExtent != null)
            {
                query.Add("searchExtent", parameters.SearchExtent.ToString());
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Search Extent"]);
            }

            if (parameters.LanguageCode != null)
            {
                query.Add("langCode", parameters.LanguageCode.TwoLetterISOLanguageName);
            }
            else
            {
                _logger.ArcGISDebug(_localizer["Invalid Language Code"]);
            }

            if (parameters.LocationType >= 0)
            {
                query.Add("locationType", parameters.LocationType.ToEnumString<LocationType>());
            }
            else
            {
                _logger.ArcGISWarning(_localizer["Invalid Location Type"]);
            }

            if (parameters.PreferredLabelValue >= 0)
            {
                query.Add("preferredLabelValues", parameters.PreferredLabelValue.ToEnumString<PreferredLabelValue>());
            }
            else
            {
                _logger.ArcGISWarning(_localizer["Invalid Preferred Label Value"]);
            }

            await AddArcGISToken(query, cancellationToken).ConfigureAwait(false);

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the ArcGIS storage flag to the query parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="StorageParameters"/> with the storage information.</param>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        internal void AddStorageParameter(StorageParameters parameters, NameValueCollection query)
        {
#pragma warning disable CA1308 // Normalize strings to uppercase
            query.Add("forStorage", parameters.ForStorage.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase
        }

        /// <summary>
        /// Adds the ArcGIS token to the query parameters.
        /// If the token cannot be generated, it will not be added to the request.
        /// </summary>
        /// <param name="query">A <see cref="NameValueCollection"/> with the query parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        internal async Task AddArcGISToken(NameValueCollection query, CancellationToken cancellationToken)
        {
            var token = await _tokenContainer.GetTokenAsync(cancellationToken).ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(token))
            {
                query.Add("token", token);
            }
        }
    }
}
