// <copyright file="IHereKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Abstractions
{
    /// <summary>
    /// A container class for keeping the HERE API key.
    /// </summary>
    public interface IHereKeyContainer
    {
        /// <summary>
        /// Gets the current HERE API key.
        /// </summary>
        /// <returns>The HERE API key.</returns>
        string GetKey();
    }
}
