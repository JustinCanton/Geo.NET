// <copyright file="BingOptionsBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models
{
    using System;

    /// <summary>
    /// Options for the Bing API dependency injection.
    /// </summary>
    public class BingOptionsBuilder
    {
        /// <summary>
        /// Gets the key associated with the Bing API calls.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Sets the Bing API key to be used during calls.
        /// </summary>
        /// <param name="key">The Bing API key to use.</param>
        /// <returns>A <see cref="BingOptionsBuilder"/> configured with the key.</returns>
        /// <exception cref="ArgumentException">If the key passedf in is null or empty.</exception>
        public BingOptionsBuilder UseKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The Bing API key can not be null or empty");
            }

            Key = key;
            return this;
        }
    }
}
