// <copyright file="AttributeConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Converter
{
    using System;
    using Geo.ArcGIS.Models.Responses;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A converter class for the Attribute class.
    /// </summary>
    public class AttributeConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return typeof(Models.Responses.Attribute).IsAssignableFrom(type);
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

            JObject obj = JObject.Load(reader);

            if (obj is null)
            {
                return null;
            }

            Models.Responses.Attribute attr;
            if (!string.IsNullOrWhiteSpace((string)obj["LongLabel"]))
            {
                attr = new LocationAttribute();
            }
            else if (!string.IsNullOrWhiteSpace((string)obj["Match_addr"]))
            {
                attr = new AddressAttribute();
            }
            else
            {
                attr = new PlaceAttribute();
            }

            serializer.Populate(obj.CreateReader(), attr);

            return attr;
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
