// <copyright file="ContactItem.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// A single contact item for a location.
    /// </summary>
    public class ContactItem
    {
        /// <summary>
        /// Gets or sets the label of the contact item.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the value of the contact item.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
