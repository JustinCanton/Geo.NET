// <copyright file="ArcGISTokenRetrevial.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Geo.ArcGIS.Tests")]

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

        private readonly IArcGISKeyContainer _keyContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISTokenRetrevial"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the ArcGIS token generation API.</param>
        /// <param name="keyContainer">A <see cref="IArcGISKeyContainer"/> used for fetching the ArcGIS keys.</param>
        public ArcGISTokenRetrevial(
            HttpClient client,
            IArcGISKeyContainer keyContainer)
        {
            _client = client;
            _keyContainer = keyContainer;
        }

        /// <inheritdoc/>
        public async Task<Token> GetTokenAsync(CancellationToken cancellationToken)
        {
            var keys = _keyContainer.GetKeys();

            if (string.IsNullOrWhiteSpace(keys.Item1) || string.IsNullOrWhiteSpace(keys.Item2))
            {
                // The key information isn't set.
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
            var keys = _keyContainer.GetKeys();

            var collection = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", keys.Item1),
                new KeyValuePair<string, string>("client_secret", keys.Item2),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            };

            var content = new FormUrlEncodedContent(collection);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            return content;
        }
    }
}
