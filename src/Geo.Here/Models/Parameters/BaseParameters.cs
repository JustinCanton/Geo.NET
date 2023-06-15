// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// The base parameters that are used with all HERE requests.
    /// </summary>
    public class BaseParameters
    {
        /// <summary>
        /// Gets or sets the language to be used for result rendering from a list of BCP47 compliant Language Codes.
        /// </summary>
        public CultureInfo Language { get; set; }

        /// <summary>
        /// Gets or sets a single ISO 3166-1 alpha-3 country code in all uppercase.
        /// If a valid 3-letter country code is provided for which GS7 does not have a dedicated political view, it will fallback to the default view.
        /// If an invalid value is provided for the politicalView parameter, GS7 will respond with a "400" error code.
        /// </summary>
        public string PoliticalView { get; set; }

        /// <summary>
        /// Gets a list of additional fields to be rendered in the response. Please note that some of the fields involve additional webservice calls and can increase the overall response time.
        /// Supported values are:
        /// <list type="bullet">
        /// <item>countryInfo: For each result item renders additional block with the country info, such as ISO 3166-1 alpha-2 and ISO 3166-1 alpha-3 country code.</item>
        /// <item>parsing</item>
        /// <item>streetInfo: For each result item renders additional block with the street name decomposed into its parts like the base name, the street type, etc.</item>
        /// <item>tz: Renders result items with additional time zone information.</item>
        /// </list>
        /// </summary>
        public IList<string> Show { get; } = new List<string>();
    }
}
