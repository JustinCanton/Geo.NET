// <copyright file="HouseNumberType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Possible house number types.
    /// </summary>
    public enum HouseNumberType
    {
        /// <summary>
        /// Indicates the address number has been interpolated.
        /// </summary>
        [EnumMember(Value = "interpolated")]
        Interpolated = 1,

        /// <summary>
        /// Indicates the address number is PA.
        /// </summary>
        [EnumMember(Value = "PA")]
        PA,
    }
}
