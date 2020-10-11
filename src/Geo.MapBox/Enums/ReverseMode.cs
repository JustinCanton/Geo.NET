// <copyright file="ReverseMode.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Enums
{
    /// <summary>
    /// The possible modes for results from a reverse geocoding request.
    /// </summary>
    public enum ReverseMode
    {
        /// <summary>
        /// The closest feature to always be returned first.
        /// </summary>
        Distance,

        /// <summary>
        /// High-prominence features to be sorted higher than nearer, lower-prominence features.
        /// </summary>
        Score,
    }
}
