// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Parameters
{
    using System.Collections.Generic;
    using System.Globalization;
    using Geo.MapBox.Enums;

    /// <summary>
    /// The parameters possible to use for all requests.
    /// </summary>
    public class BaseParameters
    {
        /// <summary>
        /// Gets or sets the type of endpoint to call.
        /// The default is non-permanent.
        /// </summary>
        public EndpointType EndpointType { get; set; } = EndpointType.Places;

        /// <summary>
        /// Gets the list of countries to limit the request to.
        /// </summary>
        public IList<RegionInfo> Countries { get; } = new List<RegionInfo>();

        /// <summary>
        /// Gets the language of the text supplied in responses.
        /// </summary>
        public IList<CultureInfo> Languages { get; } = new List<CultureInfo>();

        /// <summary>
        /// Gets or sets the maximum number of results to return.
        /// The default is 1 and the maximum supported is 5.
        /// </summary>
        public uint Limit { get; set; } = 1;

        /// <summary>
        /// Gets or sets a value indicating whether to request additional metadata about the recommended navigation destination corresponding to the feature (true) or not (false, default).
        /// </summary>
        public bool Routing { get; set; } = false;

        /// <summary>
        /// Gets a list used to filter results to include only a subset (one or more) of the available feature types.
        /// </summary>
        public IList<FeatureType> Types { get; } = new List<FeatureType>();

        /// <summary>
        /// Gets or sets an optional parameter to identify geographic features whose characteristics are defined differently by audiences belonging to various regional, cultural, or political groups.
        /// Specifies to return features that are defined differently by audiences that belong to various regional, cultural, or political groups.
        /// Available worldviews are: ar,cn,in,jp,ma,ru,tr,us. If worldview is not set, the us worldview boundaries are returned by default.
        /// </summary>
        public string Worldview { get; set; }
    }
}
