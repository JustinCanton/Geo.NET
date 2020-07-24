// <copyright file="BingKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models
{
    using System;
    using Geo.Core.Services;

    /// <summary>
    /// A container class for keeping the Bing API key.
    /// </summary>
    public class BingKeyContainer : KeyContainer
    {
        /// <summary>
        /// Gets the current Bing API key.
        /// </summary>
        /// <returns>The Bing API key.</returns>
        public static string GetKey()
        {
            return GetKey(typeof(BingKeyContainer));
        }

        /// <summary>
        /// Sets the Bing API key.
        /// </summary>
        /// <param name="key">The key to set.</param>
        /// <exception cref="InvalidOperationException">Thrown when the key tries to be overwritten.</exception>
        public static void SetKey(string key)
        {
            SetKey(typeof(BingKeyContainer), key);
        }
    }
}
