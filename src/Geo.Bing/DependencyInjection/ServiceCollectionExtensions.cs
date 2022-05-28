// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.DependencyInjection
{
    using System;
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
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the Bing services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{BingOptionsBuilder}"/> with the options to add to the Bing configuration.</param>
        /// <returns>A <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddBingServices(this IServiceCollection services, Action<BingOptionsBuilder> optionsBuilder)
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

            services.AddHttpClient<IBingGeocoding, BingGeocoding>();

            return services;
        }
    }
}
