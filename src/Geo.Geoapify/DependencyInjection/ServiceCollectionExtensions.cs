// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Geo.Geoapify;
    using Geo.Geoapify.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Geoapify geocoding services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IOptions{TOptions}"/> of <see cref="IGeoapifyGeocoding"/></item>
        /// <item><see cref="IGeoapifyGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the Geoapify services to.</param>
        /// <returns>An <see cref="KeyBuilder{T}"/> to configure the Geoapify geocoding.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static KeyBuilder<IGeoapifyGeocoding> AddGeoapifyGeocoding(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddKeyOptions<IGeoapifyGeocoding>();

            return new KeyBuilder<IGeoapifyGeocoding>(services.AddHttpClient<IGeoapifyGeocoding, GeoapifyGeocoding>());
        }
    }
}
