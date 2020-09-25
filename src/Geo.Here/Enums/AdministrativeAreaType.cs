// <copyright file="AdministrativeAreaType.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Possible administrative area types.
    /// </summary>
    public enum AdministrativeAreaType
    {
        /// <summary>
        /// Indicates the administrative area is a county.
        /// </summary>
        [EnumMember(Value = "county")]
        County = 1,

        /// <summary>
        /// Indicates the administrative area is a state.
        /// </summary>
        [EnumMember(Value = "state")]
        State,

        /// <summary>
        /// Indicates the administrative area is a country.
        /// </summary>
        [EnumMember(Value = "country")]
        Country,
    }
}
