// <copyright file="BaseLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Geo.Here.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// The base resopnse items across all responses.
    /// </summary>
    public class BaseLocation
    {
        /// <summary>
        /// Gets or sets the localized display name of this result item.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the result item. This ID can be used for a Look Up by ID search as well.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the location.
        /// </summary>
        [JsonProperty("resultType")]
        public ResultType ResultType { get; set; }
    }
}
