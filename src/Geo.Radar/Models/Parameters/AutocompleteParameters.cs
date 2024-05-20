// <copyright file="AutocompleteParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Models.Parameters
{
    using System.Collections.Generic;

    /// <summary>
    /// The parameters possible to use during an autocomplete request.
    /// </summary>
    public class AutocompleteParameters : ICountryParameter, ILayersParameter, IKeyParameters
    {
        /// <summary>
        /// Gets or sets the partial address or place name to autocomplete.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the location to prefer search results near. If not provided, the request IP geolocation will be used to anchor the search. Optional.
        /// </summary>
        public Coordinate Near { get; set; }

        /// <inheritdoc/>
        public IList<string> Countries { get; } = new List<string>();

        /// <inheritdoc/>
        public IList<Layer> Layers { get; } = new List<Layer>();

        /// <summary>
        /// Gets or sets the max number of addresses to return. A number between 1 and 100. Defaults to 10. Optional.
        /// </summary>
        public uint Limit { get; set; } = 10;

        /// <summary>
        /// Gets or sets a value indicating whether to return validated addresses. Only available for US and Canada addresses for enterprise customers. Optional.
        /// </summary>
        public bool Mailable { get; set; } = false;

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
