// <copyright file="GoogleOptionsBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models
{
    using System;

    /// <summary>
    /// Options for the Google API dependency injection.
    /// </summary>
    public class GoogleOptionsBuilder
    {
        /// <summary>
        /// Gets the key associated with the Google API calls.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Sets the Google API key to be used during calls.
        /// </summary>
        /// <param name="key">The Google API key to use.</param>
        /// <returns>A <see cref="GoogleOptionsBuilder"/> configured with the key.</returns>
        /// <exception cref="ArgumentException">If the key passedf in is null or empty.</exception>
        public GoogleOptionsBuilder UseKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The Google API key can not be null or empty");
            }

            Key = key;
            return this;
        }
    }
}
