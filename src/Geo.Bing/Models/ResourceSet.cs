// <copyright file="ResourceSet.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    /// <summary>
    /// A ResourceSet is a container of Resources returned by the request.
    /// </summary>
    public class ResourceSet
    {
        [JsonProperty("resources")]
        private readonly List<Location> _resources = new List<Location>();

        /// <summary>
        /// Gets or sets an estimate of the total number of resources in the ResourceSet.
        /// </summary>
        [JsonProperty("estimatedTotal")]
        public long EstimatedTotal { get; set; }

        /// <summary>
        /// Gets a collection of one or more resources. The resources that are returned depend on the request.
        /// </summary>
        public ReadOnlyCollection<Location> Resources
        {
            get
            {
                return _resources.AsReadOnly();
            }
        }

        /// <summary>
        /// Adds a location to the list of resources.
        /// </summary>
        /// <param name="location">A <see cref="Location"/> with the location to add.</param>
        public void AddResource(Location location)
        {
            _resources.Add(location);
        }
    }
}
