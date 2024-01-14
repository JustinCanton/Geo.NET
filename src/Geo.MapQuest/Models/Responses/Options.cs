// <copyright file="Options.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The options sent in with the request.
    /// </summary>
#pragma warning disable CA1724 // The type name Options conflicts
    public class Options
#pragma warning restore CA1724 // The type name Options conflicts
    {
        /// <summary>
        /// Gets or sets the max number of locations to return from the geocode.
        /// The default is 5.
        /// </summary>
        [JsonPropertyName("maxResults")]
        public int MaxResults { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a URL to a static map thumbnail image for a location being geocoded should be returned.
        /// Default is true.
        /// </summary>
        [JsonPropertyName("thumbMaps")]
        public bool ThumbMaps { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service should fail when given a latitude/longitude pair in an address or batch geocode call,
        /// or if it should ignore that and try and geocode what it can.
        /// The default value is false.
        /// </summary>
        [JsonPropertyName("ignoreLatLngInput")]
        public bool IgnoreLatLngInput { get; set; }
    }
}
