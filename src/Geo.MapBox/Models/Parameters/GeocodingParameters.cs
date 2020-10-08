// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a geocoding request.
    /// </summary>
    public class GeocodingParameters
    {
        /// <summary>
        /// Gets or sets the feature you’re trying to look up.
        /// This could be an address, a point of interest name, a city name, etc.
        /// When searching for points of interest, it can also be a category name (for example, “coffee shop”).
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to return autocomplete results (true, default) or not (false).
        /// When autocomplete is enabled, results will be included that start with the requested string, rather than just responses that match it exactly.
        /// For example, a query for India might return both India and Indiana with autocomplete enabled, but only India if it’s disabled.
        /// </summary>
        public bool ReturnAutocomplete { get; set; } = true;

        /// <summary>
        /// Gets or sets the bounding box that limits results to only those contained within the supplied bounding box.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Geocoding API should attempt approximate, as well as exact,
        /// matching when performing searches (true, default), or whether it should opt out of this behavior and only attempt exact matching (false).
        /// For example, the default setting might return Washington, DC for a query of wahsington, even though the query was misspelled.
        /// </summary>
        public bool FuzzyMatch { get; set; }

        /// <summary>
        /// Gets or sets a coordinate that will bias the response to favor results that are closer to this location.
        /// </summary>
        public Coordinate Proximity { get; set; }
    }
}
