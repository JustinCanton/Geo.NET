// <copyright file="AutocompleteParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a Geoapify address autocomplete request (<c>/v1/geocode/autocomplete</c>).
    /// </summary>
    public class AutocompleteParameters : BaseSearchParameters
    {
        /// <summary>
        /// Gets or sets the address, or the part of the address, to search for.
        /// <para>Required.</para>
        /// </summary>
        public string Text { get; set; }
    }
}
