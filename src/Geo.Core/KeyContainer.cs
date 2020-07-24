// <copyright file="KeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Core.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A abstract class for keeping an API key.
    /// </summary>
    public abstract class KeyContainer
    {
        private static Dictionary<string, string> _keys = new Dictionary<string, string>();

        /// <summary>
        /// Gets the current Google API key.
        /// </summary>
        /// <param name="type">A <see cref="Type"/> with the type of the key to fetch.</param>
        /// <returns>The API key.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the type is invalid.</exception>
        protected static string GetKey(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!_keys.ContainsKey(type.Name))
            {
                return string.Empty;
            }

            return _keys[type.Name];
        }

        /// <summary>
        /// Sets the API key.
        /// </summary>
        /// <param name="type">A <see cref="Type"/> with the type of the key to set.</param>
        /// <param name="key">The key to set.</param>
        /// <exception cref="ArgumentNullException">Thrown when the type is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the key tries to be overwritten.</exception>
        protected static void SetKey(Type type, string key)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (_keys.ContainsKey(type.Name))
            {
                throw new InvalidOperationException("The key has already been set and cannot be reset.");
            }

            _keys[type.Name] = key;
        }
    }
}
