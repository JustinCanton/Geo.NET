﻿// <copyright file="AddressAttribute.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Attribute information returned when searching for an address.
    /// </summary>
    public class AddressAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the match address information.
        /// </summary>
        [JsonPropertyName("Match_addr")]
        public string MatchAddress { get; set; }

        /// <summary>
        /// Gets or sets the address type information.
        /// </summary>
        [JsonPropertyName("Addr_type")]
        public string AddressType { get; set; }
    }
}
