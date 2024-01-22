// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.DependencyInjection
{
    using System;
    using System.Net.Http;
    using Geo.MapQuest.Abstractions;
    using Geo.MapQuest.Models;
    using Geo.MapQuest.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the MapQuest services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IMapQuestGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the MapQuest services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{MapQuestOptionsBuilder}"/> with the options to add to the MapQuest configuration.</param>
        /// <param name="configureClient">Optional. A delegate that is used to configure the <see cref="HttpClient"/>.</param>
        /// <returns>An <see cref="IHttpClientBuilder"/> to configure the http client.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static IHttpClientBuilder AddMapQuestServices(
            this IServiceCollection services,
            Action<MapQuestOptionsBuilder> optionsBuilder,
            Action<HttpClient> configureClient = null)
        {
            if (optionsBuilder != null)
            {
                var options = new MapQuestOptionsBuilder();
                optionsBuilder(options);

                services.AddSingleton<IMapQuestKeyContainer>(new MapQuestKeyContainer(options.Key));
                services.AddSingleton<IMapQuestEndpoint>(new MapQuestEndpoint(options.UseLicensedEndpoint));
            }
            else
            {
                services.AddSingleton<IMapQuestKeyContainer>(new MapQuestKeyContainer(string.Empty));
                services.AddSingleton<IMapQuestEndpoint>(new MapQuestEndpoint(false));
            }

            return services.AddHttpClient<IMapQuestGeocoding, MapQuestGeocoding>(configureClient ?? DefaultHttpClientConfiguration);
        }

        private static void DefaultHttpClientConfiguration(HttpClient client)
        {
            // No-op
        }
    }
}
