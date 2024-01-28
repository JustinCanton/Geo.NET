// <copyright file="IClientCredentialsParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo
{
    /// <summary>
    /// The parameters needed when making a client credentials request.
    /// </summary>
    public interface IClientCredentialsParameters
    {
        /// <summary>
        /// Gets or sets the client id used for the client credentials request.
        /// </summary>
        string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret used for the client credentials request.
        /// </summary>
        string ClientSecret { get; set; }
    }
}
