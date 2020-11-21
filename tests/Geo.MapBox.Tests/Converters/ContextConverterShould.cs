// <copyright file="ContextConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Tests.Converters
{
    using FluentAssertions;
    using Geo.MapBox.Converters;
    using Geo.MapBox.Tests.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="ContextConverter"/> class.
    /// </summary>
    [TestFixture]
    public class ContextConverterShould
    {
        /// <summary>
        /// Tests the context information is parsed into a proper list of context items.
        /// </summary>
        [Test]
        public void CorrectlyParseContextInformation()
        {
            var obj = JsonConvert.DeserializeObject<ContextObject>("{\"Contexts\": [" +
                "{" +
                    "\"id\": \"postcode.7266040401534490\"," +
                    "\"text_de\": \"4226\"," +
                    "\"text\": \"4226\"," +
                    "\"text_fr\": \"4226\"," +
                    "\"text_en\": \"4226\"," +
                    "\"text_es\": \"4226\"" +
                "}," +
                "{" +
                    "\"id\": \"locality.3059244982453840\"," +
                    "\"wikidata\": \"Q7352919\"," +
                    "\"text_de\": \"Robina\"," +
                    "\"language_de\": \"en\"," +
                    "\"text\": \"Robina\"," +
                    "\"language\": \"en\"," +
                    "\"text_fr\": \"Robina\"," +
                    "\"language_fr\": \"en\"," +
                    "\"text_en\": \"Robina\"," +
                    "\"language_en\": \"en\"," +
                    "\"text_es\": \"Robina\"," +
                    "\"language_es\": \"en\"" +
                "}," +
                "{" +
                    "\"id\": \"place.12294497843533720\"," +
                    "\"wikidata\": \"Q140075\"," +
                    "\"text_de\": \"Gold Coast\"," +
                    "\"language_de\": \"de\"," +
                    "\"text\": \"Gold Coast\"," +
                    "\"language\": \"de\"," +
                    "\"text_fr\": \"Gold Coast\"," +
                    "\"language_fr\": \"fr\"," +
                    "\"text_en\": \"Gold Coast\"," +
                    "\"language_en\": \"en\"," +
                    "\"text_es\": \"Gold Coast\"," +
                    "\"language_es\": \"es\"" +
                "}," +
                "{" +
                    "\"id\": \"region.19496380243439240\"," +
                    "\"wikidata\": \"Q36074\"," +
                    "\"short_code\": \"AU-QLD\"," +
                    "\"text_de\": \"Queensland\"," +
                    "\"language_de\": \"de\"," +
                    "\"text\": \"Queensland\"," +
                    "\"language\": \"de\"," +
                    "\"text_fr\": \"Queensland\"," +
                    "\"language_fr\": \"fr\"," +
                    "\"text_en\": \"Queensland\"," +
                    "\"language_en\": \"en\"," +
                    "\"text_es\": \"Queensland\"," +
                    "\"language_es\": \"es\"" +
                "}," +
                "{" +
                    "\"id\": \"country.9968792518346070\"," +
                    "\"wikidata\": \"Q408\"," +
                    "\"short_code\": \"au\"," +
                    "\"text_de\": \"Australien\"," +
                    "\"language_de\": \"de\"," +
                    "\"text\": \"Australien\"," +
                    "\"language\": \"de\"," +
                    "\"text_fr\": \"Australie\"," +
                    "\"language_fr\": \"fr\"," +
                    "\"text_en\": \"Australia\"," +
                    "\"language_en\": \"en\"," +
                    "\"text_es\": \"Australia\"," +
                    "\"language_es\": \"es\"" +
                "}" +
            "]}");

            obj.Contexts.Count.Should().Be(5);
            obj.Contexts[0].Id.Should().Be("postcode.7266040401534490");
            obj.Contexts[0].ContextText.Count.Should().Be(5);
            obj.Contexts[1].Id.Should().Be("locality.3059244982453840");
            obj.Contexts[1].ContextText.Count.Should().Be(5);
            obj.Contexts[2].Id.Should().Be("place.12294497843533720");
            obj.Contexts[2].ContextText.Count.Should().Be(5);
            obj.Contexts[3].Id.Should().Be("region.19496380243439240");
            obj.Contexts[3].ContextText.Count.Should().Be(5);
            obj.Contexts[4].Id.Should().Be("country.9968792518346070");
            obj.Contexts[4].ContextText.Count.Should().Be(5);
        }
    }
}
