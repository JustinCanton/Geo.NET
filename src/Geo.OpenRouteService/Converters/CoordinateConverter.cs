// <copyright file="CoordinateConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Geo.Core.Extensions;
    using Geo.OpenRouteService.Models;

    /// <summary>
    /// Converts a GeoJSON <c>[longitude, latitude]</c> coordinate array to and from a <see cref="Coordinate"/>.
    /// </summary>
    public class CoordinateConverter : JsonConverter<Coordinate>
    {
        /// <inheritdoc/>
        public override Coordinate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the coordinate. Expected to find an array, instead found {0}", reader.TokenType.GetName()));
            }

            var values = new List<double>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }
                else if (reader.TokenType == JsonTokenType.Number)
                {
                    values.Add(reader.GetDouble());
                }
                else
                {
                    throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the coordinate. Expected to find a double, instead found '{0}'", reader.GetString()));
                }
            }

            if (values.Count != 2)
            {
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected end of array while parsing the coordinate. Expected to find 2 doubles, instead found {0}", values.Count));
            }

            return new Coordinate()
            {
                Longitude = values[0],
                Latitude = values[1],
            };
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Coordinate value, JsonSerializerOptions options)
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
            writer.WriteNumberValue(value.Longitude);
            writer.WriteNumberValue(value.Latitude);
            writer.WriteEndArray();
        }
    }
}
