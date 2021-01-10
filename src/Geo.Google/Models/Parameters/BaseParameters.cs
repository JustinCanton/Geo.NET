// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    using System.Globalization;

    /// <summary>
    /// The base parameters shared across all requests.
    /// </summary>
    public class BaseParameters
    {
        /// <summary>
        /// Gets or sets the language in which to return results.
        /// </summary>
        public CultureInfo Language { get; set; }
    }
}
