// <copyright file="JsonStringEnumMemberConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Converters
{
    using System;
    using System.Collections.Concurrent;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Geo.Core.Extensions;

    /// <summary>
    /// A converter for a <see cref="string"/> to an <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the enum.</typeparam>
    public class JsonStringEnumMemberConverter<T> : JsonConverter<T>
        where T : Enum
    {
        private static readonly ConcurrentDictionary<T, string> EnumMemberValues = new ConcurrentDictionary<T, string>();
        private static readonly ConcurrentDictionary<string, T> ReverseEnumMemberValues = new ConcurrentDictionary<string, T>();

        /// <inheritdoc/>
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == null)
            {
                throw new ArgumentNullException(nameof(typeToConvert));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (reader.TokenType == JsonTokenType.Null)
            {
                return default;
            }

            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the enum. Expected to find a string, instead found {0}", reader.TokenType.GetName()));
            }

            if (ReverseEnumMemberValues.Count == 0)
            {
                LoadDictionary();
            }

            var value = reader.GetString();
            return ReverseEnumMemberValues[value];
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            if (EnumMemberValues.Count == 0)
            {
                LoadDictionary();
            }

            writer.WriteStringValue(EnumMemberValues[value]);
        }

        private static void LoadDictionary()
        {
            var enumType = typeof(T);
            var enumValues = enumType.GetEnumValues();

            foreach (var enumValue in enumValues)
            {
                var memberInfo = enumType.GetMember(enumValue.ToString());
                var attributes = memberInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
                EnumMemberValues.AddOrUpdate((T)enumValue, (attributes.Length > 0) ? ((EnumMemberAttribute)attributes[0]).Value : string.Empty, (intValue, stringValue) => stringValue);
                ReverseEnumMemberValues.AddOrUpdate((attributes.Length > 0) ? ((EnumMemberAttribute)attributes[0]).Value : string.Empty, (T)enumValue, (stringValue, intValue) => intValue);
            }
        }
    }
}
