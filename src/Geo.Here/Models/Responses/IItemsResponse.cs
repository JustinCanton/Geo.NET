// <copyright file="IItemsResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Interface for HERE API responses that contain an items collection.
    /// </summary>
    /// <typeparam name="T">The type of the items in the response.</typeparam>
    public interface IItemsResponse<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the collection of items returned by the HERE API response.
        /// </summary>
        [JsonPropertyName("items")]
        IList<T> Items { get; set; }
    }
}
