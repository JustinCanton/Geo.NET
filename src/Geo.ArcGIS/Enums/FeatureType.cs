// <copyright file="FeatureType.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Possible features to return.
    /// </summary>
    public enum FeatureType
    {
        /// <summary>
        /// A street intersection feature.
        /// </summary>
        [EnumMember(Value = "StreetInt")]
        StreetIntersection,

        /// <summary>
        /// A distance marker feature.
        /// </summary>
        [EnumMember(Value = "DistanceMarker")]
        DistanceMarker,

        /// <summary>
        /// A street address feature.
        /// </summary>
        [EnumMember(Value = "StreetAddress")]
        StreetAddress,

        /// <summary>
        /// A street name feature.
        /// </summary>
        [EnumMember(Value = "StreetName")]
        StreetName,

        /// <summary>
        /// A point of interest feature.
        /// </summary>
        [EnumMember(Value = "POI")]
        POI,

        /// <summary>
        /// A point address feature.
        /// </summary>
        [EnumMember(Value = "PointAddress")]
        PointAddress,

        /// <summary>
        /// A postal feature.
        /// </summary>
        [EnumMember(Value = "Postal")]
        Postal,

        /// <summary>
        /// A locality feature.
        /// </summary>
        [EnumMember(Value = "Locality")]
        Locality,
    }
}
