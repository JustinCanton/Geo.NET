// <copyright file="SearchParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Nominatim.Enums;

    /// <summary>
    /// The parameters possible to use during a Nominatim search request.
    /// The search API allows to look up a location from a textual description or address.
    /// </summary>
    public class SearchParameters : IBaseParameters, IPolygonParameters, ILayerParameter
    {
        /// <summary>
        /// Gets or sets the free-form query string to search for.
        /// Free-form queries are processed first left-to-right and then right-to-left if that fails.
        /// So you may search for pilkington avenue, birmingham as well as for birmingham, pilkington avenue.
        /// Commas are optional, but improve performance by reducing the complexity of the search.
        /// Required if structured parameters are not used.
        /// </summary>
        /// <example>pilkington avenue, birmingham.</example>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the amenity name and/or type of POI.
        /// Alternative to free-form query. Part of structured query.
        /// </summary>
        /// <example>135 pilkington avenue.</example>
        public string Amenity { get; set; }

        /// <summary>
        /// Gets or sets the street name and optionally a house number.
        /// Alternative to free-form query. Part of structured query.
        /// </summary>
        /// <example>135 pilkington avenue.</example>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// Alternative to free-form query. Part of structured query.
        /// </summary>
        /// <example>birmingham.</example>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the county name.
        /// Alternative to free-form query. Part of structured query.
        /// </summary>
        /// <example>west midlands.</example>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the state name.
        /// Alternative to free-form query. Part of structured query.
        /// </summary>
        /// <example>alabama.</example>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country name or country code.
        /// Alternative to free-form query. Part of structured query.
        /// </summary>
        /// <example>united kingdom.</example>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// Alternative to free-form query. Part of structured query.
        /// </summary>
        /// <example>B72 1LH.</example>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the preferred area to find search results in degrees.
        /// </summary>
        public BoundingBox ViewBox { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to restrict the results to items contained within the bounding box specified in ViewBox.
        /// When ViewBox and Bounded are used together, the search result will be restricted to the area.
        /// </summary>
        public bool? Bounded { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to return.
        /// Cannot be more than 40. Default: 10.
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// Gets a list of country codes to limit the search results to.
        /// Each country code must be a valid ISO 3166-1 alpha-2 code.
        /// </summary>
        /// <example>["us", "gb"].</example>
        public List<string> CountryCodes { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the type of geographical feature to search for.
        /// This property allows filtering search results by a specific feature type, such as country, state, city, or settlement.
        /// </summary>
        public FeatureType FeatureType { get; set; }

        /// <summary>
        /// Gets a list of place ids to skip.
        /// </summary>
        public List<int> ExcludePlaceIds { get; } = new List<int>();

        /// <inheritdoc/>
        public List<LayerType> Layers { get; } = new List<LayerType>();

        /// <inheritdoc/>
        public PolygonOutput PolygonOutput { get; set; }

        /// <inheritdoc/>
        public float PolygonThreshold { get; set; }

        /// <inheritdoc/>
        public bool? AddressDetails { get; set; }

        /// <inheritdoc/>
        public bool? ExtraInfo { get; set; }

        /// <inheritdoc/>
        public bool? NameDetails { get; set; }

        /// <inheritdoc/>
        public string AcceptLanguage { get; set; }

        /// <inheritdoc/>
        public string Email { get; set; }

        /// <inheritdoc/>
        public IDictionary<string, string> AdditionalParameters { get; } = new Dictionary<string, string>();
    }
}