// <copyright file="BaseBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A builder used to configure an instance of a type.
    /// </summary>
    public abstract class BaseBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBuilder"/> class.
        /// </summary>
        /// <param name="httpClientBuilder">An <see cref="IHttpClientBuilder"/> to configure the http client.</param>
        public BaseBuilder(IHttpClientBuilder httpClientBuilder)
        {
            HttpClientBuilder = httpClientBuilder ?? throw new ArgumentNullException(nameof(httpClientBuilder));
        }

        /// <summary>
        /// Gets the <see cref="IHttpClientBuilder"/> used to configure the HttpClient of the <typeparamref name="T"/> instance.
        /// </summary>
        public IHttpClientBuilder HttpClientBuilder { get; }
    }
}
