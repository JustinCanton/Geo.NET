// <copyright file="ArcGISOptionsBuilder.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models
{
    using System;

    /// <summary>
    /// Options for the ArcGIS API dependency injection.
    /// </summary>
    public class ArcGISOptionsBuilder
    {
        /// <summary>
        /// Gets the client id associated with the ArcGIS API calls.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// Gets the client secret associated with the ArcGIS API calls.
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// Sets the ArcGIS API credentials to be used during calls.
        /// </summary>
        /// <param name="clientId">The ArcGIS API client id to use.</param>
        /// <param name="clientSecret">The ArcGIS API client secret to use.</param>
        /// <returns>A <see cref="ArcGISOptionsBuilder"/> configured with the credentials.</returns>
        /// <exception cref="ArgumentException">If the client id/secret passed in are null or empty.</exception>
        public ArcGISOptionsBuilder UseClientCredentials(
            string clientId,
            string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException("The ArcGIS API client id can not be null or empty");
            }

            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new ArgumentException("The ArcGIS API client secret can not be null or empty");
            }

            ClientId = clientId;
            ClientSecret = clientSecret;

            return this;
        }
    }
}
