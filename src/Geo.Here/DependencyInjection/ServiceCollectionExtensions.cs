// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.DependencyInjection
{
    using System;
    using Geo.Here;
    using Geo.Here.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Here geocoding services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IOptions{TOptions}"/> of <see cref="IHereGeocoding"/></item>
        /// <item><see cref="IHereGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the Bing services to.</param>
        /// <returns>An <see cref="IHttpClientBuilder"/> to configure the http client.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static KeyBuilder<IHereGeocoding> AddHereGeocoding(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddKeyOptions<IHereGeocoding>();

            return new KeyBuilder<IHereGeocoding>(services.AddHttpClient<IHereGeocoding, HereGeocoding>());
        }
    }
}
