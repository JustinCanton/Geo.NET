// <copyright file="CountryAbbrevType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Enums
{
    /// <summary>
    /// The format used for country abbreviations in Trimble Maps API responses.
    /// </summary>
    public enum CountryAbbrevType
    {
        /// <summary>FIPS country code.</summary>
        FIPS,

        /// <summary>ISO 3166-1 alpha-2 (two-letter) country code.</summary>
        ISO2,

        /// <summary>ISO 3166-1 alpha-3 (three-letter) country code.</summary>
        ISO3,

        /// <summary>GENC two-letter country code.</summary>
        GENC2,

        /// <summary>GENC three-letter country code.</summary>
        GENC3,
    }
}
