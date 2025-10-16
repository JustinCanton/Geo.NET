// <copyright file="IBaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Parameters
{
    /// <summary>
    /// The base parameters that are used with all Nominatim requests.
    /// </summary>
    public interface IBaseParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include a breakdown of the address into elements.
        /// </summary>
        bool? AddressDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include additional information in the result if available.
        /// This includes alternative names, Wikipedia links, opening hours, etc.
        /// </summary>
        bool? ExtraInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include the full list of alternative names in the details.
        /// These may include language variants, references, operator and brand.
        /// </summary>
        bool? NameDetails { get; set; }

        /// <summary>
        /// Gets or sets the preferred language order for showing search results.
        /// Either use a standard RFC2616 accept-language string or a simple comma-separated list of language codes.
        /// </summary>
        /// <example>de,en.</example>
        string AcceptLanguage { get; set; }

        /// <summary>
        /// Gets or sets the email address to identify your requests.
        /// Strongly recommended for any serious use of the Nominatim API.
        /// </summary>
        string Email { get; set; }
    }
}
