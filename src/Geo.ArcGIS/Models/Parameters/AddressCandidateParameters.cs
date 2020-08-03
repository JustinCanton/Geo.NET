// <copyright file="AddressCandidateParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
