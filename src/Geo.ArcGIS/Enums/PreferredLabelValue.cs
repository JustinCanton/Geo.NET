// <copyright file="PreferredLabelValue.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Preferred labelling information in the response.
    /// </summary>
    public enum PreferredLabelValue
    {
        /// <summary>
        /// Include the primary postal city value in geocoding response output fields. This is the primary name assigned to the postal code of the address.
        /// </summary>
        [EnumMember(Value = "postalCity")]
        PostalCity,

        /// <summary>
        /// Include the primary local city name in geocoding response output fields. This is the name of the city that the address is actually within, and may be different than the postal city.
        /// </summary>
        [EnumMember(Value = "localCity")]
        LocalCity,
    }
}
