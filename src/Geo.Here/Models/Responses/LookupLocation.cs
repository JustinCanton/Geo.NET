// <copyright file="LookupLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Lookup location matches.
    /// </summary>
    public class LookupLocation : FullLocation
    {
        /// <summary>
        /// Gets the list of chains assigned to this place.
        /// </summary>
        [JsonProperty("chains")]
        public List<Chain> Chains { get; } = new List<Chain>();

        /// <summary>
        /// Gets the list of supplier references available for this place.
        /// </summary>
        [JsonProperty("references")]
        public List<Reference> References { get; } = new List<Reference>();

        /// <summary>
        /// Gets the list of contact information like phone, email, WWW.
        /// </summary>
        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; } = new List<Contact>();

        /// <summary>
        /// Gets a list of hours during which the place is open for business..
        /// </summary>
        [JsonProperty("openingHours")]
        public List<Hours> OpeningHours { get; } = new List<Hours>();

        /// <summary>
        /// Gets the phonemes for address and place names.
        /// </summary>
        [JsonProperty("phonemes")]
        public List<LocationPhoneme> Phonemes { get; } = new List<LocationPhoneme>();

        /// <summary>
        /// Gets or sets the additional attributes are added for BYOD use cases. Can hold arbitrary data.
        /// </summary>
        [JsonProperty("additionalAttributes")]
        public object AdditionalAttributes { get; set; }
    }
}
