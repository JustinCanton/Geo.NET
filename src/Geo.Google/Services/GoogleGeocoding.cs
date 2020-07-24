// <copyright file="GoogleGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Services
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Geo.Core;
    using Geo.Google.Abstractions;
    using Geo.Google.Enums;
    using Geo.Google.Extensions;
    using Geo.Google.Models;

    /// <summary>
    /// A service to call the Google geocoding api.
    /// </summary>
    public class GoogleGeocoding : ClientExecutor, IGoogleGeocoding
    {
        private readonly string _baseUri = "https://maps.googleapis.com/maps/api/geocode/json";

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Google Geocoding API.</param>
        public GoogleGeocoding(HttpClient client)
            : base(client)
        {
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<GeocodingResponse>(BuildGeocodingRequest(parameters), cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<GeocodingResponse> ReverseGeocodingAsync(
            ReverseGeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<GeocodingResponse>(BuildReverseGeocodingRequest(parameters), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google geocoding uri.</returns>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(_baseUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parameters.Address != null)
            {
                query.Add("address", parameters.Address);
            }

            if (parameters.Components != null)
            {
                query.Add("components", parameters.Components);
            }

            if (parameters.Bounds != null &&
                parameters.Bounds.Southwest != null &&
                parameters.Bounds.Northeast != null)
            {
                query.Add("bounds", parameters.Bounds.ToString());
            }

            if (parameters.Region != null)
            {
                query.Add("region", parameters.Region);
            }

            if (parameters.Language != null)
            {
                query.Add("language", parameters.Language);
            }

            query.Add("key", GoogleKeyContainer.GetKey());

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the reverse geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the reverse geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Google reverse geocoding uri.</returns>
        internal Uri BuildReverseGeocodingRequest(ReverseGeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(_baseUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parameters.Coordinate != null)
            {
                query.Add("latlng", parameters.Coordinate.ToString());
            }

            if (parameters.ResultTypes != null)
            {
                var resultTypesBuilder = new StringBuilder();
                foreach (var resultType in parameters.ResultTypes)
                {
                    if (resultTypesBuilder.Length > 0)
                    {
                        resultTypesBuilder.Append("|");
                    }

                    resultTypesBuilder.Append(resultType.ToEnumString<ResultType>());
                }

                query.Add("result_type", resultTypesBuilder.ToString());
            }

            if (parameters.LocationTypes != null)
            {
                var locationTypesBuilder = new StringBuilder();
                foreach (var locationType in parameters.LocationTypes)
                {
                    if (locationTypesBuilder.Length > 0)
                    {
                        locationTypesBuilder.Append("|");
                    }

                    locationTypesBuilder.Append(locationType.ToEnumString<LocationType>());
                }

                query.Add("location_type ", locationTypesBuilder.ToString());
            }

            if (parameters.Language != null)
            {
                query.Add("language", parameters.Language);
            }

            query.Add("key", GoogleKeyContainer.GetKey());

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
