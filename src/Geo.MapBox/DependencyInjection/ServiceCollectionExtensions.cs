// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.DependencyInjection
{
    using System;
    using Geo.Core.DependencyInjection;
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
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the MapBox services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{MapBoxOptionsBuilder}"/> with the options to add to the MapBox configuration.</param>
        /// <returns>A <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddMapBoxServices(this IServiceCollection services, Action<MapBoxOptionsBuilder> optionsBuilder)
        {
            services.AddCoreServices();

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

            services.AddHttpClient<IMapBoxGeocoding, MapBoxGeocoding>();

            return services;
        }
    }
}
