// <copyright file="LookupParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a lookup request.
    /// </summary>
    public class LookupParameters : BaseParameters
    {
        /// <summary>
        /// Gets or sets the location ID, which is the ID of a result item eg. of a Discover request.
        /// </summary>
        public string Id { get; set; }
    }
}
