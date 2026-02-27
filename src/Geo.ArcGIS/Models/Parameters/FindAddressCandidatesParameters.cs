// <copyright file="FindAddressCandidatesParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    using System.Collections.Generic;
    using System.Globalization;
    using Geo.ArcGIS.Enums;
    using Geo.ArcGIS.Models.Responses;

    /// <summary>
    /// A parameters object for the ArcGIS findAddressCandidates request.
    /// Supports both single-line and structured (multi-field) address input.
    /// At least one of <see cref="SingleLineAddress"/> or a structured field (e.g. <see cref="Address"/>) must be provided.
    /// </summary>
    public class FindAddressCandidatesParameters : StorageParameters, IClientCredentialsParameters
    {
        // ── Single-line input ────────────────────────────────────────────────

        /// <summary>
        /// Gets or sets the address you want to geocode as a single line of text.
        /// Mutually exclusive with structured address fields; if both are supplied, single-line takes precedence.
        /// </summary>
        public string SingleLineAddress { get; set; }

        /// <summary>
        /// Gets or sets an ID attribute value that, along with <see cref="SingleLineAddress"/>, links a suggestion
        /// returned by the suggest operation to a specific address or place.
        /// </summary>
        public string MagicKey { get; set; }

        // ── Structured (multi-field) address input ───────────────────────────

        /// <summary>
        /// Gets or sets the first line of the street address (street name and house number).
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the second line of a street address.
        /// This can include a building name, suite, subunit, or place name.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the third line of a street address.
        /// This can include a building name, suite, subunit, or place name.
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood of the location.
        /// </summary>
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the city of the location.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the subregion (county or equivalent) of the location.
        /// </summary>
        public string Subregion { get; set; }

        /// <summary>
        /// Gets or sets the region (state, province, or equivalent) of the location.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the standard postal code for an address, typically a three- to six-digit alphanumeric code.
        /// </summary>
        public string Postal { get; set; }

        /// <summary>
        /// Gets or sets a postal code extension, such as the United States Postal Service ZIP+4 code.
        /// Provides finer resolution or higher accuracy when combined with <see cref="Postal"/>.
        /// </summary>
        public string PostalExt { get; set; }

        /// <summary>
        /// Gets or sets a value representing the country. Providing this value increases geocoding speed.
        /// Acceptable values include the full country name, the two-character country code, or the three-character country code.
        /// </summary>
        public string CountryCode { get; set; }

        // ── Common optional parameters ───────────────────────────────────────

        /// <summary>
        /// Gets or sets a comma-separated list of attribute fields to include in the response.
        /// Use <c>*</c> to return all fields.
        /// </summary>
        public string OutFields { get; set; } = "Match_addr,Addr_type";

        /// <summary>
        /// Gets or sets an origin point used to prefer or boost geocoding candidates based on their proximity to the location.
        /// Candidates near the location are prioritised relative to those further away.
        /// </summary>
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets a place or address type that can be used to filter results.
        /// The parameter supports a single category value or multiple comma-separated values.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets a set of bounding box coordinates that limit the search area to a specific region.
        /// </summary>
        public BoundingBox SearchExtent { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of candidates to return.
        /// Valid range is 1–50. A value of 0 (the default) means the parameter is not sent and the service default is used.
        /// </summary>
        public uint MaximumLocations { get; set; } = 0;

        /// <summary>
        /// Gets or sets the spatial reference of the x/y coordinates returned by a geocode request.
        /// For a list of valid WKID values, see
        /// https://developers.arcgis.com/rest/services-reference/geographic-coordinate-systems.htm.
        /// </summary>
        public int OutSpatialReference { get; set; }

        /// <summary>
        /// Gets or sets the language in which geocoded addresses are returned.
        /// Addresses in many countries are available in more than one language.
        /// See https://developers.arcgis.com/rest/geocode/api-reference/geocode-coverage.htm for valid language code values per country.
        /// </summary>
        public CultureInfo LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies whether the output geometry of PointAddress and Subaddress matches
        /// should be the rooftop point or the street entrance location.
        /// The default value is <see cref="LocationType.Rooftop"/>.
        /// </summary>
        public LocationType LocationType { get; set; } = LocationType.Rooftop;

        /// <summary>
        /// Gets or sets a configuration of output fields returned in a response by specifying which address
        /// component values should be included in output label fields.
        /// The default value is <see cref="PreferredLabelValue.PostalCity"/>.
        /// </summary>
        public PreferredLabelValue PreferredLabelValue { get; set; } = PreferredLabelValue.PostalCity;

        /// <summary>
        /// Gets a list of countries to restrict geocoding results to.
        /// When values are passed, all input addresses are geocoded within the specified countries only.
        /// You can specify multiple country codes to limit results to more than one country.
        /// </summary>
        public IList<RegionInfo> SourceCountry { get; } = new List<RegionInfo>();

        /// <inheritdoc/>
        public string ClientId { get; set; }

        /// <inheritdoc/>
        public string ClientSecret { get; set; }
    }
}
