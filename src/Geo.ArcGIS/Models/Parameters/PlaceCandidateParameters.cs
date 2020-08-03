// <copyright file="PlaceCandidateParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
        /// </summary>
        public int MaximumLocations { get; set; } = 0;
    }
}
