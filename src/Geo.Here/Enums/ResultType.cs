// <copyright file="ResultType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
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
