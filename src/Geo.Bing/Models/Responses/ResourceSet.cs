// <copyright file="ResourceSet.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A ResourceSet is a container of Resources returned by the request.
    /// </summary>
    public class ResourceSet
    {
        /// <summary>
        /// Gets or sets an estimate of the total number of resources in the ResourceSet.
        /// </summary>
        [JsonProperty("estimatedTotal")]
        public long EstimatedTotal { get; set; }

        /// <summary>
        /// Gets a collection of one or more resources. The resources that are returned depend on the request.
        /// </summary>
        [JsonProperty("resources")]
        public List<Location> Resources { get; } = new List<Location>();
    }
}
