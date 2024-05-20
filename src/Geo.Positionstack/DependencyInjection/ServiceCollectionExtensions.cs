// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Geo.Positionstack;
    using Geo.Positionstack.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Positionstack geocoding services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IOptions{TOptions}"/> of <see cref="IPositionstackGeocoding"/></item>
        /// <item><see cref="IPositionstackGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the Positionstack services to.</param>
        /// <returns>An <see cref="KeyBuilder{T}"/> to configure the Positionstack geocoding.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static KeyBuilder<IPositionstackGeocoding> AddPositionstackGeocoding(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddKeyOptions<IPositionstackGeocoding>();

            return new KeyBuilder<IPositionstackGeocoding>(services.AddHttpClient<IPositionstackGeocoding, PositionstackGeocoding>());
        }
    }
}
