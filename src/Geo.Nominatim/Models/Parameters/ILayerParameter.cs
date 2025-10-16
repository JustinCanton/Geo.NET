// <copyright file="ILayerParameter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Nominatim.Enums;

    /// <summary>
    /// Interface for specifying the layers to use for reverse geocoding in Nominatim.
    /// </summary>
    public interface ILayerParameter
    {
        /// <summary>
        /// Gets the layers to use for reverse geocoding.
        /// <para>
        /// Specifies which types of features to consider when searching for a result.
        /// </para>
        /// <para>
        /// Possible values are:
        /// <list type="bullet">
        /// <item><term>Address</term><description>All places that make up an address: address points with house numbers, streets, inhabited places (suburbs, villages, cities, states etc.) and administrative boundaries.</description></item>
        /// <item><term>POI</term><description>All points of interest, including restaurants, shops, hotels, and other features like recycling bins, guideposts, or benches.</description></item>
        /// <item><term>Railway</term><description>Railway infrastructure such as tracks. Note: Only a limited set of railway features may be available.</description></item>
        /// <item><term>Natural</term><description>Natural features such as rivers, lakes, and mountains.</description></item>
        /// <item><term>ManMade</term><description>Man-made features not covered by other layers.</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Optional. If not set, all layers are considered.
        /// </para>
        /// </summary>
        List<LayerType> Layers { get; }
    }
}
