// <copyright file="Currency.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Currency associated with the location result.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Gets or sets the currency symbol of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the currency code of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the currency name of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the numeric currency code of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("numeric")]
        public uint? Numeric { get; set; }

        /// <summary>
        /// Gets or sets the minor currency unit of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("minor_unit")]
        public uint? MinorUnit { get; set; }
    }
}
