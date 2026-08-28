// <copyright file="CountryCodeFilter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters.Filters
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// A filter restricting results to one or more countries.
    /// </summary>
    public class CountryCodeFilter : Filter
    {
        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country codes to restrict results to.
        /// The special values <c>auto</c> and <c>none</c> are also accepted by Geoapify.
        /// </summary>
        public IList<string> CountryCodes { get; } = new List<string>();

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "countrycode:{0}", string.Join(",", CountryCodes));
        }
    }
}
