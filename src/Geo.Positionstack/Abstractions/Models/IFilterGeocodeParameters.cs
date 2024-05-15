// <copyright file="IFilterGeocodeParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The base filter parameters used for geocoding or reverse geocoding.
    /// </summary>
    public interface IFilterGeocodeParameters
    {
        /// <summary>
        /// Gets or sets the 2-letter(e.g.en) or the 3-letter code(e.g.eng) of your preferred language to translate specific API response objects.
        /// <para>Optional.</para>
        /// <para>Default: English.</para>
        /// </summary>
        string Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the country module should be enabled to include more extensive country data in your API response.
        /// <para>Optional.</para>
        /// <para>Default: false.</para>
        /// </summary>
        bool CountryModule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sun module should be enabled to include astrology data in your API response.
        /// <para>Optional.</para>
        /// <para>Default: false.</para>
        /// </summary>
        bool SunModule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the timezone module should be enabled to include timezone data in your API response.
        /// <para>Optional.</para>
        /// <para>Default: false.</para>
        /// </summary>
        bool TimezoneModule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bounding box module should be enabled to include boundary coordinates in your API response.
        /// <para>Optional.</para>
        /// <para>Default: false.</para>
        /// </summary>
        bool BoundingBoxModule { get; set; }

        /// <summary>
        /// Gets or sets a limit between 1 and 80 to limit the number of results returned per geocoding query.
        /// <para>Optional.</para>
        /// <para>Default: 10.</para>
        /// </summary>
        uint Limit { get; set; }

        /// <summary>
        /// Gets a list of one or more response fields to decrease API response size.
        /// <para>Optional.</para>
        /// </summary>
        IList<string> Fields { get; }
    }
}
