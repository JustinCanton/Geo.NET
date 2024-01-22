// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.DependencyInjection
{
    using System;
    using System.Net.Http;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Models;
    using Geo.ArcGIS.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the ArcGIS services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IArcGISGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the ArcGIS services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{ArcGISOptionsBuilder}"/> with the options to add to the ArcGIS configuration.</param>
        /// <param name="configureClient">Optional. A delegate that is used to configure the <see cref="HttpClient"/>.</param>
        /// <returns>An <see cref="IHttpClientBuilder"/> to configure the http client.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static IHttpClientBuilder AddArcGISServices(
            this IServiceCollection services,
            Action<ArcGISOptionsBuilder> optionsBuilder,
            Action<HttpClient> configureClient = null)
        {
            if (optionsBuilder != null)
            {
                var options = new ArcGISOptionsBuilder();
                optionsBuilder(options);

                services.AddSingleton<IArcGISCredentialsContainer>(new ArcGISCredentialsContainer(options.ClientId, options.ClientSecret));
            }
            else
            {
                services.AddSingleton<IArcGISCredentialsContainer>(new ArcGISCredentialsContainer(string.Empty, string.Empty));
            }

            services.AddHttpClient<IArcGISTokenRetrevial, ArcGISTokenRetrevial>();
            services.AddSingleton<IArcGISTokenContainer, ArcGISTokenContainer>();

            return services.AddHttpClient<IArcGISGeocoding, ArcGISGeocoding>(configureClient ?? DefaultHttpClientConfiguration);
        }

        private static void DefaultHttpClientConfiguration(HttpClient client)
        {
            // No-op
        }
    }
}
