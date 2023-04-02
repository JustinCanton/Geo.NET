// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models
{
    using System.Globalization;
    using Newtonsoft.Json;

    /// <summary>
    /// A latitude/longitude pair forming a coordinate.
    /// </summary>
    public class Coordinate : BaseBounding
    {
        /// <summary>
        /// Gets or sets the latitude of a location.
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of a location.
        /// </summary>
        [JsonProperty("lng")]
        public double Longitude { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1}", Latitude, Longitude);
        }
    }
}
