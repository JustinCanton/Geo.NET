// <copyright file="ContextConverter.cs" company="Geo.NET">
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
    /// A converter for a <see cref="Dictionary{string, string}"/> to a <see cref="Context"/>.
    /// </summary>
    public class ContextConverter : JsonConverter
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
            var contextInfo = token.ToObject<Dictionary<string, string>>();

            if (contextInfo is null)
            {
                return null;
            }

            var languages = contextInfo.Where(x => x.Key.Contains(ContextFields.Language, StringComparison.OrdinalIgnoreCase));
            if (!languages.Any())
            {
                // There are no extra languages, only the default.
                // Add in the default language item.
                languages = languages.Append(new KeyValuePair<string, string>(ContextFields.Language, string.Empty));
            }

            var context = new Context();
            contextInfo.TryGetValue(ContextFields.Id, out var id);
            context.Id = id;
            contextInfo.TryGetValue(ContextFields.Wikidata, out var wikidata);
            context.Wikidata = wikidata;
            contextInfo.TryGetValue(ContextFields.ShortCode, out var shortCode);
            context.ShortCode = shortCode;
            context.ContextText = new List<ContextText>();

            // There will be {languages.Count()} items in this context group.
            foreach (var language in languages)
            {
                var contextText = new ContextText();

                var languageEnding = language.Key.Substring(8);
                contextInfo.TryGetValue(string.Concat(ContextFields.Language, languageEnding), out var lang);
                contextText.Language = lang;
                contextInfo.TryGetValue(string.Concat(ContextFields.Text, languageEnding), out var text);
                contextText.Text = text;
                contextText.Default = string.IsNullOrWhiteSpace(languageEnding);

                context.ContextText.Add(contextText);
            }

            return context;
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

        private class ContextFields
        {
            public static string Id = "id";
            public static string Wikidata = "wikidata";
            public static string ShortCode = "short_code";
            public static string Language = "language";
            public static string Text = "text";
        }
    }
}