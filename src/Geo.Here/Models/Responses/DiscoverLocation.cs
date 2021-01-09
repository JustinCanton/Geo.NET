// <copyright file="DiscoverLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Discover location matches.
    /// </summary>
    public class DiscoverLocation : GeocodeLocation
    {
        /// <summary>
        /// Gets or sets the related ontology id.
        /// </summary>
        [JsonProperty("ontologyId")]
        public string OntologyId { get; set; }

        /// <summary>
        /// Gets the list of chains assigned to this place.
        /// </summary>
        [JsonProperty("chains")]
        public IList<Chain> Chains { get; } = new List<Chain>();

        /// <summary>
        /// Gets the list of supplier references available for this place.
        /// </summary>
        [JsonProperty("references")]
        public IList<Reference> References { get; } = new List<Reference>();

        /// <summary>
        /// Gets the list of contact information like phone, email, WWW.
        /// </summary>
        [JsonProperty("contacts")]
        public IList<Contact> Contacts { get; } = new List<Contact>();

        /// <summary>
        /// Gets a list of hours during which the place is open for business.
        /// </summary>
        [JsonProperty("openingHours")]
        public IList<Hours> OpeningHours { get; } = new List<Hours>();

        /// <summary>
        /// Gets the phonemes for address and place names.
        /// </summary>
        [JsonProperty("phonemes")]
        public IList<LocationPhoneme> Phonemes { get; } = new List<LocationPhoneme>();
    }
}
