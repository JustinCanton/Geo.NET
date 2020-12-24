// <copyright file="HereKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Services
{
    using Geo.Here.Abstractions;

    /// <summary>
    /// A container class for keeping the HERE API key.
    /// </summary>
    public class HereKeyContainer : IHereKeyContainer
    {
        private readonly string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="HereKeyContainer"/> class.
        /// </summary>
        /// <param name="key">The key to use for HERE requests.</param>
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
