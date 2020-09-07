// <copyright file="ResultType.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Possible result types.
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Indicates an administrative area location.
        /// </summary>
        [EnumMember(Value = "administrativeArea")]
        AdministrativeArea = 1,

        /// <summary>
        /// Indicates an incorporated city or town political entity.
        /// </summary>
        [EnumMember(Value = "locality")]
        Locality,

        /// <summary>
        /// Indicates a first-order civil entity below a locality.
        /// </summary>
        [EnumMember(Value = "street")]
        Street,

        /// <summary>
        /// Indicates an area with a group of addresses.
        /// </summary>
        [EnumMember(Value = "addressBlock")]
        AddressBlock,

        /// <summary>
        /// Indicates a house.
        /// </summary>
        [EnumMember(Value = "houseNumber")]
        HouseNumber,

        /// <summary>
        /// Indicates a place.
        /// </summary>
        [EnumMember(Value = "place")]
        Place,
    }
}
