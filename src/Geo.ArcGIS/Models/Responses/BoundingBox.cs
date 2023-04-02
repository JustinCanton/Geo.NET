// <copyright file="BoundingBox.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models
{
    using System.Globalization;
    using Newtonsoft.Json;

    /// <summary>
    /// The bounding box information for a location.
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        /// Gets or sets the south-most latitude point of the bounding box.
        /// </summary>
        [JsonProperty("ymin")]
        public double SouthLatitude { get; set; }

        /// <summary>
        /// Gets or sets the west-most longitude point of the bounding box.
        /// </summary>
        [JsonProperty("xmin")]
        public double WestLongitude { get; set; }

        /// <summary>
        /// Gets or sets the north-most latitude point of the bounding box.
        /// </summary>
        [JsonProperty("ymax")]
        public double NorthLatitude { get; set; }

        /// <summary>
        /// Gets or sets the east-most longitude point of the bounding box.
        /// </summary>
        [JsonProperty("xmax")]
        public double EastLongitude { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", WestLongitude, NorthLatitude, EastLongitude, SouthLatitude);
        }
    }
}
