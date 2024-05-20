// <copyright file="PhoneInformation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Information about the phone for a country.
    /// </summary>
    public class PhoneInformation
    {
        /// <summary>
        /// Gets or sets the calling code of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("calling_code")]
        public string CallingCode { get; set; }

        /// <summary>
        /// Gets or sets the national calling prefix of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("national_prefix")]
        public string NationalPrefix { get; set; }

        /// <summary>
        /// Gets or sets the international calling prefix of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("international_prefix")]
        public string InternationalPrefix { get; set; }
    }
}
