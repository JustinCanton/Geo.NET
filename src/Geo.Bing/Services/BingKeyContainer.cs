// <copyright file="BingKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Services
{
    using Geo.Bing.Abstractions;

    /// <summary>
    /// A container class for keeping the Bing API key.
    /// </summary>
    public class BingKeyContainer : IBingKeyContainer
    {
        private readonly string _key = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingKeyContainer"/> class.
        /// </summary>
        /// <param name="key">The key to use for Bing requests.</param>
        public BingKeyContainer(string key)
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
