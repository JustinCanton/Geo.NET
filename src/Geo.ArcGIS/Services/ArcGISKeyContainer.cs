// <copyright file="ArcGISKeyContainer.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Services
{
    using System;
    using Geo.ArcGIS.Abstractions;

    /// <summary>
    /// A container class for keeping the ArcGIS API keys.
    /// </summary>
    public class ArcGISKeyContainer : IArcGISKeyContainer
    {
        private readonly string _clientId = string.Empty;

        private readonly string _clientSecret = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISKeyContainer"/> class.
        /// </summary>
        /// <param name="clientId">A string containing the client id for ArcGIS.</param>
        /// <param name="clientSecret">A string containing the client secret for ArcGIS.</param>
        public ArcGISKeyContainer(
            string clientId,
            string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        /// <inheritdoc/>
        public Tuple<string, string> GetKeys()
        {
            return new Tuple<string, string>(_clientId, _clientSecret);
        }
    }
}
