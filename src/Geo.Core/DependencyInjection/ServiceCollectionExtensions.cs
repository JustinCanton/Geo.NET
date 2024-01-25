// <copyright file="ServiceCollectionExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the key options for a specified service.
        /// </summary>
        /// <typeparam name="T">The type of the service using the key options.</typeparam>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configure">Optional. A delegate that is used to configure an <see cref="KeyOptions{T}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static IServiceCollection AddKeyOptions<T>(
            this IServiceCollection services,
#if NETSTANDARD2_0
            Action<KeyOptions<T>> configure = null)
#else
            Action<KeyOptions<T>>? configure = null)
#endif
            where T : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.Configure<KeyOptions<T>>(configure ?? DefaultKeyConfigureOptions<T>);
        }

        /// <summary>
        /// Adds the client credentials options for a specified service.
        /// </summary>
        /// <typeparam name="T">The type of the service using the key options.</typeparam>
        /// <param name="services">An <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configure">Optional. A delegate that is used to configure an <see cref="ClientCredentialsOptions{T}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is null.</exception>
        public static IServiceCollection AddClientCredentialsOptions<T>(
            this IServiceCollection services,
#if NETSTANDARD2_0
            Action<ClientCredentialsOptions<T>> configure = null)
#else
            Action<ClientCredentialsOptions<T>>? configure = null)
#endif
            where T : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.Configure<ClientCredentialsOptions<T>>(configure ?? DefaultClientCredentialsConfigureOptions<T>);
        }

        private static void DefaultKeyConfigureOptions<T>(KeyOptions<T> options)
            where T : class
        {
            options.Key = string.Empty;
        }

        private static void DefaultClientCredentialsConfigureOptions<T>(ClientCredentialsOptions<T> options)
            where T : class
        {
            options.ClientId = string.Empty;
            options.ClientSecret = string.Empty;
        }
    }
}
