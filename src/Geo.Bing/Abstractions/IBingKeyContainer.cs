// <copyright file="IBingKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Abstractions
{
    /// <summary>
    /// A container class for keeping the Bing API key.
    /// </summary>
    public interface IBingKeyContainer
    {
        /// <summary>
        /// Gets the current Bing API key.
        /// </summary>
        /// <returns>The Bing API key.</returns>
        string GetKey();
    }
}
