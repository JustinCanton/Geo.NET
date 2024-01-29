// <copyright file="IKeyParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo
{
    /// <summary>
    /// The parameter needed when making a API key request.
    /// </summary>
    public interface IKeyParameters
    {
        /// <summary>
        /// Gets or sets the key used for an API key request.
        /// </summary>
        string Key { get; set; }
    }
}
