// <copyright file="RankType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The different ranking/ordering types for nearby searches.
    /// </summary>
    public enum RankType
    {
        /// <summary>
        /// Indicates to sort results based on their importance.
        /// Ranking will favor prominent places within the specified area.
        /// </summary>
        [EnumMember(Value = "prominence")]
        Prominence,

        /// <summary>
        /// Indicates tobiases search results in ascending order by their distance from the specified location.
        /// </summary>
        [EnumMember(Value = "distance")]
        Distance,
    }
}
