﻿// <copyright file="CoordinateConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Converters
{
    using System;
    using System.Linq;
    using Geo.MapBox.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A converter for a <see cref="double[]"/> to a <see cref="Coordinate"/>.
    /// </summary>
    public class CoordinateConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type == typeof(double[]);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            var token = JArray.Load(reader);
            double[] items = token.Select(jv => (double)jv).ToArray();

            if (items is null)
            {
                return null;
            }

            if (items.Length != 2)
            {
                return null;
            }

            return new Coordinate()
            {
                Longitude = items[0],
                Latitude = items[1],
            };
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            serializer.Serialize(writer, value);
        }
    }
}