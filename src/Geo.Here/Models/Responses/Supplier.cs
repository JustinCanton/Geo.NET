// <copyright file="Supplier.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Geo.Here.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// A supplier of reference information.
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Gets or sets the type of the location.
        /// </summary>
        [JsonProperty("id")]
        public SupplierType Id { get; set; }
    }
}
