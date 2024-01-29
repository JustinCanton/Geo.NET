// <copyright file="QueryString.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Encodings.Web;

    /// <summary>
    /// Provides correct handling for QueryString value when needed to reconstruct a request or redirect URI string.
    /// This was copied from the aspnetcore repo (https://github.com/dotnet/aspnetcore/blob/v6.0.15/src/Http/Http.Abstractions/src/QueryString.cs)
    /// on the suggestion of https://github.com/dotnet/aspnetcore/issues/20946 to resolve https://github.com/JustinCanton/Geo.NET/issues/60.
    /// </summary>
    public readonly struct QueryString : IEquatable<QueryString>
    {
        /// <summary>
        /// Represents the empty query string. This field is read-only.
        /// </summary>
        public static readonly QueryString Empty = new QueryString(string.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryString"/> struct.
        /// Initialize the query string with a given value.
        /// This value must be in escaped and delimited format with a leading '?' character.
        /// </summary>
        /// <param name="value">The query string to be assigned to the Value property.</param>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        public QueryString(string? value)
#else
        public QueryString(string value)
#endif
        {
            if (!string.IsNullOrEmpty(value) && value[0] != '?')
            {
                throw new ArgumentException("The leading '?' must be included for a non-empty query.", nameof(value));
            }

            Value = value;
        }

        /// <summary>
        /// Gets the escaped query string with the leading '?' character.
        /// </summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        public string? Value { get; }
#else
        public string Value { get; }
#endif

        /// <summary>
        /// Gets a value indicating whether the query string is not empty.
        /// </summary>
        public bool HasValue => !string.IsNullOrEmpty(Value);

        /// <summary>
        /// Evaluates if one query string is equal to another.
        /// </summary>
        /// <param name="left">A <see cref="QueryString"/> instance.</param>
        /// <param name="right">A <see cref="QueryString"/> instance to compare.</param>
        /// <returns><see langword="true" /> if the query strings are equal.</returns>
        public static bool operator ==(QueryString left, QueryString right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Evaluates if one query string is not equal to another.
        /// </summary>
        /// <param name="left">A <see cref="QueryString"/> instance.</param>
        /// <param name="right">A <see cref="QueryString"/> instance to compare.</param>
        /// <returns><see langword="true" /> if the query strings are not equal.</returns>
        public static bool operator !=(QueryString left, QueryString right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Concatenates <paramref name="left"/> and <paramref name="right"/> into a single query string.
        /// </summary>
        /// <param name="left">A <see cref="QueryString"/> instance.</param>
        /// <param name="right">A <see cref="QueryString"/> instance to append.</param>
        /// <returns>The concatenated <see cref="QueryString"/>.</returns>
        public static QueryString operator +(QueryString left, QueryString right)
        {
            return left.Add(right);
        }

        /// <summary>
        /// Returns an QueryString given the query as it is escaped in the URI format. The string MUST NOT contain any
        /// value that is not a query.
        /// </summary>
        /// <param name="uriComponent">The escaped query as it appears in the URI format.</param>
        /// <returns>The resulting QueryString.</returns>
        public static QueryString FromUriComponent(string uriComponent)
        {
            if (string.IsNullOrEmpty(uriComponent))
            {
                return new QueryString(string.Empty);
            }

            return new QueryString(uriComponent);
        }

        /// <summary>
        /// Returns an QueryString given the query as from a Uri object. Relative Uri objects are not supported.
        /// </summary>
        /// <param name="uri">The Uri object.</param>
        /// <returns>The resulting QueryString.</returns>
        public static QueryString FromUriComponent(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            string queryValue = uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped);
            if (!string.IsNullOrEmpty(queryValue))
            {
                queryValue = "?" + queryValue;
            }

            return new QueryString(queryValue);
        }

        /// <summary>
        /// Create a query string with a single given parameter name and value.
        /// </summary>
        /// <param name="name">The un-encoded parameter name.</param>
        /// <param name="value">The un-encoded parameter value.</param>
        /// <returns>The resulting QueryString.</returns>
        public static QueryString Create(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!string.IsNullOrEmpty(value))
            {
                value = UrlEncoder.Default.Encode(value);
            }

            return new QueryString($"?{UrlEncoder.Default.Encode(name)}={value}");
        }

        /// <summary>
        /// Creates a query string composed from the given name value pairs.
        /// </summary>
        /// <param name="parameters">An <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, TValue}"/> to append as query parameters.</param>
        /// <returns>The resulting QueryString.</returns>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        public static QueryString Create(IEnumerable<KeyValuePair<string, string?>> parameters)
#else
        public static QueryString Create(IEnumerable<KeyValuePair<string, string>> parameters)
#endif
        {
            var builder = new StringBuilder();
            var first = true;
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            foreach (var pair in parameters ?? Array.Empty<KeyValuePair<string, string?>>())
#else
            foreach (var pair in parameters ?? Array.Empty<KeyValuePair<string, string>>())
#endif
            {
                AppendKeyValuePair(builder, pair.Key, pair.Value, first);
                first = false;
            }

            return new QueryString(builder.ToString());
        }

        /// <summary>
        /// Provides the query string escaped in a way which is correct for combining into the URI representation.
        /// A leading '?' character will be included unless the Value is null or empty. Characters which are potentially
        /// dangerous are escaped.
        /// </summary>
        /// <returns>The query string value.</returns>
        public override string ToString()
        {
            return ToUriComponent();
        }

        /// <summary>
        /// Provides the query string escaped in a way which is correct for combining into the URI representation.
        /// A leading '?' character will be included unless the Value is null or empty. Characters which are potentially
        /// dangerous are escaped.
        /// </summary>
        /// <returns>The query string value.</returns>
#pragma warning disable CA1055 // URI-like return values should not be strings. The source code in aspnetcore does this. I will not change the functionality.
        public string ToUriComponent()
#pragma warning restore CA1055 // URI-like return values should not be strings
        {
            // Escape things properly so System.Uri doesn't mis-interpret the data.
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            return !string.IsNullOrEmpty(Value) ? Value!.Replace("#", "%23", StringComparison.InvariantCulture) : string.Empty;
#else
            return !string.IsNullOrEmpty(Value) ? Value.Replace("#", "%23") : string.Empty;
#endif
        }

        /// <summary>
        /// Concatenates <paramref name="other"/> to the current query string.
        /// </summary>
        /// <param name="other">The <see cref="QueryString"/> to concatenate.</param>
        /// <returns>The concatenated <see cref="QueryString"/>.</returns>
        public QueryString Add(QueryString other)
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            if (!HasValue || Value!.Equals("?", StringComparison.Ordinal))
#else
            if (!HasValue || Value.Equals("?", StringComparison.Ordinal))
#endif
            {
                return other;
            }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            if (!other.HasValue || other.Value!.Equals("?", StringComparison.Ordinal))
#else
            if (!other.HasValue || other.Value.Equals("?", StringComparison.Ordinal))
#endif
            {
                return this;
            }

            // ?name1=value1 Add ?name2=value2 returns ?name1=value1&name2=value2
#if NET6_0_OR_GREATER
            return new QueryString(string.Concat(Value, "&", other.Value.AsSpan(1)));
#elif NETSTANDARD2_1
            return new QueryString(Value + "&" + other.Value[1..]);
#else
            return new QueryString(Value + "&" + other.Value.Substring(1));
#endif
        }

        /// <summary>
        /// Concatenates a query string with <paramref name="name"/> and <paramref name="value"/>
        /// to the current query string.
        /// </summary>
        /// <param name="name">The name of the query string to concatenate.</param>
        /// <param name="value">The value of the query string to concatenate.</param>
        /// <returns>The concatenated <see cref="QueryString"/>.</returns>
        public QueryString Add(string name, string value)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            if (!HasValue || Value!.Equals("?", StringComparison.Ordinal))
#else
            if (!HasValue || Value.Equals("?", StringComparison.Ordinal))
#endif
            {
                return Create(name, value);
            }

            var builder = new StringBuilder(Value);
            AppendKeyValuePair(builder, name, value, first: false);
            return new QueryString(builder.ToString());
        }

        /// <summary>
        /// Evalutes if the current query string is equal to <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The <see cref="QueryString"/> to compare.</param>
        /// <returns><see langword="true"/> if the query strings are equal.</returns>
        public bool Equals(QueryString other)
        {
            if (!HasValue && !other.HasValue)
            {
                return true;
            }

            return string.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Evaluates if the current query string is equal to an object <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">An object to compare.</param>
        /// <returns><see langword="true" /> if the query strings are equal.</returns>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (ReferenceEquals(null, obj))
            {
                return !HasValue;
            }

            return obj is QueryString query && Equals(query);
        }

        /// <summary>
        /// Gets a hash code for the value.
        /// </summary>
        /// <returns>The hash code as an <see cref="int"/>.</returns>
        public override int GetHashCode()
        {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
            return HasValue ? Value!.GetHashCode(StringComparison.InvariantCulture) : 0;
#else
            return HasValue ? Value.GetHashCode() : 0;
#endif
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
        private static void AppendKeyValuePair(StringBuilder builder, string key, string? value, bool first)
#else
        private static void AppendKeyValuePair(StringBuilder builder, string key, string value, bool first)
#endif
        {
            builder.Append(first ? '?' : '&');
            builder.Append(UrlEncoder.Default.Encode(key));
            builder.Append('=');
            if (!string.IsNullOrEmpty(value))
            {
                builder.Append(UrlEncoder.Default.Encode(value));
            }
        }
    }
}
