﻿// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.DependencyInjection
{
    using System;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Models;
    using Geo.ArcGIS.Services;
    using Geo.Core.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the ArcGIS services to the service collection.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the ArcGIS services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{ArcGISOptionsBuilder}"/> with the options to add to the ArcGIS configuration.</param>
        /// <returns>A <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddArcGISServices(this IServiceCollection services, Action<ArcGISOptionsBuilder> optionsBuilder)
        {
            services.AddCoreServices();

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
            services.AddHttpClient<IArcGISGeocoding, ArcGISGeocoding>();

            return services;
        }
    }
}
