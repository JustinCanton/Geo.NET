// <copyright file="QueryStringExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Extensions
{
    using Geo;

    /// <summary>
    /// Extension methods on the <see cref="QueryString"/> struct.
    /// </summary>
    public static class QueryStringExtensions
    {
        /// <summary>
        /// Adds any additional parameters from an <see cref="IAdditionalParameters"/> instance to the <see cref="QueryString"/>.
        /// Entries with a null value are skipped.
        /// </summary>
        /// <param name="query">The <see cref="QueryString"/> to add to.</param>
        /// <param name="parameters">The <see cref="IAdditionalParameters"/> to read additional key/value pairs from.</param>
        /// <returns>The <see cref="QueryString"/> with the additional parameters appended.</returns>
        public static QueryString AddAdditionalParameters(this QueryString query, IAdditionalParameters parameters)
        {
            if (parameters?.AdditionalParameters == null)
            {
                return query;
            }

            foreach (var kvp in parameters.AdditionalParameters)
            {
                if (kvp.Value == null)
                {
                    continue;
                }

                query = query.Add(kvp.Key, kvp.Value);
            }

            return query;
        }
    }
}
