// <copyright file="SuggestParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    using Geo.ArcGIS.Models.Responses;

    /// <summary>
    /// A parameters object for the suggest ArcGIS request.
    /// </summary>
    public class SuggestParameters
    {
        /// <summary>
        /// Gets or sets the input text entered by a user, which is used by the suggest operation to generate a list of possible matches.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets an origin point that is used to prefer or boost geocoding candidates based on their proximity to the location.
        /// Candidates near the location are prioritized relative to those further away.
        /// </summary>
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets a place or address type that can be used to filter suggest results.
        /// The parameter supports input of single category values or multiple comma-separated values.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets a set of bounding box coordinates that limit the search area for suggestions to a specific region.
        /// </summary>
        public BoundingBox SearchExtent { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of candidates to return.
        /// The default value is 5.
        /// The maximum allowable is 15.
        /// The minimum is 1.
        /// If any other value is specified, the default value is used.
        /// </summary>
        public uint MaximumLocations { get; set; } = 5;
    }
}
