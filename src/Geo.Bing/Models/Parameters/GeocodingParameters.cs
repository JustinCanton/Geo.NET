// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models.Parameters
{
    /// <summary>
    /// Parameters for the Bing geocoding query.
    /// </summary>
    public class GeocodingParameters : ResultParameters
    {
        /// <summary>
        /// Gets or sets a string that contains information about a location, such as an address or landmark name.
        /// </summary>
        public string Query { get; set; }
    }
}
