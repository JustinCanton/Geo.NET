// <copyright file="AddressAttributeParameter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    /// <summary>
    /// The geocoding address parameters.
    /// </summary>
    public class AddressAttributeParameter : AddressParameters
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
    }
}
