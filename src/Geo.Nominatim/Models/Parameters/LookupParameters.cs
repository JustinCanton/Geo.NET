// <copyright file="LookupParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Nominatim.Enums;

    /// <summary>
    /// The parameters possible to use during a Nominatim lookup request.
    /// The lookup API allows to query the address and other details of one or multiple OSM objects like node, way or relation.
    /// </summary>
    public class LookupParameters : IBaseParameters, IPolygonParameters
    {
        /// <summary>
        /// Gets a comma-separated list of OSM ids each prefixed with its type, one of node(N), way(W) or relation(R).
        /// Up to 50 ids can be queried at the same time.
        /// Required parameter.
        /// </summary>
        /// <example>R146656,W104393803,N240109189.</example>
        public List<string> OsmIds { get; } = new List<string>();

        /// <inheritdoc/>
        public PolygonOutput PolygonOutput { get; set; }

        /// <inheritdoc/>
        public float PolygonThreshold { get; set; }

        /// <inheritdoc/>
        public bool? AddressDetails { get; set; }

        /// <inheritdoc/>
        public bool? ExtraInfo { get; set; }

        /// <inheritdoc/>
        public bool? NameDetails { get; set; }

        /// <inheritdoc/>
        public string AcceptLanguage { get; set; }

        /// <inheritdoc/>
        public string Email { get; set; }
    }
}
