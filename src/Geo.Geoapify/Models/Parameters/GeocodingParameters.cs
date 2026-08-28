// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a Geoapify forward geocoding request (<c>/v1/geocode/search</c>).
    /// <para>
    /// Either the free form <see cref="Text"/> or at least one of the structured address components must be provided.
    /// </para>
    /// </summary>
    public class GeocodingParameters : BaseSearchParameters
    {
        /// <summary>
        /// Gets or sets the free form address to geocode.
        /// <para>Required, unless a structured address component is provided.</para>
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the name of a place or amenity to geocode.
        /// <para>Optional. Part of a structured address.</para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the house number to geocode.
        /// <para>Optional. Part of a structured address.</para>
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the street to geocode.
        /// <para>Optional. Part of a structured address.</para>
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the postal code to geocode.
        /// <para>Optional. Part of a structured address.</para>
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// Gets or sets the city to geocode.
        /// <para>Optional. Part of a structured address.</para>
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state to geocode.
        /// <para>Optional. Part of a structured address.</para>
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country to geocode.
        /// <para>Optional. Part of a structured address.</para>
        /// </summary>
        public string Country { get; set; }
    }
}
