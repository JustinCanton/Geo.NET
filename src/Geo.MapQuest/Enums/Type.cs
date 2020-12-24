// <copyright file="Type.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An enum for specifying the type of the location.
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// Indicates the location type is unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Indicates the location is a stop.
        /// </summary>
        [EnumMember(Value = "s")]
        Stop,

        /// <summary>
        /// Indicates the location is a via.
        /// </summary>
        [EnumMember(Value = "v")]
        Via,
    }
}
