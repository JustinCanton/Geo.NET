// <copyright file="FlexiblePolyline.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The polyline information to be added as a filter in the geocoding request.
    /// </summary>
    public class FlexiblePolyline
    {
        /// <summary>
        /// Gets or sets the coordinates that make up the polyline.
        /// </summary>
        public IEnumerable<LatLngZ> Coordinates { get; set; } = Array.Empty<LatLngZ>();

        /// <summary>
        /// Gets or sets the precision of the coordinate to be encoded.
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// Gets or sets the third dimension, which may be a level, altitude, elevation or some other custom value.
        /// </summary>
        public ThirdDimension ThirdDimension { get; set; }

        /// <summary>
        /// Gets or sets precision for <see cref="ThirdDimension"/> value.
        /// </summary>
        public int ThirdDimensionPrecision { get; set; }
    }
}
