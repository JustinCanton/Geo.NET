// <copyright file="NominatimOptions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Settings
{
    /// <summary>
    /// The options used to configure the Nominatim geocoding.
    /// </summary>
    public class NominatimOptions
    {
        /// <summary>
        /// The base url of the public Nominatim instance operated by the OpenStreetMap Foundation.
        /// This is the instance used when no other server is configured.
        /// </summary>
        public const string DefaultServer = "https://nominatim.openstreetmap.org";

        /// <summary>
        /// Gets or sets the base url of the Nominatim instance to send requests to.
        /// <para>
        /// Nominatim can be used through the public instance, through another publicly hosted instance,
        /// or through a self hosted deployment. Set this to the root of the instance, for example
        /// <c>https://nominatim.openstreetmap.org</c> or <c>https://my-server.example.com/nominatim</c>.
        /// </para>
        /// <para>Defaults to <see cref="DefaultServer"/>.</para>
        /// </summary>
        public string Server { get; set; } = DefaultServer;

        /// <summary>
        /// Gets or sets the email address to be sent with Nominatim API requests.
        /// This is recommended by Nominatim for identification purposes.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the value of the User-Agent header sent with Nominatim API requests.
        /// <para>
        /// The usage policy of the public Nominatim instance requires requests to identify the calling
        /// application, and rejects the stock user agents set by http libraries. This is not needed when
        /// calling a self hosted instance, or when the user agent is already set on the <see cref="System.Net.Http.HttpClient"/>.
        /// </para>
        /// </summary>
        public string UserAgent { get; set; }
    }
}
