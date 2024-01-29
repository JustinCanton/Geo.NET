// <copyright file="ArcGISGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.ArcGIS.Enums;
    using Geo.ArcGIS.Models.Parameters;
    using Geo.ArcGIS.Models.Responses;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Core.Models.Exceptions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A service to call the ArcGIS geocoding API.
    /// </summary>
    public class ArcGISGeocoding : GeoClient, IArcGISGeocoding
    {
        private const string CandidatesUri = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates";
        private const string SuggestUri = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/suggest";
        private const string ReverseGeocodingUri = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode";
        private const string GeocodingUri = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/geocodeAddresses";

        private readonly IArcGISTokenProvider _tokenProvider;
        private readonly IOptions<ClientCredentialsOptions<IArcGISGeocoding>> _options;
        private readonly ILogger<ArcGISGeocoding> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for making calls to the ArcGIS system.</param>
        /// <param name="tokenProvider">An <see cref="IArcGISTokenProvider"/> used for retreiving an ArcGIS token.</param>
        /// <param name="options">An <see cref="IOptions{TOptions}"/> of <see cref="ClientCredentialsOptions{T}"/> containing ArcGIS client credential information.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public ArcGISGeocoding(
            HttpClient client,
            IArcGISTokenProvider tokenProvider,
            IOptions<ClientCredentialsOptions<IArcGISGeocoding>> options,
            ILoggerFactory loggerFactory = null)
            : base(client, loggerFactory)
        {
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = loggerFactory?.CreateLogger<ArcGISGeocoding>() ?? NullLogger<ArcGISGeocoding>.Instance;
        }

        /// <inheritdoc/>
        protected override string ApiName => "ArcGIS";

        /// <inheritdoc/>
        public async Task<CandidateResponse> AddressCandidateAsync(
            AddressCandidateParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<AddressCandidateParameters>(parameters, BuildAddressCandidateRequest, cancellationToken).ConfigureAwait(false);

            return await GetAsync<CandidateResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CandidateResponse> PlaceCandidateAsync(
            PlaceCandidateParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<PlaceCandidateParameters>(parameters, BuildPlaceCandidateRequest, cancellationToken).ConfigureAwait(false);

            return await GetAsync<CandidateResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<SuggestResponse> SuggestAsync(
            SuggestParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<SuggestParameters>(parameters, BuildSuggestRequest, cancellationToken).ConfigureAwait(false);

            return await GetAsync<SuggestResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ReverseGeocodingResponse> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, BuildReverseGeocodingRequest, cancellationToken).ConfigureAwait(false);

            return await GetAsync<ReverseGeocodingResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            var uri = await ValidateAndBuildUri<GeocodingParameters>(parameters, BuildGeocodingRequest, cancellationToken).ConfigureAwait(false);

            return await GetAsync<GeocodingResponse>(uri, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds the ArcGIS storage flag to the query parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="StorageParameters"/> with the storage information.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        internal static void AddStorageParameter(StorageParameters parameters, ref QueryString query)
        {
#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("forStorage", parameters.ForStorage.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase
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
            where TParameters : class
        {
            if (parameters is null)
            {
                _logger.ArcGISError(Resources.Services.ArcGISGeocoding.Null_Parameters);
                throw new GeoNETException(Resources.Services.ArcGISGeocoding.Null_Parameters, new ArgumentNullException(nameof(parameters)));
            }

            try
            {
                return await uriBuilderFunction(parameters, cancellationToken).ConfigureAwait(false);
            }
            catch (ArgumentException ex)
            {
                _logger.ArcGISError(Resources.Services.ArcGISGeocoding.Failed_To_Create_Uri);
                throw new GeoNETException(Resources.Services.ArcGISGeocoding.Failed_To_Create_Uri, ex);
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
                _logger.ArcGISError(Resources.Services.ArcGISGeocoding.Invalid_Single_Address_Line);
                throw new ArgumentException(Resources.Services.ArcGISGeocoding.Invalid_Single_Address_Line, nameof(parameters.SingleLineAddress));
            }

            var uriBuilder = new UriBuilder(CandidatesUri);
            var query = QueryString.Empty;
            query = query.Add("f", "json");
            query = query.Add("outFields", "Match_addr,Addr_type");

            query = query.Add("singleLine", parameters.SingleLineAddress);

            if (parameters.MagicKey != null)
            {
                query = query.Add("magicKey", parameters.MagicKey);
            }

            AddStorageParameter(parameters, ref query);

            query = await AddArcGISToken(parameters, query, cancellationToken).ConfigureAwait(false);

            uriBuilder.AddQuery(query);

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
            var query = QueryString.Empty;
            query = query.Add("f", "json");
            query = query.Add("outFields", "Place_addr,PlaceName");

            if (!string.IsNullOrWhiteSpace(parameters.Address))
            {
                query = query.Add("address", parameters.Address);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Address);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Address2))
            {
                query = query.Add("address2", parameters.Address2);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Address2);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Address3))
            {
                query = query.Add("address3", parameters.Address3);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Address3);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Neighbourhood))
            {
                query = query.Add("neighborhood", parameters.Neighbourhood);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Neighbourhood);
            }

            if (!string.IsNullOrWhiteSpace(parameters.City))
            {
                query = query.Add("city", parameters.City);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_City);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Subregion))
            {
                query = query.Add("subregion", parameters.Subregion);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Subregion);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Region))
            {
                query = query.Add("region", parameters.Region);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Region);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Postal))
            {
                query = query.Add("postal", parameters.Postal);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Postal);
            }

            if (!string.IsNullOrWhiteSpace(parameters.PostalExt))
            {
                query = query.Add("postalExt", parameters.PostalExt);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_PostalExt);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CountryCode))
            {
                query = query.Add("countryCode", parameters.CountryCode);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Country_Code);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                query = query.Add("category", parameters.Category);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Category);
            }

            if (parameters.Location != null)
            {
                query = query.Add("location", parameters.Location.ToString());
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Location);
            }

            if (parameters.MaximumLocations > 0 && parameters.MaximumLocations <= 50)
            {
                query = query.Add("maxLocations", parameters.MaximumLocations.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.ArcGISWarning(Resources.Services.ArcGISGeocoding.Invalid_Maximum_Locations);
            }

            if (parameters.OutSpatialReference > 0)
            {
                query = query.Add("outSR", parameters.OutSpatialReference.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Out_Spatial_Reference);
            }

            if (parameters.SearchExtent != null)
            {
                query = query.Add("searchExtent", parameters.SearchExtent.ToString());
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Search_Extent);
            }

            if (parameters.LanguageCode != null)
            {
                query = query.Add("langCode", parameters.LanguageCode.TwoLetterISOLanguageName);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Language_Code);
            }

            if (parameters.LocationType >= 0)
            {
                query = query.Add("locationType", parameters.LocationType.ToEnumString<LocationType>());
            }
            else
            {
                _logger.ArcGISWarning(Resources.Services.ArcGISGeocoding.Invalid_Location_Type);
            }

            AddStorageParameter(parameters, ref query);

            query = await AddArcGISToken(parameters, query, cancellationToken).ConfigureAwait(false);

            uriBuilder.AddQuery(query);

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
            var query = QueryString.Empty;
            query = query.Add("f", "json");

            if (string.IsNullOrWhiteSpace(parameters.Text))
            {
                _logger.ArcGISError(Resources.Services.ArcGISGeocoding.Invalid_Text);
                throw new ArgumentException(Resources.Services.ArcGISGeocoding.Invalid_Text, nameof(parameters.Text));
            }

            query = query.Add("text", parameters.Text);

            if (parameters.Location != null)
            {
                query = query.Add("location", parameters.Location.ToString());
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Location);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                query = query.Add("category", parameters.Category);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Category);
            }

            if (parameters.SearchExtent != null)
            {
                query = query.Add("searchExtent", parameters.SearchExtent.ToString());
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Search_Extent);
            }

            if (parameters.MaximumLocations > 0 && parameters.MaximumLocations < 16)
            {
                query = query.Add("maxLocations", parameters.MaximumLocations.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.ArcGISWarning(Resources.Services.ArcGISGeocoding.Invalid_Maximum_Locations);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CountryCode))
            {
                query = query.Add("countryCode", parameters.CountryCode);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Country_Code);
            }

            if (parameters.SourceCountry.Count != 0)
            {
                query = query.Add("sourceCountry", string.Join(",", parameters.SourceCountry.Select(x => x.ThreeLetterISORegionName)));
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Source_Country);
            }

            if (parameters.PreferredLabelValue >= 0)
            {
                query = query.Add("preferredLabelValues", parameters.PreferredLabelValue.ToEnumString<PreferredLabelValue>());
            }
            else
            {
                _logger.ArcGISWarning(Resources.Services.ArcGISGeocoding.Invalid_Preferred_Label_Value);
            }

            uriBuilder.AddQuery(query);

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
            var query = QueryString.Empty;
            query = query.Add("f", "json");

            if (parameters.Location is null)
            {
                _logger.ArcGISError(Resources.Services.ArcGISGeocoding.Invalid_Location_Error);
                throw new ArgumentException(Resources.Services.ArcGISGeocoding.Invalid_Location_Error, nameof(parameters.Location));
            }

            query = query.Add("location", parameters.Location.ToString());

            if (parameters.OutSpatialReference > 0)
            {
                query = query.Add("outSR", parameters.OutSpatialReference.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Out_Spatial_Reference);
            }

            if (parameters.LanguageCode != null)
            {
                query = query.Add("langCode", parameters.LanguageCode.TwoLetterISOLanguageName);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Language_Code);
            }

            if (parameters.FeatureTypes != null)
            {
                var featureTypesBuilder = new CommaDelimitedStringCollection();
                foreach (var featureType in parameters.FeatureTypes)
                {
                    featureTypesBuilder.Add(featureType.ToEnumString<FeatureType>());
                }

                query = query.Add("featureTypes", featureTypesBuilder.ToString());
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Feature_Types);
            }

            if (parameters.LocationType >= 0)
            {
                query = query.Add("locationType", parameters.LocationType.ToEnumString<LocationType>());
            }
            else
            {
                _logger.ArcGISWarning(Resources.Services.ArcGISGeocoding.Invalid_Location_Type);
            }

            if (parameters.PreferredLabelValue >= 0)
            {
                query = query.Add("preferredLabelValues", parameters.PreferredLabelValue.ToEnumString<PreferredLabelValue>());
            }
            else
            {
                _logger.ArcGISWarning(Resources.Services.ArcGISGeocoding.Invalid_Preferred_Label_Value);
            }

            AddStorageParameter(parameters, ref query);

            query = await AddArcGISToken(parameters, query, cancellationToken).ConfigureAwait(false);

            uriBuilder.AddQuery(query);

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
            var query = QueryString.Empty;
            query = query.Add("f", "json");

            if (parameters.AddressAttributes is null || parameters.AddressAttributes.Count == 0)
            {
                _logger.ArcGISError(Resources.Services.ArcGISGeocoding.Invalid_Address_Attributes);
                throw new ArgumentException(Resources.Services.ArcGISGeocoding.Invalid_Address_Attributes, nameof(parameters.AddressAttributes));
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

#if NET6_0_OR_GREATER
            var options = new JsonSerializerOptions() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
#else
            var options = new JsonSerializerOptions() { IgnoreNullValues = true };
#endif
            query = query.Add("addresses", JsonSerializer.Serialize(addresses, options));

#pragma warning disable CA1308 // Normalize strings to uppercase
            query = query.Add("matchOutOfRange", parameters.MatchOutOfRange.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
#pragma warning restore CA1308 // Normalize strings to uppercase

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                query = query.Add("category", parameters.Category);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Category);
            }

            if (parameters.SourceCountry.Count != 0)
            {
                query = query.Add("sourceCountry", string.Join(",", parameters.SourceCountry.Select(x => x.ThreeLetterISORegionName)));
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Source_Country);
            }

            if (parameters.OutSpatialReference > 0)
            {
                query = query.Add("outSR", parameters.OutSpatialReference.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Out_Spatial_Reference);
            }

            if (parameters.SearchExtent != null)
            {
                query = query.Add("searchExtent", parameters.SearchExtent.ToString());
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Search_Extent);
            }

            if (parameters.LanguageCode != null)
            {
                query = query.Add("langCode", parameters.LanguageCode.TwoLetterISOLanguageName);
            }
            else
            {
                _logger.ArcGISDebug(Resources.Services.ArcGISGeocoding.Invalid_Language_Code);
            }

            if (parameters.LocationType >= 0)
            {
                query = query.Add("locationType", parameters.LocationType.ToEnumString<LocationType>());
            }
            else
            {
                _logger.ArcGISWarning(Resources.Services.ArcGISGeocoding.Invalid_Location_Type);
            }

            if (parameters.PreferredLabelValue >= 0)
            {
                query = query.Add("preferredLabelValues", parameters.PreferredLabelValue.ToEnumString<PreferredLabelValue>());
            }
            else
            {
                _logger.ArcGISWarning(Resources.Services.ArcGISGeocoding.Invalid_Preferred_Label_Value);
            }

            query = await AddArcGISToken(parameters, query, cancellationToken).ConfigureAwait(false);

            uriBuilder.AddQuery(query);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Adds the ArcGIS token to the query parameters.
        /// If the token cannot be generated, it will not be added to the request.
        /// </summary>
        /// <param name="clientCredentials">An <see cref="IClientCredentialsParameters"/> to conditionally get the client credentials from.</param>
        /// <param name="query">A <see cref="QueryString"/> with the query parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        internal async Task<QueryString> AddArcGISToken(IClientCredentialsParameters clientCredentials, QueryString query, CancellationToken cancellationToken)
        {
            var clientId = _options.Value.ClientId;
            var clientSecret = _options.Value.ClientSecret;

            if (!string.IsNullOrWhiteSpace(clientCredentials.ClientId) && !string.IsNullOrWhiteSpace(clientCredentials.ClientSecret))
            {
                clientId = clientCredentials.ClientId;
                clientSecret = clientCredentials.ClientSecret;
            }

            var token = await _tokenProvider.GetTokenAsync(clientId, clientSecret, cancellationToken).ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(token))
            {
                return query.Add("token", token);
            }

            return query;
        }
    }
}
