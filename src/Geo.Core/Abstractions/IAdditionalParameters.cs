// <copyright file="IAdditionalParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo
{
    using System.Collections.Generic;

    /// <summary>
    /// Parameters that allow additional key/value pairs to be appended to the request query string.
    /// </summary>
    public interface IAdditionalParameters
    {
        /// <summary>
        /// Gets the additional key/value pairs to be appended to the request query string.
        /// </summary>
        IDictionary<string, string> AdditionalParameters { get; }
    }
}
