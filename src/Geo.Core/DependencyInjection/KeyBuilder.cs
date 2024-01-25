// <copyright file="KeyBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A builder used to configure an instance of a type associated with keys.
    /// </summary>
    /// <typeparam name="T">The type being configured.</typeparam>
    public sealed class KeyBuilder<T> : BaseBuilder
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyBuilder{T}"/> class.
        /// </summary>
        /// <param name="httpClientBuilder">An <see cref="IHttpClientBuilder"/> to configure the http client.</param>
        public KeyBuilder(IHttpClientBuilder httpClientBuilder)
            : base(httpClientBuilder)
        {
        }

        /// <summary>
        /// Adds a key to use during API calls.
        /// </summary>
        /// <param name="key">The API key to use.</param>=
        /// <returns>The modified <see cref="KeyBuilder{T}"/>.</returns>
        public KeyBuilder<T> AddKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The API key cannot be null or empty");
            }

            HttpClientBuilder.Services.AddKeyOptions<T>(x =>
            {
                x.Key = key;
            });

            return this;
        }
    }
}
