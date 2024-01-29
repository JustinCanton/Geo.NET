// <copyright file="Category.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A category assigned to a location.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the identifier number for an associated category. For example: "900-9300-0000".
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not it is a primary category. This field is visible only when the value is 'true'.
        /// </summary>
        [JsonPropertyName("primary")]
        public bool Primary { get; set; }
    }
}
