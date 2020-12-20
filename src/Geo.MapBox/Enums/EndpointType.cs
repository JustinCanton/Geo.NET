// <copyright file="EndpointType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Enums
{
    /// <summary>
    /// The possible endpoints to request data from.
    /// </summary>
    public enum EndpointType
    {
        /// <summary>
        /// Requests to the Places endpoint must be triggered by user activity.
        /// Any results must be displayed on a Mapbox map and cannot be stored permanently, as described in Mapbox’s terms of service. (https://www.mapbox.com/legal/tos)
        /// </summary>
        Places,

        /// <summary>
        /// Used for fetching data that can be stored and displayed on any map.
        /// </summary>
        Permenant,
    }
}
