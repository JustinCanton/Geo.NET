// <copyright file="KeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Services
{
    using System;

    /// <summary>
    /// A container class for keeping the Google API key.
    /// </summary>
    public static class KeyContainer
    {
        private static string _key = string.Empty;

        /// <summary>
        /// Gets the current Google API key.
        /// </summary>
        /// <returns>The Google API key.</returns>
        public static string GetKey()
        {
            return _key;
        }

        /// <summary>
        /// Sets the Google API key.
        /// </summary>
        /// <param name="key">The key to use.</param>
        /// <exception cref="InvalidOperationException">Thrown when the key tries to be overwritten.</exception>
        public static void SetKey(string key)
        {
            if (!string.IsNullOrWhiteSpace(_key))
            {
                throw new InvalidOperationException("The key has already been set and cannot be reset.");
            }

            _key = key;
        }
    }
}
