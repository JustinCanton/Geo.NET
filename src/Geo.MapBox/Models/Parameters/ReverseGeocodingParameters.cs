// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Models.Parameters
{
    using Geo.MapBox.Enums;

    /// <summary>
    /// The parameters possible to use during a reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingParameters : BaseParameters
    {
        /// <summary>
        /// Gets or sets the location being queried.
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Gets or sets how results are sorted in a reverse geocoding query if multiple results are requested using a limit other than 1.
        /// </summary>
        public ReverseMode ReverseMode { get; set; }
    }
}
