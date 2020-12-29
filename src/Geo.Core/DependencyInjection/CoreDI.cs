// <copyright file="CoreDI.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Dependency injection methods for the Core functionality.
    /// </summary>
    public static class CoreDI
    {
        /// <summary>
        /// Adds the Core services to the service collection.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the Core services to.</param>
        /// <returns>A <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddLocalization();

            return services;
        }
    }
}
