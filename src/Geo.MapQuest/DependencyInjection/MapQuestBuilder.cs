// <copyright file="MapQuestBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Geo.MapQuest.Settings;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Options for the MapQuest configuration.
    /// </summary>
    public class MapQuestBuilder : BaseBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapQuestBuilder"/> class.
        /// </summary>
        /// <param name="httpClientBuilder">An <see cref="IHttpClientBuilder"/> to configure the http client.</param>
        public MapQuestBuilder(IHttpClientBuilder httpClientBuilder)
            : base(httpClientBuilder)
        {
        }

        /// <summary>
        /// Adds a key to use during API calls.
        /// </summary>
        /// <param name="key">The API key to use.</param>=
        /// <returns>The modified <see cref="KeyBuilder{T}"/>.</returns>
        public MapQuestBuilder AddKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The API key cannot be null or empty");
            }

            HttpClientBuilder.Services.Configure<MapQuestOptions>(x =>
            {
                x.Key = key;
            });

            return this;
        }

        /// <summary>
        /// Sets the licensed MapQuest endpoint to be used during calls.
        /// </summary>
        /// <returns>A <see cref="MapQuestBuilder"/> configured with the information about which endpoints to use.</returns>
        public MapQuestBuilder UseLicensedEndpoints()
        {
            HttpClientBuilder.Services.Configure<MapQuestOptions>(x =>
            {
                x.UseLicensedEndpoint = true;
            });

            return this;
        }
    }
}
