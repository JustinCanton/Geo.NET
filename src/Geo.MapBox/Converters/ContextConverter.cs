// <copyright file="ContextConverter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.MapBox.Models.Responses;

    /// <summary>
    /// A converter for a <see cref="Dictionary{TKey, TValue}"/> to a <see cref="Context"/>.
    /// </summary>
    public class ContextConverter : JsonConverter<Context>
    {
        private static readonly Type ContextType = typeof(Context);
        private static readonly Dictionary<string, string> JsonNameMapping = GetJsonNameMapping();

        /// <inheritdoc/>
        public override Context Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the context. Expected to find an object, instead found {0}", reader.TokenType.GetName()));
            }

            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options);

            if (data is null)
            {
                return null;
            }

            return PopulateData(data, options);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Context value, JsonSerializerOptions options)
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

            JsonSerializer.Serialize(writer, value, options);
        }

        private static Dictionary<string, string> GetJsonNameMapping()
        {
            var mapping = new Dictionary<string, string>();
            foreach (var property in ContextType.GetProperties())
            {
                var attributeName = ContextType.GetAttribute<JsonPropertyNameAttribute>(property.Name)?.Name;
                if (attributeName == null)
                {
                    continue;
                }

                mapping.Add(property.Name, attributeName);
            }

            return mapping;
        }

        private static string GetString(Dictionary<string, string> data, string name)
        {
            data.TryGetValue(JsonNameMapping[name], out var value);
            return value;
        }

        private Context PopulateData(Dictionary<string, string> data, JsonSerializerOptions options)
        {
            var context = new Context();
            context.Id = GetString(data, nameof(Context.Id));
            context.Wikidata = GetString(data, nameof(Context.Wikidata));
            context.ShortCode = GetString(data, nameof(Context.ShortCode));
            context.ContextText = GetContextText(data);

            return context;
        }

        private IList<ContextText> GetContextText(Dictionary<string, string> data)
        {
            var texts = new List<ContextText>();

#if NETSTANDARD2_1_OR_GREATER
            var languages = data.Where(x => x.Key.Contains(ContextFields.Language, StringComparison.OrdinalIgnoreCase));
#else
            var languages = data.Where(x => x.Key.Contains(ContextFields.Language));
#endif
            if (!languages.Any())
            {
                // There are 2 cases here:
                // Either there are no extra languages, only the default,
                // or there is a context group without the language tags.
#if NETSTANDARD2_1_OR_GREATER
                var textItems = data.Keys.Where(x => x.Contains("text", StringComparison.OrdinalIgnoreCase));
#else
                var textItems = data.Keys.Where(x => x.Contains("text"));
#endif
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

            // There will be {languages.Count()} items in this context group.
            foreach (var language in languages)
            {
                var contextText = new ContextText();

                var languageEnding = language.Key.Substring(8);
                data.TryGetValue(string.Concat(ContextFields.Language, languageEnding), out var lang);
                if (lang == null)
                {
                    lang = language.Value;
                }

                contextText.Language = lang;
                data.TryGetValue(string.Concat(ContextFields.Text, languageEnding), out var text);
                contextText.Text = text;
                contextText.IsDefault = string.IsNullOrWhiteSpace(languageEnding);

                texts.Add(contextText);
            }

            return texts;
        }

        /// <summary>
        /// A class containing the names of the fields in the context response JSON.
        /// </summary>
        private static class ContextFields
        {
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
