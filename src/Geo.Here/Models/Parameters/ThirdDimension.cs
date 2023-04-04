// <copyright file="ThirdDimension.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    /// <summary>
    /// 3rd dimension specification.
    /// Example a level, altitude, elevation or some other custom value.
    /// ABSENT is default when there is no third dimension en/decoding required.
    /// </summary>
    public enum ThirdDimension
    {
        /// <summary>
        /// The third dimension is absent.
        /// </summary>
        Absent = 0,

        /// <summary>
        /// The third dimension is level.
        /// </summary>
        Level = 1,

        /// <summary>
        /// The third dimension uses altitude.
        /// </summary>
        Altitude = 2,

        /// <summary>
        /// The third dimension uses elevation.
        /// </summary>
        Elevation = 3,

        /// <summary>
        /// A reserved number.
        /// </summary>
        Reserved1 = 4,

        /// <summary>
        /// A reserved number.
        /// </summary>
        Reserved2 = 5,

        /// <summary>
        /// A custom number.
        /// </summary>
        Custom1 = 6,

        /// <summary>
        /// A custom number.
        /// </summary>
        Custom2 = 7,
    }
}
