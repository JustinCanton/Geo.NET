// <copyright file="BoundingBox.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models
{
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
            return $"{WestLongitude},{NorthLatitude},{EastLongitude},{SouthLatitude}";
        }
    }
}
