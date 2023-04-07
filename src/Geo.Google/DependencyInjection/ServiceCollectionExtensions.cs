// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.DependencyInjection
{
    using System;
    using System.Net.Http;
    using Geo.Core.DependencyInjection;
    using Geo.Google.Abstractions;
    using Geo.Google.Models;
    using Geo.Google.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Google services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IGoogleGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the Google services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{GoogleOptionsBuilder}"/> with the options to add to the Google configuration.</param>
        /// <param name="configureClient">Optional. A delegate that is used to configure the <see cref="HttpClient"/>.</param>
        /// <returns>An <see cref="IHttpClientBuilder"/> to configure the http client.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static IHttpClientBuilder AddGoogleServices(
            this IServiceCollection services,
            Action<GoogleOptionsBuilder> optionsBuilder,
            Action<HttpClient> configureClient = null)
        {
            services.AddCoreServices();

            if (optionsBuilder != null)
            {
                var options = new GoogleOptionsBuilder();
                optionsBuilder(options);

                services.AddSingleton<IGoogleKeyContainer>(new GoogleKeyContainer(options.Key));
            }
            else
            {
                services.AddSingleton<IGoogleKeyContainer>(new GoogleKeyContainer(string.Empty));
            }

            return services.AddHttpClient<IGoogleGeocoding, GoogleGeocoding>(configureClient ?? DefaultHttpClientConfiguration);
        }

        private static void DefaultHttpClientConfiguration(HttpClient client)
        {
            // No-op
        }
    }
}
