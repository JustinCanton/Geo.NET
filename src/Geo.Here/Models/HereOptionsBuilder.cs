// <copyright file="HereOptionsBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
{
    using System;

    /// <summary>
    /// Options for the here API dependency injection.
    /// </summary>
    public class HereOptionsBuilder
    {
        /// <summary>
        /// Gets the key associated with the here API calls.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Sets the here API key to be used during calls.
        /// </summary>
        /// <param name="key">The here API key to use.</param>
        /// <returns>A <see cref="HereOptionsBuilder"/> configured with the key.</returns>
        /// <exception cref="ArgumentException">If the key passed in is null or empty.</exception>
        public HereOptionsBuilder UseKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The here API key can not be null or empty");
            }

            Key = key;
            return this;
        }
    }
}
