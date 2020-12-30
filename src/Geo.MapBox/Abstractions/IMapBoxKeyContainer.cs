// <copyright file="IMapBoxKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Abstractions
{
    /// <summary>
    /// A container class for keeping the MapBox API key.
    /// </summary>
    public interface IMapBoxKeyContainer
    {
        /// <summary>
        /// Gets the current MapBox API key.
        /// </summary>
        /// <returns>The MapBox API key.</returns>
        string GetKey();
    }
}
