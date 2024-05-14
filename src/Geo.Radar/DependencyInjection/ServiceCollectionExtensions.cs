// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Geo.Radar;
    using Geo.Radar.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Radar geocoding services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IOptions{TOptions}"/> of <see cref="IRadarGeocoding"/></item>
        /// <item><see cref="IRadarGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the Radar services to.</param>
        /// <returns>An <see cref="KeyBuilder{T}"/> to configure the Radar geocoding.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static KeyBuilder<IRadarGeocoding> AddRadarGeocoding(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddKeyOptions<IRadarGeocoding>();

            return new KeyBuilder<IRadarGeocoding>(services.AddHttpClient<IRadarGeocoding, RadarGeocoding>());
        }
    }
}
