// <copyright file="MapBoxOptionsBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Models
{
    using System;

    /// <summary>
    /// Options for the MapBox API dependency injection.
    /// </summary>
    public class MapBoxOptionsBuilder
    {
        /// <summary>
        /// Gets the key associated with the MapBox API calls.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Sets the MapBox API key to be used during calls.
        /// </summary>
        /// <param name="key">The MapBox API key to use.</param>
        /// <returns>A <see cref="MapBoxOptionsBuilder"/> configured with the key.</returns>
        /// <exception cref="ArgumentException">If the key passed in is null or empty.</exception>
        public MapBoxOptionsBuilder UseKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The MapBox API key cannot be null or empty");
            }

            Key = key;
            return this;
        }
    }
}
