// <copyright file="LookupParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
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
