// <copyright file="Options.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The options sent in with the request.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the max number of locations to return from the geocode.
        /// The default is 5.
        /// </summary>
        [JsonProperty("maxResults")]
        public int MaxResults { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a URL to a static map thumbnail image for a location being geocoded should be returned.
        /// Default is true.
        /// </summary>
        [JsonProperty("thumbMaps")]
        public bool ThumbMaps { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service should fail when given a latitude/longitude pair in an address or batch geocode call,
        /// or if it should ignore that and try and geocode what it can.
        /// The default value is false.
        /// </summary>
        [JsonProperty("ignoreLatLngInput")]
        public bool IgnoreLatLngInput { get; set; }
    }
}
