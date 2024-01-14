// <copyright file="Contact.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A group of contact information for a location.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Gets a list of phone numbers associated with a location.
        /// </summary>
        [JsonPropertyName("phone")]
        public IList<ContactItem> Phones { get; } = new List<ContactItem>();

        /// <summary>
        /// Gets a list of mobile numbers associated with a location.
        /// </summary>
        [JsonPropertyName("mobile")]
        public IList<ContactItem> Mobiles { get; } = new List<ContactItem>();

        /// <summary>
        /// Gets a list of toll free numbers associated with a location.
        /// </summary>
        [JsonPropertyName("tollFree")]
        public IList<ContactItem> TollFrees { get; } = new List<ContactItem>();

        /// <summary>
        /// Gets a list of fax numbers associated with a location.
        /// </summary>
        [JsonPropertyName("fax")]
        public IList<ContactItem> Faxes { get; } = new List<ContactItem>();

        /// <summary>
        /// Gets a list of websites associated with a location.
        /// </summary>
        [JsonPropertyName("www")]
        public IList<ContactItem> Websites { get; } = new List<ContactItem>();

        /// <summary>
        /// Gets a list of emails associated with a location.
        /// </summary>
        [JsonPropertyName("email")]
        public IList<ContactItem> Emails { get; } = new List<ContactItem>();
    }
}
