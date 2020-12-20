// <copyright file="IGoogleKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Abstractions
{
    /// <summary>
    /// A container class for keeping the Google API key.
    /// </summary>
    public interface IGoogleKeyContainer
    {
        /// <summary>
        /// Gets the current Google API key.
        /// </summary>
        /// <returns>The Google API key.</returns>
        string GetKey();
    }
}