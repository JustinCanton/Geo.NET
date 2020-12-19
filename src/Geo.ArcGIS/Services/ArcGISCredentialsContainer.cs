// <copyright file="ArcGISCredentialsContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Services
{
    using Geo.ArcGIS.Abstractions;

    /// <summary>
    /// A container class for keeping the ArcGIS API credentials.
    /// </summary>
    public class ArcGISCredentialsContainer : IArcGISCredentialsContainer
    {
        private readonly string _clientId = string.Empty;

        private readonly string _clientSecret = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISCredentialsContainer"/> class.
        /// </summary>
        /// <param name="clientId">A string containing the client id for ArcGIS.</param>
        /// <param name="clientSecret">A string containing the client secret for ArcGIS.</param>
        public ArcGISCredentialsContainer(
            string clientId,
            string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        /// <inheritdoc/>
        public (string ClientId, string ClientSecret) GetCredentials()
        {
            return (_clientId, _clientSecret);
        }
    }
}
