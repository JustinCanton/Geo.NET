// <copyright file="PlaceInformation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Responses
{
    /// <summary>
    /// The information about a location.
    /// </summary>
    public class PlaceInformation
    {
        /// <summary>
        /// Gets or sets a string of the IETF language tag of the query’s language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets a string representing the feature in the requested language, if specified.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a string representing the feature in the requested language, if specified, and its full result hierarchy.
        /// </summary>
        public string PlaceName { get; set; }
    }
}
