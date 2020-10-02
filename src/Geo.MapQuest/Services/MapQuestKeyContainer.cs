// <copyright file="MapQuestKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Services
{
    using Geo.MapQuest.Abstractions;

    /// <summary>
    /// A container class for keeping the MapQuest API key.
    /// </summary>
    public class MapQuestKeyContainer : IMapQuestKeyContainer
    {
        private readonly string _key = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapQuestKeyContainer"/> class.
        /// </summary>
        /// <param name="key">The key to use for MapQuest requests.</param>
        public MapQuestKeyContainer(string key)
        {
            _key = key;
        }

        /// <inheritdoc/>
        public string GetKey()
        {
            return _key;
        }
    }
}
