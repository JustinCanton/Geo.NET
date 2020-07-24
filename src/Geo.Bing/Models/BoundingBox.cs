// <copyright file="BoundingBox.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models
{
    using Geo.Bing.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// A bounding box is defined by two latitudes and two longitudes that represent the four sides of a rectangular area on the Earth.
    /// </summary>
    [JsonConverter(typeof(BoundingBoxConverter))]
    public class BoundingBox
    {
        /// <summary>
        /// Gets or sets the south-most latitude point of the bounding box.
        /// </summary>
        [JsonProperty("southLatitude")]
        public double SouthLatitude { get; set; }

        /// <summary>
        /// Gets or sets the west-most longitude point of the bounding box.
        /// </summary>
        [JsonProperty("westLongitude")]
        public double WestLongitude { get; set; }

        /// <summary>
        /// Gets or sets the north-most latitude point of the bounding box.
        /// </summary>
        [JsonProperty("northLatitude")]
        public double NorthLatitude { get; set; }

        /// <summary>
        /// Gets or sets the east-most longitude point of the bounding box.
        /// </summary>
        [JsonProperty("eastLongitude")]
        public double EastLongitude { get; set; }
    }
}
