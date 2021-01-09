// <copyright file="IKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Abstractions
{
    /// <summary>
    /// A container class for keeping an API key.
    /// </summary>
    public interface IKeyContainer
    {
        /// <summary>
        /// Gets the current API key.
        /// </summary>
        /// <returns>The API key.</returns>
        string GetKey();
    }
}
