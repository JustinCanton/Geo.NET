// <copyright file="BoundingBox.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Responses
{
    using System.Text.Json.Serialization;
    using Geo.Bing.Converters;

    /// <summary>
    /// A bounding box is defined by two latitudes and two longitudes that represent the four sides of a rectangular area on the Earth.
    /// </summary>
    [JsonConverter(typeof(BoundingBoxConverter))]
    public class BoundingBox
    {
        /// <summary>
        /// Gets or sets the south-most latitude point of the bounding box.
        /// </summary>
        [JsonPropertyName("southLatitude")]
        public double SouthLatitude { get; set; }

        /// <summary>
        /// Gets or sets the west-most longitude point of the bounding box.
        /// </summary>
        [JsonPropertyName("westLongitude")]
        public double WestLongitude { get; set; }

        /// <summary>
        /// Gets or sets the north-most latitude point of the bounding box.
        /// </summary>
        [JsonPropertyName("northLatitude")]
        public double NorthLatitude { get; set; }

        /// <summary>
        /// Gets or sets the east-most longitude point of the bounding box.
        /// </summary>
        [JsonPropertyName("eastLongitude")]
        public double EastLongitude { get; set; }
    }
}
