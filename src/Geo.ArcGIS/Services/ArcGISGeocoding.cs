// <copyright file="ArcGISGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Enums;
    using Geo.ArcGIS.Extensions;
    using Geo.ArcGIS.Models.Parameters;
    using Geo.ArcGIS.Models.Responses;
    using Geo.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// A service to call the ArcGIS geocoding api.
    /// </summary>
    public class ArcGISGeocoding : ClientExecutor, IArcGISGeocoding
    {
        private readonly IArcGISTokenContainer _tokenContainer;

        private readonly Uri _candidatesUri = new Uri("https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates");

        private readonly Uri _suggestUri = new Uri("http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/suggest");

        private readonly Uri _reverseGeocodingUri = new Uri("https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode");

        private readonly Uri _geocodingUri = new Uri("https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/geocodeAddresses");

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for making calls to the ArcGIS system.</param>
        /// <param name="tokenContainer">A <see cref="IArcGISTokenContainer"/> used for retreiving the ArcGIS token.</param>
        public ArcGISGeocoding(
            HttpClient client,
            IArcGISTokenContainer tokenContainer)
            : base(client)
        {
            _tokenContainer = tokenContainer;
        }

        /// <inheritdoc/>
        public async Task<CandidateResponse> AddressCandidateAsync(AddressCandidateParameters parameters, CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<CandidateResponse>(await BuildAddressCandidateRequest(parameters, cancellationToken).ConfigureAwait(false), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CandidateResponse> PlaceCandidateAsync(PlaceCandidateParameters parameters, CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<CandidateResponse>(await BuildPlaceCandidateRequest(parameters, cancellationToken).ConfigureAwait(false), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<SuggestResponse> SuggestAsync(SuggestParameters parameters, CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<SuggestResponse>(BuildSuggestRequest(parameters), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ReverseGeocodingResponse> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<ReverseGeocodingResponse>(await BuildReverseGeocodingRequest(parameters, cancellationToken).ConfigureAwait(false), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<GeocodingResponse>(await BuildGeocodingRequest(parameters, cancellationToken).ConfigureAwait(false), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Builds the address candidate uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="AddressCandidateParameters"/> with the address candidate parameters to build the uri with.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Uri"/> with the completed ArcGIS geocoding uri.</returns>
        internal async Task<Uri> BuildAddressCandidateRequest(AddressCandidateParameters parameters, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(_candidatesUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");
            query.Add("outFields", "Match_addr,Addr_type");

            if (parameters.SingleLineAddress != null)
            {
                query.Add("singleLine", parameters.SingleLineAddress);
            }

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
            var uriBuilder = new UriBuilder(_candidatesUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");
            query.Add("outFields", "Place_addr,PlaceName");

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                query.Add("category", parameters.Category);
            }

            if (parameters.Location != null)
            {
                query.Add("location", parameters.Location.ToString());
            }

            if (parameters.MaximumLocations > 0)
            {
                query.Add("maxLocations", parameters.MaximumLocations.ToString(CultureInfo.InvariantCulture));
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
        /// <returns>A <see cref="Uri"/> with the completed ArcGIS geocoding uri.</returns>
        internal Uri BuildSuggestRequest(SuggestParameters parameters)
        {
            var uriBuilder = new UriBuilder(_suggestUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");

            if (string.IsNullOrWhiteSpace(parameters.Text))
            {
                throw new ArgumentException("The text cannot be null or empty.", nameof(parameters.Text));
            }

            query.Add("text", parameters.Text);

            if (parameters.Location != null)
            {
                query.Add("location", parameters.Location.ToString());
            }

            if (string.IsNullOrWhiteSpace(parameters.Category))
            {
                query.Add("category", parameters.Category);
            }

            if (parameters.SearchExtent != null)
            {
                query.Add("searchExtent", parameters.SearchExtent.ToString());
            }

            if (parameters.MaximumLocations > 0)
            {
                query.Add("maxLocations", parameters.MaximumLocations.ToString(CultureInfo.InvariantCulture));
            }

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Uri"/> with the completed ArcGIS geocoding uri.</returns>
        internal async Task<Uri> BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(_reverseGeocodingUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");

            if (parameters.Location is null)
            {
                throw new ArgumentException("The location cannot be null.", nameof(parameters.Location));
            }

            query.Add("location", parameters.Location.ToString());

            if (parameters.OutSpatialReference > 0)
            {
                query.Add("outSR", parameters.OutSpatialReference.ToString(CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrWhiteSpace(parameters.LanguageCode))
            {
                query.Add("langCode", parameters.LanguageCode);
            }

            if (parameters.FeatureTypes != null)
            {
                var featureTypesBuilder = new StringBuilder();
                foreach (var featureType in parameters.FeatureTypes)
                {
                    if (featureTypesBuilder.Length > 0)
                    {
                        featureTypesBuilder.Append(",");
                    }

                    featureTypesBuilder.Append(featureType.ToEnumString<FeatureType>());
                }

                query.Add("featureTypes", featureTypesBuilder.ToString());
            }

            if (parameters.LocationType >= 0)
            {
                query.Add("locationType", parameters.LocationType.ToEnumString<LocationType>());
            }

            if (parameters.PreferredLabelValue >= 0)
            {
                query.Add("preferredLabelValues", parameters.PreferredLabelValue.ToEnumString<PreferredLabelValue>());
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
            var uriBuilder = new UriBuilder(_geocodingUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("f", "json");

            if (parameters.AddressAttributes is null)
            {
                throw new ArgumentException("The address attributes cannot be null.", nameof(parameters.AddressAttributes));
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

            query.Add("matchOutOfRange", parameters.MatchOutOfRange.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());

            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                query.Add("category", parameters.Category);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SourceCountry))
            {
                query.Add("sourceCountry", parameters.SourceCountry);
            }

            if (parameters.OutSpatialReference > 0)
            {
                query.Add("outSR", parameters.OutSpatialReference.ToString(CultureInfo.InvariantCulture));
            }

            if (parameters.SearchExtent != null)
            {
                query.Add("searchExtent", parameters.SearchExtent.ToString());
            }

            if (!string.IsNullOrWhiteSpace(parameters.LanguageCode))
            {
                query.Add("langCode", parameters.LanguageCode);
            }

            if (parameters.LocationType >= 0)
            {
                query.Add("locationType", parameters.LocationType.ToEnumString<LocationType>());
            }

            if (parameters.PreferredLabelValue >= 0)
            {
                query.Add("preferredLabelValues", parameters.PreferredLabelValue.ToEnumString<PreferredLabelValue>());
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
            query.Add("forStorage", parameters.ForStorage.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
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
