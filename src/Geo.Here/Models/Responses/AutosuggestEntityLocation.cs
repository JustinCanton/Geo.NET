// <copyright file="AutosuggestEntityLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// autosuggest entity location matches.
    /// </summary>
    public class AutosuggestEntityLocation : PartialLocation
    {
        /// <summary>
        /// Gets or sets the distance from the search center to this result item in meters. For example: "172039".
        /// </summary>
        [JsonPropertyName("distance")]
        public long Distance { get; set; }

        /// <summary>
        /// Gets or sets the related ontology id.
        /// </summary>
        [JsonPropertyName("ontologyId")]
        public string OntologyId { get; set; }

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
        /// Gets or sets an item describing how the parts of the response element matched the input query.
        /// </summary>
        [JsonPropertyName("highlights")]
        public Highlight Highlight { get; set; }
    }
}
