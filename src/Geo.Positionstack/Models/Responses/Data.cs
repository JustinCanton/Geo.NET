// <copyright file="Data.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The data with the address results.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Gets the address results.
        /// </summary>
        [JsonPropertyName("results")]
        public IList<Address> Results { get; } = new List<Address>();
    }
}
