// <copyright file="SupplierType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The supplier of information about a location.
    /// </summary>
    public enum SupplierType
    {
        /// <summary>
        /// Indicates the supplier of the information is a core reference.
        /// </summary>
        [EnumMember(Value = "core")]
        Core = 1,

        /// <summary>
        /// Indicates the supplier of the information is yelp.
        /// </summary>
        [EnumMember(Value = "yelp")]
        Yelp,

        /// <summary>
        /// Indicates the supplier of the information is trip advisor.
        /// </summary>
        [EnumMember(Value = "tripadvisor")]
        TripAdvisor,
    }
}
