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
        private static readonly Type Entity = typeof(AutosuggestEntityLocation);
        private static readonly Type Query = typeof(AutosuggestQueryLocation);

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
            var addressAttributeName = Entity.GetAttribute<JsonPropertyNameAttribute>(nameof(AutosuggestEntityLocation.Address))?.Name;
            var hrefAttributeName = Query.GetAttribute<JsonPropertyNameAttribute>(nameof(AutosuggestQueryLocation.Href))?.Name;

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
                return JsonSerializer.Deserialize(ref reader, typeof(AutosuggestEntityLocation)) as AutosuggestEntityLocation;
            }
            else
            {
                return JsonSerializer.Deserialize(ref reader, typeof(AutosuggestQueryLocation)) as AutosuggestQueryLocation;
            }

            // Switch to the reader instead of the typeReader
            // Read the StartObject
            /*reader.Read();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return location;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case BaseType.GetAttribute<JsonPropertyNameAttribute>(nameof(BaseLocation.Title)):
                            location.Title = reader.GetString();
                            break;
                    }
                }
            }*/
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

            if (value.GetType() == Entity)
            {
                JsonSerializer.Serialize(writer, value, Entity);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, Query);
            }
        }
    }
}
