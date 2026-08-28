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
    /// A builder used to configure an instance of the Nominatim geocoding.
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
        /// Adds the base url of the Nominatim instance to send requests to.
        /// <para>
        /// Use this to point at a publicly hosted instance other than the OpenStreetMap Foundation one,
        /// or at a self hosted deployment. When this is not called, the public instance at
        /// <see cref="NominatimOptions.DefaultServer"/> is used.
        /// </para>
        /// </summary>
        /// <param name="server">The base url of the Nominatim instance to use.</param>
        /// <returns>The modified <see cref="NominatimBuilder"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when the server is null, empty, consists only of white-space characters, or is not a valid absolute url.</exception>
        public NominatimBuilder AddServer(string server)
        {
            if (string.IsNullOrWhiteSpace(server))
            {
                throw new ArgumentException("The server cannot be null or empty");
            }

            if (!Uri.TryCreate(server, UriKind.Absolute, out _))
            {
                throw new ArgumentException("The server is not a valid absolute url");
            }

            HttpClientBuilder.Services.Configure<NominatimOptions>(x =>
            {
                x.Server = server;
            });

            return this;
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

        /// <summary>
        /// Adds the User-Agent header sent with API calls to Nominatim.
        /// <para>
        /// The usage policy of the public Nominatim instance requires requests to identify the calling application.
        /// </para>
        /// </summary>
        /// <param name="userAgent">The user agent to identify the calling application with.</param>
        /// <returns>The modified <see cref="NominatimBuilder"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when the user agent is null, empty, or consists only of white-space characters.</exception>
        public NominatimBuilder AddUserAgent(string userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
            {
                throw new ArgumentException("The user agent cannot be null or empty");
            }

            HttpClientBuilder.Services.Configure<NominatimOptions>(x =>
            {
                x.UserAgent = userAgent;
            });

            return this;
        }
    }
}
