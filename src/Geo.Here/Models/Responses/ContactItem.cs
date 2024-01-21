// <copyright file="ContactItem.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A single contact item for a location.
    /// </summary>
    public class ContactItem
    {
        /// <summary>
        /// Gets or sets the label of the contact item.
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the value of the contact item.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
