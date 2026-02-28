// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Parameters
{
    using System.Collections.Generic;

    /// <summary>
    /// The base parameters used across all requests.
    /// </summary>
    public class BaseParameters : IAdditionalParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether a URL to a static map thumbnail image for a location being geocoded should be returned.
        /// Default is true.
        /// </summary>
        public bool IncludeThumbMaps { get; set; } = true;

        /// <inheritdoc/>
        public IDictionary<string, string> AdditionalParameters { get; } = new Dictionary<string, string>();
    }
}
