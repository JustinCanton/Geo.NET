// <copyright file="ParsedQuery.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The address components a Geoapify search text was parsed into.
    /// </summary>
    public class ParsedQuery
    {
        /// <summary>
        /// Gets or sets the parsed house number.
        /// </summary>
        [JsonPropertyName("housenumber")]
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the parsed street.
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the parsed postal code.
        /// </summary>
        [JsonPropertyName("postcode")]
        public string PostCode { get; set; }

        /// <summary>
        /// Gets or sets the parsed city.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the parsed state.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the parsed country.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the type of location the parsed text is expected to describe.
        /// </summary>
        [JsonPropertyName("expected_type")]
        public string ExpectedType { get; set; }
    }
}
