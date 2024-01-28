// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Parameters
{
    using Geo.MapQuest.Enums;

    /// <summary>
    /// The parameters possible to use during a geocoding request.
    /// </summary>
    public class GeocodingParameters : BaseParameters, IKeyParameters
    {
        /// <summary>
        /// Gets or sets the location to geocode.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the bounding box for results.
        /// When ambiguous results are returned, any results within the provided bounding box will be moved to the top of the results list.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service should fail when given a latitude/longitude pair in an address or batch geocode call,
        /// or if it should ignore that and try and geocode what it can.
        /// The default value is false.
        /// </summary>
        public bool IgnoreLatLngInput { get; set; } = false;

        /// <summary>
        /// Gets or sets the max number of locations to return from the geocode.
        /// The default is 5.
        /// </summary>
        public int MaxResults { get; set; } = 5;

        /// <summary>
        /// Gets or sets the flag which indicates how to handle a 5-box geocode for users of the International Geocoder.
        /// The default is auto.
        /// </summary>
        public InternationalMode IntlMode { get; set; } = InternationalMode.Auto;

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
