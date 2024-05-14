// <copyright file="ILayersParameter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// A layers representation for a query.
    /// </summary>
    public interface ILayersParameter
    {
        /// <summary>
        /// Gets what layers to return. If not provided, results from address and coarse layers will be returned. Optional.
        /// </summary>
        IList<Layer> Layers { get; }
    }
}
