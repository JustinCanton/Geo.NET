// <copyright file="FeatureConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Geo.MapBox.Models.Responses;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A converter for a <see cref="JObject"/> to a <see cref="Feature"/>.
    /// </summary>
    public class FeatureConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanConvert(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type == typeof(List<Context>);
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

            var token = JObject.Load(reader);
            var feature = new Feature();
            serializer.Populate(token.CreateReader(), feature);

            var featureInformation = token.ToObject<Dictionary<string, object>>();
            var languages = featureInformation.Where(x => x.Key.Contains(FeatureFields.Language, StringComparison.OrdinalIgnoreCase));
            if (!languages.Any())
            {
                // There are no extra languages, only the default.
                // Add in the default language item.
                languages = languages.Append(new KeyValuePair<string, object>(FeatureFields.Language, new { }));
            }

            // There will be {languages.Count()} items in this place information list.
            foreach (var language in languages)
            {
                var placeInformation = new PlaceInformation();

                var languageEnding = language.Key.Substring(8);
                featureInformation.TryGetValue(string.Concat(FeatureFields.Language, languageEnding), out var lang);
                placeInformation.Language = lang?.ToString();
                featureInformation.TryGetValue(string.Concat(FeatureFields.Text, languageEnding), out var text);
                placeInformation.Text = text?.ToString();
                featureInformation.TryGetValue(string.Concat(FeatureFields.PlaceName, languageEnding), out var placeName);
                placeInformation.PlaceName = placeName?.ToString();

                feature.PlaceInformation.Add(placeInformation);
            }

            return feature;
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

        /// <summary>
        /// A class containing the names of the fields in the feature response JSON.
        /// </summary>
        private static class FeatureFields
        {
            /// <summary>
            /// Gets the language field name.
            /// </summary>
            public static string Language => "language";

            /// <summary>
            /// Gets the text field name.
            /// </summary>
            public static string Text => "text";

            /// <summary>
            /// Gets the place name field name.
            /// </summary>
            public static string PlaceName => "place_name";
        }
    }
}