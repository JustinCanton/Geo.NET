// <copyright file="FeatureConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Tests.Converters
{
    using FluentAssertions;
    using Geo.MapBox.Converters;
    using Geo.MapBox.Tests.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="FeatureConverter"/> class.
    /// </summary>
    [TestFixture]
    public class FeatureConverterShould
    {
        /// <summary>
        /// Tests the context information is parsed into a proper list of context items.
        /// </summary>
        [Test]
        public void CorrectlyParseFeatureInformation()
        {
            var obj = JsonConvert.DeserializeObject<FeatureObject>("{\"Features\": [" +
            "{" +
                "\"id\": \"address.4562086697174018\"," +
                "\"type\": \"Feature\"," +
                "\"place_type\": [" +
                "\"address\"" +
                "]," +
                "\"relevance\": 1," +
                "\"properties\": {" +
                    "\"accuracy\": \"point\"" +
                "}," +
                "\"text_de\": \"Easthill Drive\"," +
                "\"place_name_de\": \"123 Easthill Drive, Robina Queensland 4226, Australien\"," +
                "\"text\": \"Easthill Drive\"," +
                "\"place_name\": \"123 Easthill Drive, Robina Queensland 4226, Australien\"," +
                "\"text_fr\": \"Easthill Drive\"," +
                "\"place_name_fr\": \"123 Easthill Drive, Robina Queensland 4226, Australie\"," +
                "\"text_en\": \"Easthill Drive\"," +
                "\"place_name_en\": \"123 Easthill Drive, Robina Queensland 4226, Australia\"," +
                "\"text_es\": \"Easthill Drive\"," +
                "\"place_name_es\": \"123 Easthill Drive, Robina Queensland 4226, Australia\"," +
                "\"center\": [" +
                    "153.379627," +
                    "-28.081626" +
                "]," +
                "\"geometry\": {" +
                    "\"type\": \"Point\"," +
                    "\"coordinates\": [" +
                        "153.379627," +
                        "-28.081626" +
                    "]" +
                "}," +
                "\"address\": \"123\"," +
                "\"context\": [" +
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
                "}]" +
            "}]}");

            obj.Features.Count.Should().Be(1);
            obj.Features[0].PlaceInformation.Count.Should().Be(5);
            obj.Features[0].PlaceInformation[0].PlaceName.Should().Be("123 Easthill Drive, Robina Queensland 4226, Australien");
            obj.Features[0].PlaceInformation[0].Language.Should().Be("de");
            obj.Features[0].PlaceInformation[1].PlaceName.Should().Be("123 Easthill Drive, Robina Queensland 4226, Australien");
            obj.Features[0].PlaceInformation[1].Language.Should().Be(string.Empty);
            obj.Features[0].PlaceInformation[2].PlaceName.Should().Be("123 Easthill Drive, Robina Queensland 4226, Australie");
            obj.Features[0].PlaceInformation[2].Language.Should().Be("fr");
            obj.Features[0].PlaceInformation[3].PlaceName.Should().Be("123 Easthill Drive, Robina Queensland 4226, Australia");
            obj.Features[0].PlaceInformation[3].Language.Should().Be("en");
            obj.Features[0].PlaceInformation[4].PlaceName.Should().Be("123 Easthill Drive, Robina Queensland 4226, Australia");
            obj.Features[0].PlaceInformation[4].Language.Should().Be("es");
        }
    }
}
