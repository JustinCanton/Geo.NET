// <copyright file="Layer.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar
{
    /// <summary>
    /// The layers to return.
    /// </summary>
    public enum Layer
    {
        /// <summary>
        /// Return the place information.
        /// </summary>
        Place,

        /// <summary>
        /// Return the address information.
        /// </summary>
        Address,

        /// <summary>
        /// Return the postal code information.
        /// </summary>
        PostalCode,

        /// <summary>
        /// Return the locality information.
        /// </summary>
        Locality,

        /// <summary>
        /// Return the county information.
        /// </summary>
        County,

        /// <summary>
        /// Return the state information.
        /// </summary>
        State,

        /// <summary>
        /// Return the country information.
        /// </summary>
        Country,

        /// <summary>
        /// Coarse includes all of postalCode, locality, county, state, and country.
        /// </summary>
        Coarse,

        /// <summary>
        /// Fine includes address and place
        /// </summary>
        Fine,
    }
}
