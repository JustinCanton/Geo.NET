// <copyright file="NominatimBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Extensions.DependencyInjection
{
    using System;
    using Geo.Nominatim.Settings;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Options for the MapQuest configuration.
    /// </summary>
    public class NominatimBuilder : BaseBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NominatimBuilder"/> class.
        /// </summary>
        /// <param name="httpClientBuilder">An <see cref="IHttpClientBuilder"/> to configure the http client.</param>
        public NominatimBuilder(IHttpClientBuilder httpClientBuilder)
            : base(httpClientBuilder)
        {
        }

        /// <summary>
        /// Adds an email address to use for API calls to Nominatim, for identification purposes.
        /// </summary>
        /// <param name="email">The email address to use.</param>
        /// <returns>The modified <see cref="NominatimBuilder"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when the email is null, empty, or consists only of white-space characters.</exception>
        public NominatimBuilder AddEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("The email cannot be null or empty");
            }

            HttpClientBuilder.Services.Configure<NominatimOptions>(x =>
            {
                x.Email = email;
            });

            return this;
        }
    }
}
