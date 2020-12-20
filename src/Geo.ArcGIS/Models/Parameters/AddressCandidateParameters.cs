// <copyright file="AddressCandidateParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    /// <summary>
    /// A parameters object for the address candidates ArcGIS request.
    /// </summary>
    public class AddressCandidateParameters : StorageParameters
    {
        /// <summary>
        /// Gets or sets the address you want to geocode as a single line of text.
        /// </summary>
        public string SingleLineAddress { get; set; }
    }
}
