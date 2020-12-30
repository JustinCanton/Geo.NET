// <copyright file="BoundingBoxConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Converters
{
    using System;
    using System.Linq;
    using Geo.Bing.Models.Responses;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A converter for a <see cref="double"/>[] to a <see cref="BoundingBox"/>.
    /// </summary>
    public class BoundingBoxConverter : JsonConverter
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

            if (items.Length != 4)
            {
                return null;
            }

            return new BoundingBox()
            {
                SouthLatitude = items[0],
                WestLongitude = items[1],
                NorthLatitude = items[2],
                EastLongitude = items[3],
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
