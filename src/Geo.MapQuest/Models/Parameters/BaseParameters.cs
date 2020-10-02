// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Models.Parameters
{
    /// <summary>
    /// The base parameters used across all requests.
    /// </summary>
    public class BaseParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether a URL to a static map thumbnail image for a location being geocoded should be returned.
        /// Default is true.
        /// </summary>
        public bool IncludeThumbMaps { get; set; } = true;
    }
}
