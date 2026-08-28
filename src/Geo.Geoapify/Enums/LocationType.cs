// <copyright file="LocationType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The location types available to restrict Geoapify geocoding results to.
    /// </summary>
    public enum LocationType
    {
        /// <summary>
        /// A country.
        /// </summary>
        [EnumMember(Value = "country")]
        Country = 1,

        /// <summary>
        /// A state, province, or equivalent first level administrative area.
        /// </summary>
        [EnumMember(Value = "state")]
        State,

        /// <summary>
        /// A city, town, or village.
        /// </summary>
        [EnumMember(Value = "city")]
        City,

        /// <summary>
        /// A postal code area.
        /// </summary>
        [EnumMember(Value = "postcode")]
        PostCode,

        /// <summary>
        /// A street.
        /// </summary>
        [EnumMember(Value = "street")]
        Street,

        /// <summary>
        /// A named place such as a building, shop, or point of interest.
        /// </summary>
        [EnumMember(Value = "amenity")]
        Amenity,

        /// <summary>
        /// An administrative area such as a district, suburb, or neighbourhood.
        /// </summary>
        [EnumMember(Value = "locality")]
        Locality,
    }
}
