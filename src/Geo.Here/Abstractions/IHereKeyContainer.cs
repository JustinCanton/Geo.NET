// <copyright file="IHereKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Abstractions
{
    /// <summary>
    /// A container class for keeping the here API key.
    /// </summary>
    public interface IHereKeyContainer
    {
        /// <summary>
        /// Gets the current here API key.
        /// </summary>
        /// <returns>The here API key.</returns>
        string GetKey();
    }
}