// <copyright file="ArcGISTokenRetrevial.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// A class for retreiving the ArcGIS API token.
    /// </summary>
    public class ArcGISTokenRetrevial : IArcGISTokenRetrevial
    {
        private readonly Uri _tokenRefreshAddress = new Uri("https://www.arcgis.com/sharing/rest/oauth2/token");
        private readonly HttpClient _client;
        private readonly IArcGISCredentialsContainer _credentialsContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISTokenRetrevial"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the ArcGIS token generation API.</param>
        /// <param name="credentialsContainer">A <see cref="IArcGISCredentialsContainer"/> used for fetching the ArcGIS credentials.</param>
        public ArcGISTokenRetrevial(
            HttpClient client,
            IArcGISCredentialsContainer credentialsContainer)
        {
            _client = client;
            _credentialsContainer = credentialsContainer;
        }

        /// <inheritdoc/>
        public async Task<Token> GetTokenAsync(CancellationToken cancellationToken)
        {
            var credentials = _credentialsContainer.GetCredentials();

            if (string.IsNullOrWhiteSpace(credentials.ClientId) || string.IsNullOrWhiteSpace(credentials.ClientSecret))
            {
                // The credential information isn't set.
                // Return an object with an infinite time in the future so more calls aren't made.
                return new Token()
                {
                    AccessToken = string.Empty,
                    ExpiresIn = int.MaxValue,
                };
            }

            using var content = BuildContent();
            var response = await _client.PostAsync(_tokenRefreshAddress, content, cancellationToken).ConfigureAwait(false);

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Token>(json);
        }

        /// <summary>
        /// Builds the content for the http request.
        /// </summary>
        /// <returns>A <see cref="FormUrlEncodedContent"/> with the built up content information.</returns>
        internal FormUrlEncodedContent BuildContent()
        {
            var credentials = _credentialsContainer.GetCredentials();

            var collection = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", credentials.ClientId),
                new KeyValuePair<string, string>("client_secret", credentials.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            };

            var content = new FormUrlEncodedContent(collection);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            return content;
        }
    }
}
