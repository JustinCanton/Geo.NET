// <copyright file="KeyOptions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo
{
    /// <summary>
    /// Options for the API key configuration.
    /// </summary>
    /// <typeparam name="T">The type of the consuming class using the options.</typeparam>
    public class KeyOptions<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the key associated with the API calls.
        /// </summary>
#if NETSTANDARD2_0
        public string Key { get; set; }
#else
        public string? Key { get; set; }
#endif
    }
}
