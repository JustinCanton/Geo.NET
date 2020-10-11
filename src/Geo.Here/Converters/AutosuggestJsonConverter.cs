﻿// <copyright file="AutosuggestJsonConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Converters
{
    using System;
    using Geo.Here.Models.Responses;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A json converter for the autosuggest json response.
    /// </summary>
    public class AutosuggestJsonConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return typeof(BaseLocation).IsAssignableFrom(type);
        }

        /// <inheritdoc/>
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

            var jobj = JObject.Load(reader);
            if (jobj.TryGetValue(nameof(AutosuggestEntityLocation.Address), StringComparison.OrdinalIgnoreCase, out _))
            {
                var obj = new AutosuggestEntityLocation();
                serializer.Populate(jobj.CreateReader(), obj);
                return obj;
            }
            else
            {
                var obj = new AutosuggestQueryLocation();
                serializer.Populate(jobj.CreateReader(), obj);
                return obj;
            }
        }

        /// <inheritdoc/>
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