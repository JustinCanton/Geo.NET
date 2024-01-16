// <copyright file="LookupLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Lookup location matches.
    /// </summary>
    public class LookupLocation : FullLocation
    {
        /// <summary>
        /// Gets or sets the list of chains assigned to this place.
        /// </summary>
        [JsonPropertyName("chains")]
        public IList<Chain> Chains { get; set; } = new List<Chain>();

        /// <summary>
        /// Gets or sets the list of supplier references available for this place.
        /// </summary>
        [JsonPropertyName("references")]
        public IList<Reference> References { get; set; } = new List<Reference>();

        /// <summary>
        /// Gets or sets the list of contact information like phone, email, WWW.
        /// </summary>
        [JsonPropertyName("contacts")]
        public IList<Contact> Contacts { get; set; } = new List<Contact>();

        /// <summary>
        /// Gets or sets a list of hours during which the place is open for business..
        /// </summary>
        [JsonPropertyName("openingHours")]
        public IList<Hours> OpeningHours { get; set; } = new List<Hours>();

        /// <summary>
        /// Gets or sets the phonemes for address and place names.
        /// </summary>
        [JsonPropertyName("phonemes")]
        public IList<LocationPhoneme> Phonemes { get; set; } = new List<LocationPhoneme>();

        /// <summary>
        /// Gets or sets the additional attributes are added for BYOD use cases. Can hold arbitrary data.
        /// </summary>
        [JsonPropertyName("additionalAttributes")]
        public object AdditionalAttributes { get; set; }
    }
}
