// <copyright file="AddressCandidateParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    /// <summary>
    /// A parameters object for the address candidates ArcGIS request.
    /// </summary>
    public class AddressCandidateParameters : StorageParameters, IClientCredentialsParameters
    {
        /// <summary>
        /// Gets or sets the address you want to geocode as a single line of text.
        /// </summary>
        public string SingleLineAddress { get; set; }

        /// <summary>
        /// Gets or sets an ID attribute value that, along with the text attribute, links a suggestion to an address or place.
        /// </summary>
        public string MagicKey { get; set; }

        /// <summary>
        /// Comma-separated list of attribute fields to include in the response. To return all fields, specify the wildcard '*' as the value of this parameter.
        /// </summary>
        public string OutFields { get; set; } = "Match_addr,Addr_type";

        /// <inheritdoc/>
        public string ClientId { get; set; }

        /// <inheritdoc/>
        public string ClientSecret { get; set; }
    }
}
