// <copyright file="HouseNumberType.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
