// <copyright file="ClientCredentialsOptions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo
{
    /// <summary>
    /// Options for the API client credentials configuration.
    /// </summary>
    /// <typeparam name="T">The type of the consuming class using the options.</typeparam>
    public class ClientCredentialsOptions<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the client id associated with the API calls.
        /// </summary>
#if NETSTANDARD2_0
        public string ClientId { get; set; }
#else
        public string? ClientId { get; set; }
#endif

        /// <summary>
        /// Gets or sets the client secret associated with the API calls.
        /// </summary>
#if NETSTANDARD2_0
        public string ClientSecret { get; set; }
#else
        public string? ClientSecret { get; set; }
#endif
    }
}
