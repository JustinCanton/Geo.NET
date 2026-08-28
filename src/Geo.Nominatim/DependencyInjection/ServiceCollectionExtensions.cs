// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Geo.Nominatim;
    using Geo.Nominatim.Services;
    using Geo.Nominatim.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Nominatim geocoding services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IOptions{TOptions}"/> of <see cref="NominatimOptions"/></item>
        /// <item><see cref="INominatimGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the Nominatim services to.</param>
        /// <returns>An <see cref="NominatimBuilder"/> to configure the Nominatim geocoding.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static NominatimBuilder AddNominatimGeocoding(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<NominatimOptions>(x =>
            {
                x.Server = NominatimOptions.DefaultServer;
                x.Email = null;
                x.UserAgent = null;
            });

            return new NominatimBuilder(services.AddHttpClient<INominatimGeocoding, NominatimGeocoding>());
        }
    }
}
