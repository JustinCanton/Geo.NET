// <copyright file="ArcGISDI.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.DependencyInjection
{
    using System;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Models;
    using Geo.ArcGIS.Services;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Dependency injection methods for the ArcGIS APIs.
    /// </summary>
    public static class ArcGISDI
    {
        /// <summary>
        /// Adds the ArcGIS services to the service collection.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the Google services to.</param>
        /// <param name="optionsBuilder">A <see cref="Action{ArcGISOptionsBuilder}"/> with the options to add to the Google configuration.</param>
        /// <returns>A <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddArcGISServices(this IServiceCollection services, Action<ArcGISOptionsBuilder> optionsBuilder)
        {
            if (optionsBuilder != null)
            {
                var options = new ArcGISOptionsBuilder();
                optionsBuilder(options);

                services.AddSingleton<IArcGISKeyContainer>(new ArcGISKeyContainer(options.ClientId, options.ClientSecret));
            }
            else
            {
                services.AddSingleton<IArcGISKeyContainer>(new ArcGISKeyContainer(string.Empty, string.Empty));
            }

            services.AddHttpClient<IArcGISTokenRetrevial, ArcGISTokenRetrevial>();
            services.AddSingleton<IArcGISTokenContainer, ArcGISTokenContainer>();
            services.AddHttpClient<IArcGISGeocoding, ArcGISGeocoding>();

            return services;
        }
    }
}
