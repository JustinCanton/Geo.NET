// <copyright file="ClientCredentialsBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A builder used to configure an instance of a type associated with client credentials.
    /// </summary>
    /// <typeparam name="T">The type being configured.</typeparam>
    public sealed class ClientCredentialsBuilder<T> : BaseBuilder
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCredentialsBuilder{T}"/> class.
        /// </summary>
        /// <param name="httpClientBuilder">An <see cref="IHttpClientBuilder"/> to configure the http client.</param>
        public ClientCredentialsBuilder(IHttpClientBuilder httpClientBuilder)
            : base(httpClientBuilder)
        {
        }

        /// <summary>
        /// Adds a client credentials to use during API calls.
        /// </summary>
        /// <param name="clientId">The API client id to use.</param>
        /// <param name="clientSecret">The API client secret to use.</param>
        /// <returns>The modified <see cref="ClientCredentialsBuilder{T}"/>.</returns>
        public ClientCredentialsBuilder<T> AddClientCredentials(
            string clientId,
            string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException("The API client id cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new ArgumentException("The API client secret cannot be null or empty");
            }

            HttpClientBuilder.Services.AddClientCredentialsOptions<T>(x =>
            {
                x.ClientId = clientId;
                x.ClientSecret = clientSecret;
            });

            return this;
        }
    }
}
