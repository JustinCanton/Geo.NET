// <copyright file="DiscoverParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

using System.Collections.Generic;

namespace Geo.Here.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a discover request.
    /// </summary>
    public class DiscoverParameters : IAreaParameters, IKeyParameters
    {
        /// <summary>
        /// Gets or sets a free-text query.
        /// Examples:
        /// 125, Berliner, berlin
        /// Beacon, Boston, Hospital
        /// Schnurrbart German Pub and Restaurant, Hong Kong.
        /// </summary>
        public string Query { get; set; }

        /// <inheritdoc/>
        public string InCountry { get; set; }

        /// <inheritdoc/>
        public Circle InCircle { get; set; }

        /// <inheritdoc/>
        public BoundingBox InBoundingBox { get; set; }

        /// <inheritdoc/>
        public string Route { get; set; }

        /// <inheritdoc/>
        public FlexiblePolyline FlexiblePolyline { get; set; }

        /// <inheritdoc/>
        public Coordinate At { get; set; }

        /// <inheritdoc/>
        public uint Limit { get; set; }

        /// <inheritdoc/>
        public System.Globalization.CultureInfo Language { get; set; }

        /// <inheritdoc/>
        public string PoliticalView { get; set; }

        /// <inheritdoc/>
        public IList<string> Show { get; } = new List<string>();

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
