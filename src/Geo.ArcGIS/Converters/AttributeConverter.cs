// <copyright file="AttributeConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Converters
{
    using System;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Geo.ArcGIS.Models.Responses;
    using Geo.Core.Extensions;

    /// <summary>
    /// A converter class for the Attribute class.
    /// </summary>
    public class AttributeConverter : JsonConverter<Models.Responses.Attribute>
    {
        private const string LocationAttribute = "LongLabel";
        private const string AddressAttribute = "Match_addr";
        private static readonly Type Location = typeof(LocationAttribute);
        private static readonly Type Address = typeof(AddressAttribute);
        private static readonly Type Place = typeof(PlaceAttribute);

        /// <inheritdoc/>
        public override Models.Responses.Attribute Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the attribute. Expected to find an object, instead found {0}", reader.TokenType.GetName()));
            }

            // 0 = Location
            // 1 = Address
            // 2 = Place
            var type = 2;

            while (typeReader.Read())
            {
                if (typeReader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (typeReader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the attribute. Expected to find a property name, instead found {0}", reader.TokenType.GetName()));
                }

                if (typeReader.GetString() == LocationAttribute)
                {
                    type = 0;
                    break;
                }

                if (typeReader.GetString() == AddressAttribute)
                {
                    type = 1;
                    break;
                }

                typeReader.Read();
            }

            if (type == 0)
            {
                return JsonSerializer.Deserialize(ref reader, typeof(LocationAttribute)) as LocationAttribute;
            }
            else if (type == 1)
            {
                return JsonSerializer.Deserialize(ref reader, typeof(AddressAttribute)) as AddressAttribute;
            }
            else
            {
                return JsonSerializer.Deserialize(ref reader, typeof(PlaceAttribute)) as PlaceAttribute;
            }
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Models.Responses.Attribute value, JsonSerializerOptions options)
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

            if (value.GetType() == Location)
            {
                JsonSerializer.Serialize(writer, value, Location);
            }
            else if (value.GetType() == Address)
            {
                JsonSerializer.Serialize(writer, value, Address);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, Place);
            }
        }
    }
}
