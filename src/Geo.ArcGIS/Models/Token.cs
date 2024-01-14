// <copyright file="Token.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A token information class for the ArcGIS API.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Gets or sets the access token for the ArcGIS API.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the expiry time for the ArcGIS API token.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
