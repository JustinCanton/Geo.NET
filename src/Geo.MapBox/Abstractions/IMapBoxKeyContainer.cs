// <copyright file="IMapBoxKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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