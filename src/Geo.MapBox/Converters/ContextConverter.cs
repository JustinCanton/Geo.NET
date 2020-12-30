// <copyright file="ContextConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
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
                // There are 2 cases here:
                // Either there are no extra languages, only the default,
                // or there is a context group without the language tags.
                var textItems = contextInfo.Keys.Where(x => x.Contains("text", StringComparison.OrdinalIgnoreCase));
                if (textItems.Count() > 1)
                {
                    // This context group does not contain any language keys for an unknown reason.
                    // Try to use the text items language names.
                    foreach (var item in textItems)
                    {
                        var language = item.Substring(4);
                        languages = languages.Append(new KeyValuePair<string, string>(string.Concat(ContextFields.Language, language), language.Length > 0 ? language.Substring(1) : string.Empty));
                    }
                }
                else
                {
                    // Add in the default language item.
                    languages = languages.Append(new KeyValuePair<string, string>(ContextFields.Language, string.Empty));
                }
            }

            var context = new Context();
            contextInfo.TryGetValue(ContextFields.Id, out var id);
            context.Id = id;
            contextInfo.TryGetValue(ContextFields.Wikidata, out var wikidata);
            context.Wikidata = wikidata;
            contextInfo.TryGetValue(ContextFields.ShortCode, out var shortCode);
            context.ShortCode = shortCode;

            // There will be {languages.Count()} items in this context group.
            foreach (var language in languages)
            {
                var contextText = new ContextText();

                var languageEnding = language.Key.Substring(8);
                contextInfo.TryGetValue(string.Concat(ContextFields.Language, languageEnding), out var lang);
                if (lang == null)
                {
                    lang = language.Value;
                }

                contextText.Language = lang;
                contextInfo.TryGetValue(string.Concat(ContextFields.Text, languageEnding), out var text);
                contextText.Text = text;
                contextText.IsDefault = string.IsNullOrWhiteSpace(languageEnding);

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

        /// <summary>
        /// A class containing the names of the fields in the context response JSON.
        /// </summary>
        private static class ContextFields
        {
            /// <summary>
            /// Gets the id field name.
            /// </summary>
            public static string Id => "id";

            /// <summary>
            /// Gets the wikidata field name.
            /// </summary>
            public static string Wikidata => "wikidata";

            /// <summary>
            /// Gets the short code field name.
            /// </summary>
            public static string ShortCode => "short_code";

            /// <summary>
            /// Gets the language field name.
            /// </summary>
            public static string Language => "language";

            /// <summary>
            /// Gets the text field name.
            /// </summary>
            public static string Text => "text";
        }
    }
}
