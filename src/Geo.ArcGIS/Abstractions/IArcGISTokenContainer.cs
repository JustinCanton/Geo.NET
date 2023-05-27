// <copyright file="IArcGISTokenContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A container class for keeping the ArcGIS API token.
    /// </summary>
    public interface IArcGISTokenContainer
    {
        /// <summary>
        /// Gets the current ArcGIS API token.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="string"/> with the ArcGIS API token.</returns>
        Task<string> GetTokenAsync(CancellationToken cancellationToken);
    }
}
