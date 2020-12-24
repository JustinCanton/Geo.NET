// <copyright file="SideOfStreet.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An enum for specifying the side of the street a location is on.
    /// </summary>
    public enum SideOfStreet
    {
        /// <summary>
        /// Indicates the location is on neither side of the street.
        /// </summary>
        [EnumMember(Value = "N")]
        None,

        /// <summary>
        /// Indicates the location is on the left side of the street.
        /// </summary>
        [EnumMember(Value = "L")]
        Left,

        /// <summary>
        /// Indicates the location is on the right side of the street.
        /// </summary>
        [EnumMember(Value = "R")]
        Right,

        /// <summary>
        /// Indicates the location is on both the left and right side of the street.
        /// </summary>
        [EnumMember(Value = "M")]
        Mixed,
    }
}
