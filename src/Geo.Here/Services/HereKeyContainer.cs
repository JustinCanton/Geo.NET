// <copyright file="HereKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Services
{
    using Geo.Here.Abstractions;

    /// <summary>
    /// A container class for keeping the here API key.
    /// </summary>
    public class HereKeyContainer : IHereKeyContainer
    {
        private readonly string _key = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="HereKeyContainer"/> class.
        /// </summary>
        /// <param name="key">The key to use for here requests.</param>
        public HereKeyContainer(string key)
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
