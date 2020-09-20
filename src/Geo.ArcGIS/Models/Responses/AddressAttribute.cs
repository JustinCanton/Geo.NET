﻿// <copyright file="AddressAttribute.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Attribute information returned when searching for an address.
    /// </summary>
    public class AddressAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the match address information.
        /// </summary>
        [JsonProperty("Match_addr")]
        public string MatchAddress { get; set; }

        /// <summary>
        /// Gets or sets the address type information.
        /// </summary>
        [JsonProperty("Addr_type")]
        public string AddressType { get; set; }
    }
}