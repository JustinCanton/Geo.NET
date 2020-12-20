// <copyright file="ArcGISTokenContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.ArcGIS.Abstractions;

    /// <summary>
    /// A container class for keeping the ArcGIS API token.
    /// </summary>
    public class ArcGISTokenContainer : IArcGISTokenContainer
    {
        private readonly IArcGISTokenRetrevial _tokenRetrevial;

        private string _accessToken = string.Empty;

        private DateTime _expiry = DateTime.MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISTokenContainer"/> class.
        /// </summary>
        /// <param name="tokenRetrevial">A <see cref="IArcGISTokenRetrevial"/> used to retrieve the token if expired of none existant.</param>
        public ArcGISTokenContainer(IArcGISTokenRetrevial tokenRetrevial)
        {
            _tokenRetrevial = tokenRetrevial;
        }

        /// <inheritdoc/>
        public async Task<string> GetTokenAsync(CancellationToken cancellationToken)
        {
            // Make sure the expiry is in the future.
            // Add an extra 30 second onto the expiry time to allow for any latency.
            if (_expiry > DateTime.Now.AddSeconds(30))
            {
                return _accessToken;
            }

            var token = await _tokenRetrevial.GetTokenAsync(cancellationToken).ConfigureAwait(false);

            _accessToken = token.AccessToken;
            _expiry = DateTime.Now.AddSeconds(token.ExpiresIn);

            return _accessToken;
        }
    }
}
