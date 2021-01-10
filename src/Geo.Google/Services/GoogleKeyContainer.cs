// <copyright file="GoogleKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Services
{
    using Geo.Google.Abstractions;

    /// <summary>
    /// A container class for keeping the Google API key.
    /// </summary>
    public class GoogleKeyContainer : IGoogleKeyContainer
    {
        private readonly string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleKeyContainer"/> class.
        /// </summary>
        /// <param name="key">The key to use for Google requests.</param>
        public GoogleKeyContainer(string key)
        {
            _key = key;
        }

        /// <inheritdoc/>
        public string GetKey()
        {
            return _key;
        }
    }
}
