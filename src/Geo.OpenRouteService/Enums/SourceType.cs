// <copyright file="SourceType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The data source types available to filter ORS geocoding results.
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// OpenStreetMap data.
        /// </summary>
        [EnumMember(Value = "osm")]
        Osm = 1,

        /// <summary>
        /// OpenAddresses data.
        /// </summary>
        [EnumMember(Value = "oa")]
        Oa,

        /// <summary>
        /// GeoNames data.
        /// </summary>
        [EnumMember(Value = "gn")]
        Gn,

        /// <summary>
        /// Who's On First data.
        /// </summary>
        [EnumMember(Value = "wof")]
        Wof,
    }
}
