// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Parameters
{
    /// <summary>
    /// Parameters for the Bing geocoding query.
    /// </summary>
    public class GeocodingParameters : ResultParameters, IKeyParameters
    {
        /// <summary>
        /// Gets or sets a string that contains information about a location, such as an address or landmark name.
        /// </summary>
        public string Query { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
