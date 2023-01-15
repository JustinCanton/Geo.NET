// <copyright file="GeoNETResourceStringProvider.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System.Globalization;
    using System.Resources;

    /// <summary>
    /// An implementation for the <see cref="IGeoNETResourceStringProvider"/>.
    /// </summary>
    public sealed class GeoNETResourceStringProvider : IGeoNETResourceStringProvider
    {
        private readonly ResourceManager _resourceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoNETResourceStringProvider"/> class.
        /// </summary>
        /// <param name="resourceManager">A <see cref="ResourceManager"/> used to fetch resources.</param>
        internal GeoNETResourceStringProvider(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        /// <inheritdoc/>
        public string GetString(string resourceName, params object[] args)
        {
            var resourceString = _resourceManager.GetString(resourceName, CultureInfo.InvariantCulture);

            if ((args?.Length ?? 0) > 0)
            {
                return string.Format(CultureInfo.InvariantCulture, resourceString, args);
            }

            return resourceString;
        }
    }
}
