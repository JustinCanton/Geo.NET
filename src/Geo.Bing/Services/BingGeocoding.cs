// <copyright file="BingGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Services
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Geo.Bing.Abstractions;
    using Geo.Bing.Models;
    using Geo.Core;

    /// <summary>
    /// A service to call the Bing geocoding api.
    /// </summary>
    public class BingGeocoding : ClientExecutor, IBingGeocoding
    {
        private readonly string _baseUri = "http://dev.virtualearth.net/REST/v1/Locations";

        /// <summary>
        /// Initializes a new instance of the <see cref="BingGeocoding"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the Google Geocoding API.</param>
        public BingGeocoding(HttpClient client)
            : base(client)
        {
        }

        /// <inheritdoc/>
        public async Task<Response> GeocodingAsync(
            GeocodingParameters parameters,
            CancellationToken cancellationToken = default)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return await CallAsync<Response>(BuildGeocodingRequest(parameters), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Builds the geocoding uri based on the passed parameters.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the geocoding parameters to build the uri with.</param>
        /// <returns>A <see cref="Uri"/> with the completed Bing geocoding uri.</returns>
        internal Uri BuildGeocodingRequest(GeocodingParameters parameters)
        {
            var uriBuilder = new UriBuilder(_baseUri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (parameters.Query != null)
            {
                query.Add("query", parameters.Query);
            }

            if (parameters.IncludeNeighborhood == true)
            {
                query.Add("includeNeighborhood", "1");
            }

            var includes = string.Empty;
            if (parameters.IncludeQueryParse == true)
            {
                includes += "queryParse";
            }

            if (parameters.IncludeCiso2 == true)
            {
                if (includes.Length > 0)
                {
                    includes += ",";
                }

                includes += "ciso2";
            }

            if (includes.Length > 0)
            {
                query.Add("incl", includes);
            }

            if (parameters.MaximumResults > 0 && parameters.MaximumResults <= 20)
            {
                query.Add("maxRes", parameters.MaximumResults.ToString());
            }

            query.Add("key", BingKeyContainer.GetKey());

            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
