// <copyright file="HereDI.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.DependencyInjection
{
    using System;
    using Geo.Core.DependencyInjection;
    using Geo.Here.Abstractions;
    using Geo.Here.Models;
    using Geo.Here.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Dependency injection methods for the HERE APIs.
    /// </summary>
    public static class HereDI
    {
        /// <summary>
        /// Adds the HERE services to the service collection.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the HERE services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{HereOptionsBuilder}"/> with the options to add to the HERE configuration.</param>
        /// <returns>A <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddHereServices(this IServiceCollection services, Action<HereOptionsBuilder> optionsBuilder)
        {
            services.AddCoreServices();

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

            services.AddHttpClient<IHereGeocoding, HereGeocoding>();

            return services;
        }
    }
}
