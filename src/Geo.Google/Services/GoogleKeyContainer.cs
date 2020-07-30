// <copyright file="GoogleKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Services
{
    using Geo.Google.Abstractions;

    /// <summary>
    /// A container class for keeping the Google API key.
    /// </summary>
    public class GoogleKeyContainer : IGoogleKeyContainer
    {
        private readonly string _key = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleKeyContainer"/> class.
        /// </summary>
        /// <param name="key">The key to use for Google requests.</param>
        public GoogleKeyContainer(string key)
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
