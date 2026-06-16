// <copyright file="Region.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Enums
{
    /// <summary>
    /// The geographic region used to scope Trimble Maps API requests.
    /// </summary>
    public enum Region
    {
        /// <summary>North America (default).</summary>
        NorthAmerica = 0,

        /// <summary>Africa.</summary>
        Africa = 1,

        /// <summary>Asia.</summary>
        Asia = 2,

        /// <summary>Europe.</summary>
        Europe = 3,

        /// <summary>Oceania.</summary>
        Oceania = 4,

        /// <summary>South America.</summary>
        SouthAmerica = 5,

        /// <summary>Middle East.</summary>
        MiddleEast = 6,

        /// <summary>Global.</summary>
        Global = 7,
    }
}
