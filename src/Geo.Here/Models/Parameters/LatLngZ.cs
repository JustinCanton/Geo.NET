// <copyright file="LatLngZ.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System;

    /// <summary>
    /// A coordinate triple.
    /// </summary>
    public class LatLngZ
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LatLngZ"/> class.
        /// </summary>
        /// <param name="latitude">The latitude of the point.</param>
        /// <param name="longitude">The longitude of the point.</param>
        /// <param name="thirdDimension">The Z dimension of the point.</param>
        public LatLngZ(double latitude, double longitude, double thirdDimension = 0)
        {
            Lat = latitude;
            Lng = longitude;
            Z = thirdDimension;
        }

        /// <summary>
        /// Gets the latitude of the point.
        /// </summary>
        public double Lat { get; }

        /// <summary>
        /// Gets the longitude of the point.
        /// </summary>
        public double Lng { get; }

        /// <summary>
        /// Gets the Z dimension of the point.
        /// </summary>
        public double Z { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "LatLngZ [lat=" + Lat + ", lng=" + Lng + ", z=" + Z + "]";
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj is LatLngZ latLngZ)
            {
                if (latLngZ.Lat == Lat && latLngZ.Lng == Lng && latLngZ.Z == Z)
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Lat, Lng, Z);
        }
    }
}
