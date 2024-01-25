// <copyright file="IArcGISTokenProvider.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A class for providing an ArcGIS API token.
    /// </summary>
    public interface IArcGISTokenProvider
    {
        /// <summary>
        /// Gets a token for the ArcGIS API using the provided client id and client secret.
        /// </summary>
        /// <param name="clientId">The client id to use to fetch the token.</param>
        /// <param name="clientSecret">The client secret to use to fetch the token.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="string"/> with the token for accessing the ArcGIS API, or null if a token could not be retrieved.</returns>
        Task<string> GetTokenAsync(string clientId, string clientSecret, CancellationToken cancellationToken);
    }
}
