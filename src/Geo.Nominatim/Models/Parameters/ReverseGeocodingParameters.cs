// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Nominatim.Enums;

    /// <summary>
    /// The parameters for a Nominatim reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingParameters : IBaseParameters, IPolygonParameters, ILayerParameter
    {
        /// <summary>
        /// Gets or sets the latitude of the location to generate an address for.
        /// Required.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the location to generate an address for.
        /// Required.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the preferred zoom level for the result.
        /// This is a number that corresponds roughly to the zoom level used in XYZ tile sources.
        /// The following values are supported:
        /// <list type="table">
        /// <item><term>3</term><description>country</description></item>
        /// <item><term>5</term><description>state</description></item>
        /// <item><term>8</term><description>county</description></item>
        /// <item><term>10</term><description>city</description></item>
        /// <item><term>12</term><description>town/borough</description></item>
        /// <item><term>13</term><description>village/suburb</description></item>
        /// <item><term>14</term><description>neighbourhood</description></item>
        /// <item><term>15</term><description>any settlement</description></item>
        /// <item><term>16</term><description>major streets</description></item>
        /// <item><term>17</term><description>major and minor streets</description></item>
        /// <item><term>18</term><description>building</description></item>
        /// </list>
        /// Optional.
        /// </summary>
        /// <remarks>The default is 18 (building).</remarks>
        public int? Zoom { get; set; } = 18;

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