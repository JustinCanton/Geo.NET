// <copyright file="IArcGISCredentialsContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Abstractions
{
    /// <summary>
    /// A container class for keeping the ArcGIS API credentials.
    /// </summary>
    public interface IArcGISCredentialsContainer
    {
        /// <summary>
        /// Gets the current ArcGIS API credentials.
        /// </summary>
        /// <returns>A named <see cref="Tuple{string, string}"/> with the ArcGIS ClientId and ClientSecret.</returns>
        (string ClientId, string ClientSecret) GetCredentials();
    }
}
