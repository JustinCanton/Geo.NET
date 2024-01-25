// <copyright file="ArcGISTokenProvider.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Models;

    /// <summary>
    /// A class for providing an ArcGIS API token.
    /// </summary>
    public class ArcGISTokenProvider : IArcGISTokenProvider
    {
        private static readonly Uri _tokenRefreshAddress = new Uri("https://www.arcgis.com/sharing/rest/oauth2/token");
        private static readonly ConcurrentDictionary<string, (string Token, DateTime Expiry)> _tokens = new ConcurrentDictionary<string, (string Token, DateTime Expiry)>();
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISTokenProvider"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the ArcGIS token generation API.</param>
        public ArcGISTokenProvider(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public async Task<string> GetTokenAsync(string clientId, string clientSecret, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            {
                // The credential information isn't set.
                return null;
            }

            if (_tokens.TryGetValue($"{clientId}:{clientSecret}", out var existingToken) && existingToken.Expiry > DateTime.Now.AddSeconds(30))
            {
                return existingToken.Token;
            }

            using (var content = BuildContent(clientId, clientSecret))
            {
                var response = await _client.PostAsync(_tokenRefreshAddress, content, cancellationToken).ConfigureAwait(false);

#if NETSTANDARD2_0
                var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
#else
                var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#endif

                var token = await JsonSerializer.DeserializeAsync<Token>(stream).ConfigureAwait(false);

                _tokens.AddOrUpdate($"{clientId}:{clientSecret}", (token.AccessToken, DateTime.Now.AddSeconds(token.ExpiresIn)), (key, value) => (token.AccessToken, DateTime.Now.AddSeconds(token.ExpiresIn)));

                return token.AccessToken;
            }
        }

        /// <summary>
        /// Builds the content for the http request.
        /// </summary>
        /// <param name="clientId">The client id to use to fetch the token.</param>
        /// <param name="clientSecret">The client secret to use to fetch the token.</param>
        /// <returns>A <see cref="FormUrlEncodedContent"/> with the built up content information.</returns>
        internal FormUrlEncodedContent BuildContent(string clientId, string clientSecret)
        {
            var collection = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            };

            var content = new FormUrlEncodedContent(collection);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            return content;
        }
    }
}
