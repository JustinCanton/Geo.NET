// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.DependencyInjection
{
    using System;
    using System.Net.Http;
    using Geo.Bing.Abstractions;
    using Geo.Bing.Models;
    using Geo.Bing.Services;
    using Geo.Core.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Bing services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IBingGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the Bing services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{BingOptionsBuilder}"/> with the options to add to the Bing configuration.</param>
        /// <param name="configureClient">Optional. A delegate that is used to configure the <see cref="HttpClient"/>.</param>
        /// <returns>An <see cref="IHttpClientBuilder"/> to configure the http client.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static IHttpClientBuilder AddBingServices(
            this IServiceCollection services,
            Action<BingOptionsBuilder> optionsBuilder,
            Action<HttpClient> configureClient = null)
        {
            services.AddCoreServices();

            if (optionsBuilder != null)
            {
                var options = new BingOptionsBuilder();
                optionsBuilder(options);

                services.AddSingleton<IBingKeyContainer>(new BingKeyContainer(options.Key));
            }
            else
            {
                services.AddSingleton<IBingKeyContainer>(new BingKeyContainer(string.Empty));
            }

            return services.AddHttpClient<IBingGeocoding, BingGeocoding>(configureClient ?? DefaultHttpClientConfiguration);
        }

        private static void DefaultHttpClientConfiguration(HttpClient client)
        {
            // No-op
        }
    }
}
