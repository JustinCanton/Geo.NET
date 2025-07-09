// <copyright file="ReverseGeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;

    /// <summary>
    /// The response from a reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingResponse : IItemsResponse<GeocodeLocation>
    {
        /// <inheritdoc/>
        public IList<GeocodeLocation> Items { get; set; } = new List<GeocodeLocation>();
    }
}
