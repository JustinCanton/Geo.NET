// <copyright file="DefaultStringEnumConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Converters
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// A default enum converter. This will convert a string using the JSON.Net StringEnumConverter.
    /// If the enum fails to be converted, a default of type T will be returned instead.
    /// </summary>
    /// <typeparam name="T">A <see cref="Enum"/> type.</typeparam>
    public class DefaultStringEnumConverter<T> : StringEnumConverter
        where T : Enum
    {
        /// <inheritdoc />
        public override bool CanConvert(Type type)
        {
            return base.CanConvert(typeof(T));
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                return default(T);
            }
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            base.WriteJson(writer, value, serializer);
        }
    }
}