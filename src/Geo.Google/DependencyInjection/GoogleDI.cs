// <copyright file="GoogleDI.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.DependencyInjection
{
    using System;
    using Geo.Google.Abstractions;
    using Geo.Google.Models;
    using Geo.Google.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Dependency injection methods for the Google APIs.
    /// </summary>
    public static class GoogleDI
    {
        /// <summary>
        /// Adds the Google services to the service collection.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the Google services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{GoogleOptionsBuilder}"/> with the options to add to the Google configuration.</param>
        /// <returns>A <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddGoogleServices(this IServiceCollection services, Action<GoogleOptionsBuilder> optionsBuilder)
        {
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

            services.AddHttpClient<IGoogleGeocoding, GoogleGeocoding>();

            return services;
        }
    }
}
