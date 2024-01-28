// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Geo.MapQuest;
    using Geo.MapQuest.Services;
    using Geo.MapQuest.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the MapQuest geocoding services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IOptions{TOptions}"/> of <see cref="IMapQuestGeocoding"/></item>
        /// <item><see cref="IMapQuestGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the MapQuest services to.</param>
        /// <returns>An <see cref="MapQuestBuilder"/> to configure the MapQuest geocoding.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static MapQuestBuilder AddMapQuestGeocoding(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Configure<MapQuestOptions>(x =>
            {
                x.Key = string.Empty;
                x.UseLicensedEndpoint = false;
            });

            return new MapQuestBuilder(services.AddHttpClient<IMapQuestGeocoding, MapQuestGeocoding>());
        }
    }
}
