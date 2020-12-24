// <copyright file="IMapQuestKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Abstractions
{
    /// <summary>
    /// A container class for keeping the MapQuest API key.
    /// </summary>
    public interface IMapQuestKeyContainer
    {
        /// <summary>
        /// Gets the current MapQuest API key.
        /// </summary>
        /// <returns>The MapQuest API key.</returns>
        string GetKey();
    }
}
