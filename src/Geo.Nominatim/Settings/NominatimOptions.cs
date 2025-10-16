// <copyright file="NominatimOptions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Settings
{
    /// <summary>
    /// A builder used to configure an instance of the Nominatim geocoding.
    /// </summary>
    public class NominatimOptions
    {
        /// <summary>
        /// Gets or sets the email address to be sent with Nominatim API requests.
        /// This is recommended by Nominatim for identification purposes.
        /// </summary>
        public string Email { get; set; }
    }
}
