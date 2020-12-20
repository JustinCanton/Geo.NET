// <copyright file="AddressAttributeParameter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    /// <summary>
    /// The geocoding address parameters.
    /// </summary>
    public class AddressAttributeParameter
    {
        /// <summary>
        /// Gets or sets a UNIQUE value used for identifying the address.
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the single line representation of the address.
        /// If this is passed in, the other fields will be ignored.
        /// </summary>
        public string SingleLine { get; set; }

        /// <summary>
        /// Gets or sets the address of the location.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood of the location.
        /// </summary>
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the city of the location.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the subregion of the location.
        /// </summary>
        public string Subregion { get; set; }

        /// <summary>
        /// Gets or sets the region of the location.
        /// </summary>
        public string Region { get; set; }
    }
}
