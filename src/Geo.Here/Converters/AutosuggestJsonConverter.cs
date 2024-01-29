// <copyright file="AutosuggestJsonConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Converters
{
    using System;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Here.Models.Responses;

    /// <summary>
    /// A json converter for the autosuggest json response.
    /// </summary>
    public class AutosuggestJsonConverter : JsonConverter<BaseLocation>
    {
        private static readonly Type EntityType = typeof(AutosuggestEntityLocation);
        private static readonly Type QueryType = typeof(AutosuggestQueryLocation);

        /// <inheritdoc/>
        public override BaseLocation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == null)
            {
                throw new ArgumentNullException(nameof(typeToConvert));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var typeReader = reader;

            if (typeReader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (typeReader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the autosuggest. Expected to find an object, instead found {0}", reader.TokenType.GetName()));
            }

            var isEntity = false;
            var addressAttributeName = EntityType.GetAttribute<JsonPropertyNameAttribute>(nameof(AutosuggestEntityLocation.Address))?.Name;
            var hrefAttributeName = QueryType.GetAttribute<JsonPropertyNameAttribute>(nameof(AutosuggestQueryLocation.Href))?.Name;

            while (typeReader.Read())
            {
                if (typeReader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (typeReader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the autosuggest. Expected to find a property name, instead found {0}", reader.TokenType.GetName()));
                }

                if (typeReader.GetString() == addressAttributeName)
                {
                    isEntity = true;
                    break;
                }

                if (typeReader.GetString() == hrefAttributeName)
                {
                    isEntity = false;
                    break;
                }

                typeReader.Read();
            }

            if (isEntity)
            {
                return JsonSerializer.Deserialize<AutosuggestEntityLocation>(ref reader, options);
            }
            else
            {
                return JsonSerializer.Deserialize<AutosuggestQueryLocation>(ref reader, options);
            }
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, BaseLocation value, JsonSerializerOptions options)
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

            if (value.GetType() == EntityType)
            {
                JsonSerializer.Serialize(writer, value, EntityType, options);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, QueryType, options);
            }
        }
    }
}
