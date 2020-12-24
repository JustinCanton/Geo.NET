// <copyright file="LocalityType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Possible locality types.
    /// </summary>
    public enum LocalityType
    {
        /// <summary>
        /// Indicates the locality is a specific postal code.
        /// </summary>
        [EnumMember(Value = "postalCode")]
        PostalCode = 1,

        /// <summary>
        /// Indicates the locality is a sub-district.
        /// </summary>
        [EnumMember(Value = "subdistrict")]
        Subdistrict,

        /// <summary>
        /// Indicates the locality is a district.
        /// </summary>
        [EnumMember(Value = "district")]
        District,

        /// <summary>
        /// Indicates the locality is a city.
        /// </summary>
        [EnumMember(Value = "city")]
        City,
    }
}
