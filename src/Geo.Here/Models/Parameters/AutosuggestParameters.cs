// <copyright file="AutosuggestParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The parameters possible to use during a autosuggest request.
    /// </summary>
    public class AutosuggestParameters : AreaParameters
    {
        /// <summary>
        /// Gets or sets a free-text query.
        /// Examples:
        /// 125, Berliner, berlin
        /// Beacon, Boston, Hospital
        /// Schnurrbart German Pub and Restaurant, Hong Kong
        /// Note: Either q or qq-parameter is required on this endpoint. Both parameters can be provided in the same request.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of Query Terms Suggestions to be returned.
        /// </summary>
        [Range(0, 10)]
        public int TermsLimit { get; set; }
    }
}
