// <copyright file="LayerType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The layer types available to filter ORS geocoding results.
    /// </summary>
    public enum LayerType
    {
        /// <summary>
        /// A venue such as a restaurant, shop, or point of interest.
        /// </summary>
        [EnumMember(Value = "venue")]
        Venue = 1,

        /// <summary>
        /// A specific address.
        /// </summary>
        [EnumMember(Value = "address")]
        Address,

        /// <summary>
        /// A street.
        /// </summary>
        [EnumMember(Value = "street")]
        Street,

        /// <summary>
        /// A neighbourhood within a locality.
        /// </summary>
        [EnumMember(Value = "neighbourhood")]
        Neighbourhood,

        /// <summary>
        /// A borough within a city.
        /// </summary>
        [EnumMember(Value = "borough")]
        Borough,

        /// <summary>
        /// A local administrative area.
        /// </summary>
        [EnumMember(Value = "localadmin")]
        LocalAdmin,

        /// <summary>
        /// A city or town.
        /// </summary>
        [EnumMember(Value = "locality")]
        Locality,

        /// <summary>
        /// A county.
        /// </summary>
        [EnumMember(Value = "county")]
        County,

        /// <summary>
        /// A macro county (larger grouping of counties).
        /// </summary>
        [EnumMember(Value = "macrocounty")]
        MacroCounty,

        /// <summary>
        /// A region such as a state or province.
        /// </summary>
        [EnumMember(Value = "region")]
        Region,

        /// <summary>
        /// A macro region (larger grouping of regions).
        /// </summary>
        [EnumMember(Value = "macroregion")]
        MacroRegion,

        /// <summary>
        /// A country.
        /// </summary>
        [EnumMember(Value = "country")]
        Country,

        /// <summary>
        /// A coarse result encompassing multiple administrative levels.
        /// </summary>
        [EnumMember(Value = "coarse")]
        Coarse,

        /// <summary>
        /// A postal code area.
        /// </summary>
        [EnumMember(Value = "postalcode")]
        PostalCode,
    }
}
