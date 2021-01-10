// <copyright file="InternationalMode.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Enums
{
    /// <summary>
    /// The International Geocoder modes.
    /// </summary>
    public enum InternationalMode
    {
        /// <summary>
        /// Keeps the query as a 5-box and sends it to the TomTom International Geocoder as a 5-box.
        /// </summary>
        FiveBox,

        /// <summary>
        /// Converts the 5-box query into a 1-box query and sends it to the TomTom International Geocoder.
        /// </summary>
        OneBox,

        /// <summary>
        /// Handles the query in a way deemed most optimal by MapQuest. Currently, this converts a 5-box query to a 1-box query across the board.
        /// </summary>
        Auto,
    }
}
