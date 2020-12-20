// <copyright file="BusinessStatus.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Enums
{
    /// <summary>
    /// The operational status of the place, if it is a business.
    /// </summary>
    public enum BusinessStatus
    {
        /// <summary>
        /// The business is operational.
        /// </summary>
        Operational,

        /// <summary>
        /// The business is closed temporarily.
        /// </summary>
        ClosedTemporarily,

        /// <summary>
        /// The business is closed permanently.
        /// </summary>
        ClosedPermanently,
    }
}
