// <copyright file="Component.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// A component object thhat can return address results restricted to a specific area.
    /// </summary>
    public class Component
    {
        /// <summary>
        /// Gets or sets the postal code value which matches postal_code and postal_code_prefix.
        /// </summary>
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the region information for a country.
        /// The API follows the ISO standard for defining countries, and the filtering works best when using the corresponding ISO code of the country.
        /// Note: If you receive unexpected results with a country code, verify that you are using a code which includes the countries,
        /// dependent territories, and special areas of geographical interest you intend.
        /// You can find code information at Wikipedia: List of ISO 3166 country codes (https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes)
        /// or the ISO Online Browsing Platform (https://www.iso.org/obp/ui/#search).
        /// </summary>
        public RegionInfo Country { get; set; } = null;

        /// <summary>
        /// Gets or sets a value that matches the long or short name of a route.
        /// </summary>
        public string Route { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that matches against locality and sublocality types.
        /// </summary>
        public string Locality { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that matches all the administrative_area levels.
        /// </summary>
        public string AdministrativeArea { get; set; } = string.Empty;

        /// <inheritdoc/>
        public override string ToString()
        {
            var components = new List<string>();
            if (!string.IsNullOrWhiteSpace(PostalCode))
            {
                components.Add($"postal_code:{PostalCode}");
            }

            if (Country != null)
            {
#pragma warning disable CA1308 // Normalize strings to uppercase
                components.Add($"country:{Country.TwoLetterISORegionName.ToLowerInvariant()}");
#pragma warning restore CA1308 // Normalize strings to uppercase
            }

            if (!string.IsNullOrWhiteSpace(Route))
            {
                components.Add($"route:{Route}");
            }

            if (!string.IsNullOrWhiteSpace(Locality))
            {
                components.Add($"locality:{Locality}");
            }

            if (!string.IsNullOrWhiteSpace(AdministrativeArea))
            {
                components.Add($"administrative_area:{AdministrativeArea}");
            }

            return string.Join("|", components);
        }
    }
}
