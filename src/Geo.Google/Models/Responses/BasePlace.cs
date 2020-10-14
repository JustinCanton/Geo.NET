// <copyright file="BasePlace.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The base fields shared across place requests.
    /// </summary>
    public class BasePlace
    {
        /// <summary>
        /// Gets or sets the human-readable name for the returned result. For establishment results, this is usually the business name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the geometry information about the result, generally including the location (geocode) of the place and (optionally) the viewport identifying its general area of coverage.
        /// </summary>
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }
}
