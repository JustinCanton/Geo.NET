// <copyright file="AddressType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Indicates the type of the address.
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// Indicates the returned addres type is unknown.
        /// </summary>
        [EnumMember(Value = "unknown")]
        Unknown,

        /// <summary>
        /// Indicates a precise street address.
        /// </summary>
        [EnumMember(Value = "street_address")]
        StreetAddress,

        /// <summary>
        /// Indicates a named route (such as "US 101").
        /// </summary>
        [EnumMember(Value = "route")]
        Route,

        /// <summary>
        /// Indicates a major intersection, usually of two major roads.
        /// </summary>
        [EnumMember(Value = "intersection")]
        Intersection,

        /// <summary>
        /// Indicates a political entity. Usually, this type indicates a polygon of some civil administration.
        /// </summary>
        [EnumMember(Value = "political")]
        Political,

        /// <summary>
        /// Indicates the national political entity, and is typically the highest order type returned by the Geocoder.
        /// </summary>
        [EnumMember(Value = "country")]
        Country,

        /// <summary>
        /// Indicates a first-order civil entity below the country level. Within the United States, these administrative levels are states.
        /// Not all nations exhibit these administrative levels.
        /// In most cases, AdministrativeArea1 short names will closely match ISO 3166-2 subdivisions and other widely circulated lists;
        /// however this is not guaranteed as our geocoding results are based on a variety of signals and location data.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_1")]
        AdministrativeArea1,

        /// <summary>
        /// Indicates a second-order civil entity below the country level. Within the United States, these administrative levels are counties.
        /// Not all nations exhibit these administrative levels.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_2")]
        AdministrativeArea2,

        /// <summary>
        /// Indicates a third-order civil entity below the country level. This type indicates a minor civil division.
        /// Not all nations exhibit these administrative levels.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_3")]
        AdministrativeArea3,

        /// <summary>
        /// Indicates a fourth-order civil entity below the country level. This type indicates a minor civil division.
        /// Not all nations exhibit these administrative levels.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_4")]
        AdministrativeArea4,

        /// <summary>
        /// Indicates a fifth-order civil entity below the country level. This type indicates a minor civil division.
        /// Not all nations exhibit these administrative levels.
        /// </summary>
        [EnumMember(Value = "administrative_area_level_5")]
        AdministrativeArea5,

        /// <summary>
        /// Indicates a commonly-used alternative name for the entity.
        /// </summary>
        [EnumMember(Value = "colloquial_area")]
        ColloquialArea,

        /// <summary>
        /// Indicates an incorporated city or town political entity.
        /// </summary>
        [EnumMember(Value = "locality")]
        Locality,

        /// <summary>
        /// Indicates a first-order civil entity below a locality.
        /// </summary>
        [EnumMember(Value = "sublocality")]
        Sublocality,

        /// <summary>
        /// Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
        /// </summary>
        [EnumMember(Value = "sublocality_level_1")]
        Sublocality1,

        /// <summary>
        /// Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
        /// </summary>
        [EnumMember(Value = "sublocality_level_2")]
        Sublocality2,

        /// <summary>
        /// Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
        /// </summary>
        [EnumMember(Value = "sublocality_level_3")]
        Sublocality3,

        /// <summary>
        /// Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
        /// </summary>
        [EnumMember(Value = "sublocality_level_4")]
        Sublocality4,

        /// <summary>
        /// Each sublocality level is a civil entity. Larger numbers indicate a smaller geographic area.
        /// </summary>
        [EnumMember(Value = "sublocality_level_5")]
        Sublocality5,

        /// <summary>
        /// Indicates a named neighbourhood.
        /// </summary>
        [EnumMember(Value = "neighborhood")]
        Neighbourhood,

        /// <summary>
        /// Indicates a named location, usually a building or collection of buildings with a common name.
        /// </summary>
        [EnumMember(Value = "premise")]
        Premise,

        /// <summary>
        /// Indicates a first-order entity below a named location, usually a singular building within a collection of buildings with a common name.
        /// </summary>
        [EnumMember(Value = "subpremise")]
        Subpremise,

        /// <summary>
        /// Indicates an encoded location reference, derived from latitude and longitude.
        /// Plus codes can be used as a replacement for street addresses in places where they do not exist (where buildings are not numbered or streets are not named).
        /// See https://plus.codes for details.
        /// </summary>
        [EnumMember(Value = "plus_code")]
        PlusCode,

        /// <summary>
        /// Indicates a postal code as used to address postal mail within the country.
        /// </summary>
        [EnumMember(Value = "postal_code")]
        PostalCode,

        /// <summary>
        /// Indicates a prominent natural feature.
        /// </summary>
        [EnumMember(Value = "natural_feature")]
        NaturalFeature,

        /// <summary>
        /// Indicates an airport.
        /// </summary>
        [EnumMember(Value = "airport")]
        Airport,

        /// <summary>
        /// Indicates a named park.
        /// </summary>
        [EnumMember(Value = "park")]
        Park,

        /// <summary>
        /// Indicates a named point of interest. Typically, these "POI"s are prominent local entities that don't easily fit in another category, such as "Empire State Building" or "Eiffel Tower".
        /// </summary>
        [EnumMember(Value = "point_of_interest")]
        PointOfInterest,

        /// <summary>
        /// Indicates the floor of a building address.
        /// </summary>
        [EnumMember(Value = "floor")]
        Floor,

        /// <summary>
        /// Typically indicates a place that has not yet been categorized.
        /// </summary>
        [EnumMember(Value = "establishment")]
        Establishment,

        /// <summary>
        /// Indicates a parking lot or parking structure.
        /// </summary>
        [EnumMember(Value = "parking")]
        Parking,

        /// <summary>
        /// Indicates a specific postal box.
        /// </summary>
        [EnumMember(Value = "post_box")]
        PostBox,

        /// <summary>
        /// Indicates a grouping of geographic areas, such as locality and sublocality, used for mailing addresses in some countries.
        /// </summary>
        [EnumMember(Value = "postal_town")]
        PostalTown,

        /// <summary>
        /// Indicates the room of a building address.
        /// </summary>
        [EnumMember(Value = "room")]
        Room,

        /// <summary>
        /// Indicates the precise street number.
        /// </summary>
        [EnumMember(Value = "street_number")]
        StreetNumber,

        /// <summary>
        /// Indicates the location of a bus stop.
        /// </summary>
        [EnumMember(Value = "bus_station")]
        BusStation,

        /// <summary>
        /// Indicates the location of a train stop.
        /// </summary>
        [EnumMember(Value = "train_station")]
        TrainStation,

        /// <summary>
        /// Indicates the location of a public transit stop.
        /// </summary>
        [EnumMember(Value = "transit_station")]
        TransitStation,

        /// <summary>
        /// Indicates the location of a public store.
        /// </summary>
        [EnumMember(Value = "store")]
        Store,

        /// <summary>
        /// Indicates the postal code suffix.
        /// </summary>
        [EnumMember(Value = "postal_code_suffix")]
        PostalCodeSuffix,

        /// <summary>
        /// Indicates the location has an atm.
        /// </summary>
        [EnumMember(Value = "atm")]
        Atm,

        /// <summary>
        /// Indicates the location has an bank.
        /// </summary>
        [EnumMember(Value = "bank")]
        Bank,

        /// <summary>
        /// Indicates the location is a financial institution.
        /// </summary>
        [EnumMember(Value = "finance")]
        Finance,
    }
}
