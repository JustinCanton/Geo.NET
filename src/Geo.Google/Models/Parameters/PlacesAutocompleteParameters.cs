// <copyright file="PlacesAutocompleteParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Google.Enums;

    /// <summary>
    /// Parameters used in an places autocomplete request.
    /// </summary>
    public class PlacesAutocompleteParameters : QueryAutocompleteParameters
    {
        /// <summary>
        /// Gets or sets a random string which identifies an autocomplete session for billing purposes.
        /// If this parameter is omitted from an autocomplete request, the request is billed independently.
        /// </summary>
        public string SessionToken { get; set; }

        /// <summary>
        /// Gets or sets the origin point from which to calculate straight-line distance to the destination (returned as distance_meters).
        /// If this value is omitted, straight-line distance will not be returned.
        /// </summary>
        public Coordinate Origin { get; set; }

        /// <summary>
        /// Gets the types of place results to return.
        /// If no type is specified, all types will be returned.
        /// </summary>
        public IList<PlaceType> Types { get; } = new List<PlaceType>();

        /// <summary>
        /// Gets or sets a components filter, and fully restricts the results from the geocoder.
        /// </summary>
        public Component Components { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to Return only those places that are strictly within the region defined by location and radius.
        /// This is a restriction, rather than a bias, meaning that results outside this region will not be returned even if they match the user input.
        /// </summary>
        public bool StrictBounds { get; set; } = false;
    }
}
