// <copyright file="IArcGISCredentialsContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Abstractions
{
    using System;

    /// <summary>
    /// A container class for keeping the ArcGIS API credentials.
    /// </summary>
    public interface IArcGISCredentialsContainer
    {
        /// <summary>
        /// Gets the current ArcGIS API credentials.
        /// </summary>
        /// <returns>A named <see cref="Tuple{T1, T1}"/> with the ArcGIS ClientId and ClientSecret.</returns>
        (string ClientId, string ClientSecret) GetCredentials();
    }
}
