// <copyright file="IArcGISKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Abstractions
{
    using System;

    /// <summary>
    /// A container class for keeping the ArcGIS API keys.
    /// </summary>
    public interface IArcGISKeyContainer
    {
        /// <summary>
        /// Gets the current ArcGIS API keys.
        /// </summary>
        /// <returns>A <see cref="Tuple{string, string}"/> with the ArcGIS clientId,clientSecret.</returns>
        Tuple<string, string> GetKeys();
    }
}
