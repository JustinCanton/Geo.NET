// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.DependencyInjection
{
    using System;
    using Geo.Core.DependencyInjection;
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
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the MapQuest services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{MapQuestOptionsBuilder}"/> with the options to add to the MapQuest configuration.</param>
        /// <returns>A <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddMapQuestServices(this IServiceCollection services, Action<MapQuestOptionsBuilder> optionsBuilder)
        {
            services.AddCoreServices();

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

            services.AddHttpClient<IMapQuestGeocoding, MapQuestGeocoding>();

            return services;
        }
    }
}
