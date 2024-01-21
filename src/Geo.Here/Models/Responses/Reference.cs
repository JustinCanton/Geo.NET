// <copyright file="Reference.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A reference about a location.
    /// </summary>
    public class Reference
    {
        /// <summary>
        /// Gets or sets the information about the supplier of this reference.
        /// </summary>
        [JsonPropertyName("supplier")]
        public Supplier Supplier { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the place as provided by the supplier.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
