// <copyright file="LocationType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Additional data about the specified location.
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// Indicates the returned location type is unknown.
        /// </summary>
        [EnumMember(Value = "unknown")]
        Unknown,

        /// <summary>
        /// Indicates that the returned result is a precise geocode for which we have location information accurate down to street address precision.
        /// </summary>
        [EnumMember(Value = "ROOFTOP")]
        Rooftop,

        /// <summary>
        /// Indicates that the returned result reflects an approximation (usually on a road) interpolated between two precise points (such as intersections).
        /// Interpolated results are generally returned when rooftop geocodes are unavailable for a street address.
        /// </summary>
        [EnumMember(Value = "RANGE_INTERPOLATED")]
        RangeInterpolated,

        /// <summary>
        /// Indicates that the returned result is the geometric center of a result such as a polyline (for example, a street) or polygon (region).
        /// </summary>
        [EnumMember(Value = "GEOMETRIC_CENTER")]
        GeometricCenter,

        /// <summary>
        /// Indicates that the returned result is approximate.
        /// </summary>
        [EnumMember(Value = "APPROXIMATE")]
        Approximate,
    }
}
