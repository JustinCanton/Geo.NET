// <copyright file="IArcGISTokenRetrevial.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.ArcGIS.Models;

    /// <summary>
    /// A class for retreiving the ArcGIS API token.
    /// </summary>
    public interface IArcGISTokenRetrevial
    {
        /// <summary>
        /// Gets the current ArcGIS API token.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Token"/> continaing the token information.</returns>
        Task<Token> GetTokenAsync(CancellationToken cancellationToken);
    }
}
