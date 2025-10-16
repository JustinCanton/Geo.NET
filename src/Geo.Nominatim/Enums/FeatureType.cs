// <copyright file="FeatureType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Specifies the type of geographical feature for Nominatim geocoding.
    /// </summary>
    public enum FeatureType
    {
        /// <summary>
        /// No specific feature type.
        /// </summary>
        [EnumMember(Value = "none")]
        None,

        /// <summary>
        /// A country-level geographical feature.
        /// </summary>
        [EnumMember(Value = "country")]
        Country,

        /// <summary>
        /// A state or province-level geographical feature.
        /// </summary>
        [EnumMember(Value = "state")]
        State,

        /// <summary>
        /// A city-level geographical feature.
        /// </summary>
        [EnumMember(Value = "city")]
        City,

        /// <summary>
        /// A settlement or town-level geographical feature.
        /// </summary>
        [EnumMember(Value = "settlement")]
        Settlement,
    }
}
