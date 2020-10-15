// <copyright file="PartialPlace.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A place item with partial information used in multiple place requests.
    /// </summary>
    public class PartialPlace : BasePlace
    {
        /// <summary>
        /// Gets or sets the opening hours of the place.
        /// </summary>
        [JsonProperty("opening_hours")]
        public OpeningHours OpeningHours { get; set; }
    }
}
