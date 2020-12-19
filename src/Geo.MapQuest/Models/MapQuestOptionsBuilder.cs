// <copyright file="MapQuestOptionsBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Models
{
    using System;

    /// <summary>
    /// Options for the MapQuest API dependency injection.
    /// </summary>
    public class MapQuestOptionsBuilder
    {
        /// <summary>
        /// Gets the key associated with the MapQuest API calls.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to use the lisenced data endpoints with the MapQuest API calls.
        /// The default is false.
        /// </summary>
        public bool UseLicensedEndpoint { get; private set; } = false;

        /// <summary>
        /// Sets the MapQuest API key to be used during calls.
        /// </summary>
        /// <param name="key">The MapQuest API key to use.</param>
        /// <returns>A <see cref="MapQuestOptionsBuilder"/> configured with the key.</returns>
        /// <exception cref="ArgumentException">If the key passed in is null or empty.</exception>
        public MapQuestOptionsBuilder UseKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The MapQuest API key cannot be null or empty");
            }

            Key = key;
            return this;
        }

        /// <summary>
        /// Sets the MapQuest endpoint to be used during calls.
        /// </summary>
        /// <returns>A <see cref="MapQuestOptionsBuilder"/> configured with the information about which endpoints to use.</returns>
        public MapQuestOptionsBuilder UseLicensedEndpoints()
        {
            UseLicensedEndpoint = true;
            return this;
        }
    }
}
