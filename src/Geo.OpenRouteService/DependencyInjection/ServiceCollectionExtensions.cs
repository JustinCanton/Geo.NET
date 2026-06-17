// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Geo.OpenRouteService;
    using Geo.OpenRouteService.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the OpenRouteService geocoding services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IOptions{TOptions}"/> of <see cref="IOpenRouteServiceGeocoding"/></item>
        /// <item><see cref="IOpenRouteServiceGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the OpenRouteService services to.</param>
        /// <returns>A <see cref="KeyBuilder{T}"/> to configure the OpenRouteService geocoding.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static KeyBuilder<IOpenRouteServiceGeocoding> AddOpenRouteServiceGeocoding(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddKeyOptions<IOpenRouteServiceGeocoding>();

            return new KeyBuilder<IOpenRouteServiceGeocoding>(services.AddHttpClient<IOpenRouteServiceGeocoding, OpenRouteServiceGeocoding>());
        }
    }
}
