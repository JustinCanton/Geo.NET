// <copyright file="UriBuilderExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Extensions
{
    using System;

    /// <summary>
    /// Extension methods on the <see cref="UriBuilder"/> class.
    /// </summary>
    public static class UriBuilderExtensions
    {
        /// <summary>
        /// Adds a <see cref="QueryString"/> to a <see cref="UriBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="UriBuilder"/> to add the query to.</param>
        /// <param name="query">The <see cref="QueryString"/> to add.</param>
        /// <returns>The <see cref="UriBuilder"/> with the added query.</returns>
        public static UriBuilder AddQuery(this UriBuilder builder, QueryString query)
        {
#if NETSTANDARD2_1_OR_GREATER
            builder.Query = query.ToString();
#else
            // There is an issue in the netstandard2.0 UriBuilder where it just adds the ? no matter what.
            // See https://github.com/microsoft/referencesource/blob/4.6.2/System/net/System/uribuilder.cs line 277
            builder.Query = query.ToString().Substring(1);
#endif

            return builder;
        }
    }
}
