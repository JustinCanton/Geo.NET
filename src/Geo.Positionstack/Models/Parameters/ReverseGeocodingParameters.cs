// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Positionstack.Models;

    /// <summary>
    /// The parameters possible to use during a reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingParameters : ILocationGeocodeParameters, IFilterGeocodeParameters, IKeyParameters
    {
        /// <summary>
        /// Gets or sets the coordinates to reverse geocode.
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <inheritdoc/>
        public IList<string> Country { get; } = new List<string>();

        /// <inheritdoc/>
        public string Region { get; set; }

        /// <inheritdoc/>
        public string Language { get; set; }

        /// <inheritdoc/>
        public bool CountryModule { get; set; }

        /// <inheritdoc/>
        public bool SunModule { get; set; }

        /// <inheritdoc/>
        public bool TimezoneModule { get; set; }

        /// <inheritdoc/>
        public bool BoundingBoxModule { get; set; }

        /// <inheritdoc/>
        public uint Limit { get; set; } = 10;

        /// <inheritdoc/>
        public IList<string> Fields { get; } = new List<string>();

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
