// <copyright file="Supplier.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
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
