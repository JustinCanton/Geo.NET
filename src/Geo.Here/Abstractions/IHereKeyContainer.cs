// <copyright file="IHereKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
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