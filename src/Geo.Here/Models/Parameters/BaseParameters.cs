// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System.Globalization;

    /// <summary>
    /// The base parameters that are used with all HERE requests.
    /// </summary>
    public class BaseParameters
    {
        /// <summary>
        /// Gets or sets the language to be used for result rendering from a list of BCP47 compliant Language Codes.
        /// </summary>
        public CultureInfo Language { get; set; }
    }
}
