// <copyright file="SuggestParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    using System.Collections.Generic;
    using System.Globalization;
    using Geo.ArcGIS.Enums;
    using Geo.ArcGIS.Models.Responses;

    /// <summary>
    /// A parameters object for the suggest ArcGIS request.
    /// </summary>
    public class SuggestParameters
    {
        /// <summary>
        /// Gets or sets the input text entered by a user, which is used by the suggest operation to generate a list of possible matches.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets an origin point that is used to prefer or boost geocoding candidates based on their proximity to the location.
        /// Candidates near the location are prioritized relative to those further away.
        /// </summary>
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets a place or address type that can be used to filter suggest results.
        /// The parameter supports input of single category values or multiple comma-separated values.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets a set of bounding box coordinates that limit the search area for suggestions to a specific region.
        /// </summary>
        public BoundingBox SearchExtent { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of candidates to return.
        /// The default value is 5.
        /// The maximum allowable is 15.
        /// The minimum is 1.
        /// If any other value is specified, the default value is used.
        /// </summary>
        public uint MaximumLocations { get; set; } = 5;

        /// <summary>
        /// Gets or sets a value representing the country. Providing this value increases geocoding speed.
        /// Acceptable values include the full country name in English or the official language of the country, the two-character country code, or the three-character country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets a value representing the country. When a value is passed for this parameter,
        /// all of the addresses in the input table are sent to the specified country to be geocoded.
        /// You can specify multiple country codes to limit results to more than one country.
        /// </summary>
        public IList<RegionInfo> SourceCountry { get; } = new List<RegionInfo>();

        /// <summary>
        /// Gets or sets a configuration of output fields returned in a response from the
        /// ArcGIS World Geocoding Service by specifying which address component values should be included in output fields.
        /// The default value is postal city.
        /// </summary>
        public PreferredLabelValue PreferredLabelValue { get; set; } = PreferredLabelValue.PostalCity;
    }
}
