// <copyright file="BrowseResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;

    /// <summary>
    /// The response from a browse request.
    /// </summary>
    public class BrowseResponse : IItemsResponse<BrowseLocation>
    {
        /// <inheritdoc/>
        public IList<BrowseLocation> Items { get; set; } = new List<BrowseLocation>();
    }
}
