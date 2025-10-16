// <copyright file="PolygonOutput.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Enums
{
    /// <summary>
    /// Specifies the available output formats for polygon data in Nominatim queries.
    /// </summary>
    public enum PolygonOutput
    {
        /// <summary>
        /// No polygon output.
        /// </summary>
        None,

        /// <summary>
        /// Output polygon data in GeoJSON format.
        /// </summary>
        GeoJSON,

        /// <summary>
        /// Output polygon data in KML format.
        /// </summary>
        KML,

        /// <summary>
        /// Output polygon data in SVG format.
        /// </summary>
        SVG,

        /// <summary>
        /// Output polygon data in WKT (Well-Known Text) format.
        /// </summary>
        WKT,
    }
}
