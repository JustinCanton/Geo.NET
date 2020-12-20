// <copyright file="ResultParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Parameters
{
    /// <summary>
    /// Parameters for Bing APIs including result limiting.
    /// </summary>
    public class ResultParameters : BaseParameters
    {
        /// <summary>
        /// Gets or sets the maximum number of results allowable to be returned.
        /// Allowable values are between 1 and 20. The default value is 5.
        /// If the value is 0, the default value will be used.
        /// </summary>
        public int MaximumResults { get; set; } = 5;
    }
}
