// <copyright file="BingDI.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.DependencyInjection
{
    using System;
    using Geo.Bing.Abstractions;
    using Geo.Bing.Models;
    using Geo.Bing.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Dependency injection methods for the Bing APIs.
    /// </summary>
    public static class BingDI
    {
        /// <summary>
        /// Adds the Bing services to the service collection.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the Bing services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{BingOptionsBuilder}"/> with the options to add to the Bing configuration.</param>
        /// <returns>A <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddBingServices(this IServiceCollection services, Action<BingOptionsBuilder> optionsBuilder)
        {
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

            services.AddHttpClient<IBingGeocoding, BingGeocoding>();

            return services;
        }
    }
}
