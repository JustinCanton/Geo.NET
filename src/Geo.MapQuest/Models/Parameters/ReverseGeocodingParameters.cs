// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingParameters : BaseParameters
    {
        /// <summary>
        /// Gets or sets the latitude,longitude of the point to reverse geocode.
        /// </summary>
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to return the nearest cross streets (intersection) to the given points.
        /// Default is false.
        /// </summary>
        public bool IncludeNearestIntersection { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether or not to return the speed limit and toll road data, if available.
        /// Defaut is false.
        /// </summary>
        public bool IncludeRoadMetadata { get; set; } = false;
    }
}
