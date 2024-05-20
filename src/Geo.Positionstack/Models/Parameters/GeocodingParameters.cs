// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Positionstack.Models;

    /// <summary>
    /// The parameters possible to use during a geocoding request.
    /// </summary>
    public class GeocodingParameters : ILocationGeocodeParameters, IFilterGeocodeParameters, IKeyParameters
    {
        /// <summary>
        /// Gets or sets the address to geocode.
        /// </summary>
        public string Query { get; set; }

        /// <inheritdoc/>
        public IList<string> Countries { get; } = new List<string>();

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
