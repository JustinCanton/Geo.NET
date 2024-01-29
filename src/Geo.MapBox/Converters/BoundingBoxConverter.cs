// <copyright file="BoundingBoxConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Geo.Core.Extensions;
    using Geo.MapBox.Models;

    /// <summary>
    /// A converter for a <see cref="double"/>[] to a <see cref="BoundingBox"/>.
    /// </summary>
    public class BoundingBoxConverter : JsonConverter<BoundingBox>
    {
        /// <inheritdoc/>
        public override BoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                return null;
            }

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the bounding box. Expected to find an array, instead found {0}", reader.TokenType.GetName()));
            }

            var box = new List<double>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }
                else if (reader.TokenType == JsonTokenType.Number)
                {
                    box.Add(reader.GetDouble());
                }
                else
                {
                    throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the bounding box. Expected to find a double, instead found '{0}'", reader.GetString()));
                }
            }

            if (box.Count != 4)
            {
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected end of array while parsing the bounding box. Expected to find a 4 doubles, instead found {0}", box.Count));
            }

            return new BoundingBox()
            {
                West = box[0],
                South = box[1],
                East = box[2],
                North = box[3],
            };
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
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

            writer.WriteStartArray();
            writer.WriteNumberValue(value.South);
            writer.WriteNumberValue(value.West);
            writer.WriteNumberValue(value.East);
            writer.WriteNumberValue(value.North);
            writer.WriteEndArray();
        }
    }
}
