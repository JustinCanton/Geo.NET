// <copyright file="PlaceType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Different search types for the places endpoint.
    /// </summary>
    public enum PlaceType
    {
        /// <summary>
        /// Indicates a geocoding result.
        /// </summary>
        [EnumMember(Value = "geocode")]
        Geocode,

        /// <summary>
        /// Indicates a geocoding result with a precise address.
        /// </summary>
        [EnumMember(Value = "address")]
        Address,

        /// <summary>
        /// Indicates a business result.
        /// </summary>
        [EnumMember(Value = "establishment")]
        Establishment,

        /// <summary>
        /// Indicates a region or area result.
        /// </summary>
        [EnumMember(Value = "regions")]
        Regions,

        /// <summary>
        /// Indicates a city result.
        /// </summary>
        [EnumMember(Value = "cities")]
        Cities,
    }
}
