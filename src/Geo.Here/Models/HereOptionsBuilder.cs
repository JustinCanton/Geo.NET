// <copyright file="HereOptionsBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models
{
    using System;

    /// <summary>
    /// Options for the HERE API dependency injection.
    /// </summary>
    public class HereOptionsBuilder
    {
        /// <summary>
        /// Gets the key associated with the HERE API calls.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Sets the HERE API key to be used during calls.
        /// </summary>
        /// <param name="key">The HERE API key to use.</param>
        /// <returns>A <see cref="HereOptionsBuilder"/> configured with the key.</returns>
        /// <exception cref="ArgumentException">If the key passed in is null or empty.</exception>
        public HereOptionsBuilder UseKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The HERE API key cannot be null or empty");
            }

            Key = key;
            return this;
        }
    }
}
