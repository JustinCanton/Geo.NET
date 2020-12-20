// <copyright file="MapBoxKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Services
{
    using Geo.MapBox.Abstractions;

    /// <summary>
    /// A container class for keeping the MapBox API key.
    /// </summary>
    public class MapBoxKeyContainer : IMapBoxKeyContainer
    {
        private readonly string _key = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapBoxKeyContainer"/> class.
        /// </summary>
        /// <param name="key">The key to use for MapBox requests.</param>
        public MapBoxKeyContainer(string key)
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
