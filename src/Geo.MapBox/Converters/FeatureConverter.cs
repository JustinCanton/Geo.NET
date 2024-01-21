// <copyright file="FeatureConverter.cs" company="Geo.NET">
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
    using Geo.MapBox.Models;
    using Geo.MapBox.Models.Responses;

    /// <summary>
    /// A converter for an <see cref="object"/> to a <see cref="Feature"/>.
    /// </summary>
    public class FeatureConverter : JsonConverter<Feature>
    {
        private static readonly Type FeatureType = typeof(Feature);
        private static readonly Dictionary<string, string> JsonNameMapping = GetJsonNameMapping();

        /// <inheritdoc/>
        public override Feature Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                throw new JsonException(string.Format(CultureInfo.InvariantCulture, "Unexpected token while parsing the feature. Expected to find an object, instead found {0}", reader.TokenType.GetName()));
            }

            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);

            if (data is null)
            {
                return null;
            }

            return PopulateData(data, options);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Feature value, JsonSerializerOptions options)
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
            foreach (var property in FeatureType.GetProperties())
            {
                var attributeName = FeatureType.GetAttribute<JsonPropertyNameAttribute>(property.Name)?.Name;
                if (attributeName == null)
                {
                    continue;
                }

                mapping.Add(property.Name, attributeName);
            }

            return mapping;
        }

        private static JsonElement? GetJsonElement(Dictionary<string, object> data, string name)
        {
            data.TryGetValue(JsonNameMapping[name], out var value);
            return (JsonElement?)value;
        }

        private Feature PopulateData(Dictionary<string, object> data, JsonSerializerOptions options)
        {
            var feature = new Feature();
            feature.Id = GetJsonElement(data, nameof(Feature.Id))?.GetString();
            feature.Type = GetJsonElement(data, nameof(Feature.Type))?.GetString();
            var element = GetJsonElement(data, nameof(Feature.PlaceType));
            if (element != null)
            {
                feature.PlaceType = JsonSerializer.Deserialize<IList<string>>(element?.GetRawText(), options);
            }

            feature.Relevance = GetJsonElement(data, nameof(Feature.Relevance))?.GetSingle();
            feature.Address = GetJsonElement(data, nameof(Feature.Address))?.GetString();
            element = GetJsonElement(data, nameof(Feature.Properties));
            if (element != null)
            {
                feature.Properties = JsonSerializer.Deserialize<Properties>(element?.GetRawText(), options);
            }

            feature.PlaceInformation = GetPlaceInformation(data);
            feature.MatchingText = GetJsonElement(data, nameof(Feature.MatchingText))?.GetString();
            feature.MatchingPlaceName = GetJsonElement(data, nameof(Feature.MatchingPlaceName))?.GetString();
            element = GetJsonElement(data, nameof(Feature.BoundingBox));
            if (element != null)
            {
                feature.BoundingBox = JsonSerializer.Deserialize<BoundingBox>(element?.GetRawText(), options);
            }

            element = GetJsonElement(data, nameof(Feature.Center));
            if (element != null)
            {
                feature.Center = JsonSerializer.Deserialize<Coordinate>(element?.GetRawText(), options);
            }

            element = GetJsonElement(data, nameof(Feature.Geometry));
            if (element != null)
            {
                feature.Geometry = JsonSerializer.Deserialize<Geometry>(element?.GetRawText(), options);
            }

            element = GetJsonElement(data, nameof(Feature.Contexts));
            if (element != null)
            {
                feature.Contexts = JsonSerializer.Deserialize<IList<Context>>(element?.GetRawText(), options);
            }

            return feature;
        }

        private IList<PlaceInformation> GetPlaceInformation(Dictionary<string, object> data)
        {
            var infomation = new List<PlaceInformation>();

#if NETSTANDARD2_1_OR_GREATER
            var languages = data.Where(x => x.Key.Contains(FeatureFields.Language, StringComparison.OrdinalIgnoreCase));
#else
            var languages = data.Where(x => x.Key.Contains(FeatureFields.Language));
#endif

            if (!languages.Any())
            {
                // There are 2 cases here:
                // Either there are no extra languages, only the default,
                // or there is a context group without the language tags.
#if NETSTANDARD2_1_OR_GREATER
                var textItems = data.Keys.Where(x => x.Contains("text", StringComparison.OrdinalIgnoreCase) && !x.Contains("context", StringComparison.OrdinalIgnoreCase));
#else
                var textItems = data.Keys.Where(x => x.Contains("text") && !x.Contains("context"));
#endif
                if (textItems.Count() > 1)
                {
                    // This context group does not contain any language keys for an unknown reason.
                    // Try to use the text items language names.
                    foreach (var item in textItems)
                    {
                        var language = item.Substring(4);
                        languages = languages.Append(new KeyValuePair<string, object>(string.Concat(FeatureFields.Language, language), language.Length > 0 ? language.Substring(1) : string.Empty));
                    }
                }
                else
                {
                    // Add in the default language item.
                    languages = languages.Append(new KeyValuePair<string, object>(FeatureFields.Language, new { }));
                }
            }

            // There will be {languages.Count()} items in this place information list.
            foreach (var language in languages)
            {
                var placeInformation = new PlaceInformation();

                var languageEnding = language.Key.Substring(8);
                data.TryGetValue(string.Concat(FeatureFields.Language, languageEnding), out var lang);
                if (lang == null)
                {
                    lang = languageEnding.Length > 0 ? languageEnding.Substring(1) : languageEnding;
                }

                placeInformation.Language = lang?.ToString();
                data.TryGetValue(string.Concat(FeatureFields.Text, languageEnding), out var text);
                placeInformation.Text = text?.ToString();
                data.TryGetValue(string.Concat(FeatureFields.PlaceName, languageEnding), out var placeName);
                placeInformation.PlaceName = placeName?.ToString();

                infomation.Add(placeInformation);
            }

            return infomation;
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
