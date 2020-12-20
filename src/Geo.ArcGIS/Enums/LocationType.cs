// <copyright file="LocationType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Different geometry matches.
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// A rooftop point.
        /// </summary>
        [EnumMember(Value = "rooftop")]
        Rooftop,

        /// <summary>
        /// A street entrance location.
        /// </summary>
        [EnumMember(Value = "street")]
        Street,
    }
}
