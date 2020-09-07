// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The base parameters possible for all here requests.
    /// </summary>
    public class BaseParameters
    {
        /// <summary>
        /// Gets or sets the center of the search context expressed as coordinates.
        /// Required parameter for endpoints that are expected to rank results by distance from the explicitly specified search center.
        /// Example: -13.163068,-72.545128 (Machu Picchu Mountain, Peru).
        /// </summary>
        public Coordinate At { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to be returned.
        /// </summary>
        [Range(0, 100)]
        public uint Limit { get; set; } = 0;

        /// <summary>
        /// Gets or sets the language to be used for result rendering from a list of BCP47 compliant Language Codes.
        /// </summary>
        public string Language { get; set; }
    }
}
