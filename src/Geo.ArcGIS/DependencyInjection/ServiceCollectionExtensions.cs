// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.DependencyInjection
{
    using System;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the ArcGIS services to the service collection.
        /// <para>
        /// Adds the services:
        /// <list type="bullet">
        /// <item><see cref="IOptions{TOptions}"/> of <see cref="IArcGISGeocoding"/></item>
        /// <item><see cref="IArcGISTokenProvider"/></item>
        /// <item><see cref="IArcGISGeocoding"/></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the ArcGIS services to.</param>
        /// <returns>An <see cref="ClientCredentialsBuilder{T}"/> to configure the ArcGIS geocoding.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static ClientCredentialsBuilder<IArcGISGeocoding> AddArcGISGeocoding(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services
                .AddClientCredentialsOptions<IArcGISGeocoding>()
                .AddHttpClient<IArcGISTokenProvider, ArcGISTokenProvider>();

            return new ClientCredentialsBuilder<IArcGISGeocoding>(services.AddHttpClient<IArcGISGeocoding, ArcGISGeocoding>());
        }
    }
}
