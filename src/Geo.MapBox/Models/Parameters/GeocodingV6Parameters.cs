// <copyright file="GeocodingV6Parameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Parameters
{
    using System.Collections.Generic;
    using System.Globalization;
    using Geo.MapBox.Enums;
    using Geo.MapBox.Models;

    /// <summary>
    /// The parameters possible to use during a Mapbox Geocoding API v6 forward geocoding request.
    /// </summary>
    public class GeocodingV6Parameters : IKeyParameters, IAdditionalParameters
    {
        /// <summary>
        /// Gets or sets the search text to geocode.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to store results permanently.
        /// Must be true when using the Permanent endpoint (requires an enterprise plan). Default is false. Optional.
        /// </summary>
        public bool Permanent { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to enable autocomplete suggestions. Optional.
        /// </summary>
        public bool? Autocomplete { get; set; }

        /// <summary>
        /// Gets or sets a bounding box to limit results. Optional.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets the list of countries to limit the request to. Optional.
        /// </summary>
        public IList<RegionInfo> Countries { get; } = new List<RegionInfo>();

        /// <summary>
        /// Gets or sets the response format. Accepted values: geojson, v5. Defaults to geojson. Optional.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets the list of languages of the text supplied in responses. Optional.
        /// </summary>
        public IList<CultureInfo> Languages { get; } = new List<CultureInfo>();

        /// <summary>
        /// Gets or sets the maximum number of results to return. Default is 5. Optional.
        /// </summary>
        public uint? Limit { get; set; }

        /// <summary>
        /// Gets or sets a location to bias results toward. Format: longitude,latitude or "ip". Optional.
        /// </summary>
        public string Proximity { get; set; }

        /// <summary>
        /// Gets a list used to filter results to include only a subset of available feature types. Optional.
        /// </summary>
        public IList<FeatureType> Types { get; } = new List<FeatureType>();

        /// <summary>
        /// Gets or sets the worldview to use.
        /// Available values: ar, cn, in, jp, ma, rs, ru, tr, us. Defaults to us. Optional.
        /// </summary>
        public string Worldview { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include building entrance data (beta feature). Optional.
        /// </summary>
        public bool? Entrances { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }

        /// <inheritdoc/>
        public IDictionary<string, string> AdditionalParameters { get; } = new Dictionary<string, string>();
    }
}
