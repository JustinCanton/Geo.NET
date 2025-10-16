// <copyright file="LayerType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Specifies the available layer types for Nominatim reverse geocoding.
    /// </summary>
    public enum LayerType
    {
        /// <summary>
        /// All places that make up an address: address points with house numbers, streets, inhabited places
        /// (suburbs, villages, cities, states etc.) and administrative boundaries.
        /// </summary>
        [EnumMember(Value = "address")]
        Address,

        /// <summary>
        /// All point of interest.
        /// This includes classic points of interest like restaurants, shops, hotels but also less obvious features like recycling bins, guideposts or benches.
        /// </summary>
        [EnumMember(Value = "poi")]
        POI,

        /// <summary>
        /// Railway infrastructure like tracks.
        /// Note that in Nominatim's standard configuration, only very few railway features are imported into the database.
        /// </summary>
        [EnumMember(Value = "railway")]
        Railway,

        /// <summary>
        /// Features like rivers, lakes and mountains.
        /// </summary>
        [EnumMember(Value = "natural")]
        Natural,

        /// <summary>
        /// A catch-all for features not covered by the other layers.
        /// </summary>
        [EnumMember(Value = "manmade")]
        ManMade,
    }
}
