// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.DependencyInjection
{
    using System;
    using System.Net.Http;
    using Geo.MapBox.Abstractions;
    using Geo.MapBox.Models;
    using Geo.MapBox.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the MapBox services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IMapBoxGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the MapBox services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{MapBoxOptionsBuilder}"/> with the options to add to the MapBox configuration.</param>
        /// <param name="configureClient">Optional. A delegate that is used to configure the <see cref="HttpClient"/>.</param>
        /// <returns>An <see cref="IHttpClientBuilder"/> to configure the http client.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static IHttpClientBuilder AddMapBoxServices(
            this IServiceCollection services,
            Action<MapBoxOptionsBuilder> optionsBuilder,
            Action<HttpClient> configureClient = null)
        {
            if (optionsBuilder != null)
            {
                var options = new MapBoxOptionsBuilder();
                optionsBuilder(options);

                services.AddSingleton<IMapBoxKeyContainer>(new MapBoxKeyContainer(options.Key));
            }
            else
            {
                services.AddSingleton<IMapBoxKeyContainer>(new MapBoxKeyContainer(string.Empty));
            }

            return services.AddHttpClient<IMapBoxGeocoding, MapBoxGeocoding>(configureClient ?? DefaultHttpClientConfiguration);
        }

        private static void DefaultHttpClientConfiguration(HttpClient client)
        {
            // No-op
        }
    }
}
