// <copyright file="PlaceCandidateParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    using Geo.ArcGIS.Models.Responses;

    /// <summary>
    /// A parameters object for the place candidates ArcGIS request.
    /// </summary>
    public class PlaceCandidateParameters : StorageParameters
    {
        /// <summary>
        /// gets or sets the type of places to search for such as Restaurants, Coffee Shop, Gas Stations.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the x and y coordinate or the geometry used to conduct the search.
        /// </summary>
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of candidates to return.
        /// The maximum allowable is 50.
        /// The minimum is 1.
        /// If any other value is specified, then all matching candidates up to the service maximum are returned.
        /// </summary>
        public uint MaximumLocations { get; set; } = 0;
    }
}
