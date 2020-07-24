// <copyright file="GoogleKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Services
{
    using System;
    using Geo.Core.Services;

    /// <summary>
    /// A container class for keeping the Google API key.
    /// </summary>
    public class GoogleKeyContainer : KeyContainer
    {
        /// <summary>
        /// Gets the current Google API key.
        /// </summary>
        /// <returns>The Google API key.</returns>
        public static string GetKey()
        {
            return GetKey(typeof(GoogleKeyContainer));
        }

        /// <summary>
        /// Sets the Google API key.
        /// </summary>
        /// <param name="key">The key to set.</param>
        /// <exception cref="InvalidOperationException">Thrown when the key tries to be overwritten.</exception>
        public static void SetKey(string key)
        {
            SetKey(typeof(GoogleKeyContainer), key);
        }
    }
}
