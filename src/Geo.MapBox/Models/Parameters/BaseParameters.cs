// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Parameters
{
    using System.Collections.Generic;
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
        /// Permitted values are ISO 3166 alpha 2 country codes.
        /// </summary>
        public List<string> Countries { get; } = new List<string>();

        /// <summary>
        /// Gets the language of the text supplied in responses.
        /// Options are IETF language tags comprised of a mandatory ISO 639-1 language code and, optionally, one or more IETF subtags for country or script.
        /// </summary>
        public List<string> Languages { get; } = new List<string>();

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
        public List<FeatureType> Types { get; } = new List<FeatureType>();
    }
}
