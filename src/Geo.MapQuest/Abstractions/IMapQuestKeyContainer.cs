// <copyright file="IMapQuestKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
