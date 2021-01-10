// <copyright file="CoordinateParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    /// <summary>
    /// Parameter information using a coordinate to refine the search.
    /// </summary>
    public class CoordinateParameters : BaseParameters
    {
        /// <summary>
        /// Gets or sets the latitude/longitude around which to retrieve place information.
        /// </summary>
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets the distance (in meters) within which to return place results.
        /// The maximum allowed radius is 50,000 meters.
        /// </summary>
        public uint Radius { get; set; } = 0;
    }
}
