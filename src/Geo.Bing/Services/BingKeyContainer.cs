// <copyright file="BingKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models
{
    using Geo.Bing.Abstractions;

    /// <summary>
    /// A container class for keeping the Bing API key.
    /// </summary>
    public class BingKeyContainer : IBingKeyContainer
    {
        private readonly string _key = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingKeyContainer"/> class.
        /// </summary>
        /// <param name="key">The key to use for Bing requests.</param>
        public BingKeyContainer(string key)
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
