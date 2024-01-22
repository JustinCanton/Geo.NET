// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.DependencyInjection
{
    using System;
    using System.Net.Http;
    using Geo.Here.Abstractions;
    using Geo.Here.Models;
    using Geo.Here.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the HERE services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IHereGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the HERE services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{HereOptionsBuilder}"/> with the options to add to the HERE configuration.</param>
        /// <param name="configureClient">Optional. A delegate that is used to configure the <see cref="HttpClient"/>.</param>
        /// <returns>An <see cref="IHttpClientBuilder"/> to configure the http client.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static IHttpClientBuilder AddHereServices(
            this IServiceCollection services,
            Action<HereOptionsBuilder> optionsBuilder,
            Action<HttpClient> configureClient = null)
        {
            if (optionsBuilder != null)
            {
                var options = new HereOptionsBuilder();
                optionsBuilder(options);

                services.AddSingleton<IHereKeyContainer>(new HereKeyContainer(options.Key));
            }
            else
            {
                services.AddSingleton<IHereKeyContainer>(new HereKeyContainer(string.Empty));
            }

            return services.AddHttpClient<IHereGeocoding, HereGeocoding>(configureClient ?? DefaultHttpClientConfiguration);
        }

        private static void DefaultHttpClientConfiguration(HttpClient client)
        {
            // No-op
        }
    }
}
