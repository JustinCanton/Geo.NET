// <copyright file="LatLngZ.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
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
        /// <param name="thirdDimension">Optional. The Z dimension of the point. Default = 0.</param>
        public LatLngZ(double latitude, double longitude, double thirdDimension = 0)
        {
            Latitude = latitude;
            Longitude = longitude;
            Z = thirdDimension;
        }

        /// <summary>
        /// Gets the latitude of the point.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Gets the longitude of the point.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Gets the Z dimension of the point.
        /// </summary>
        public double Z { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "LatLngZ [Latitude=" + Latitude + ", Longitude=" + Longitude + ", Z=" + Z + "]";
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
                if (latLngZ.Latitude == Latitude && latLngZ.Longitude == Longitude && latLngZ.Z == Z)
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
#if NETSTANDARD2_1_OR_GREATER
            return HashCode.Combine(Latitude, Longitude, Z);
#else
            unchecked
            {
                int hc = -1817952719;
                hc = (-1521134295 * hc) + Latitude.GetHashCode();
                hc = (-1521134295 * hc) + Longitude.GetHashCode();
                hc = (-1521134295 * hc) + Z.GetHashCode();
                return hc;
            }
#endif
        }
    }
}
