// <copyright file="AutosuggestJsonConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Tests.Converters
{
    using FluentAssertions;
    using Geo.Here.Converters;
    using Geo.Here.Models.Responses;
    using Geo.Here.Tests.Models;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="AutosuggestJsonConverter"/> class.
    /// </summary>
    public class AutosuggestJsonConverterShould
    {
        /// <summary>
        /// Tests the autosuggest query location is correctly parsed.
        /// </summary>
        [Fact]
        public void CorrectlyParseAutosuggestQueryLocation()
        {
            var obj = JsonConvert.DeserializeObject<AutosuggestObject>("{\"Autosuggest\":{\"href\":\"Query href\",\"title\":\"A query title\",\"id\":\"12345\"}}");
            obj.Autosuggest.GetType().Should().Be(typeof(AutosuggestQueryLocation));
            ((AutosuggestQueryLocation)obj.Autosuggest).Id.Should().Be("12345");
            ((AutosuggestQueryLocation)obj.Autosuggest).Title.Should().Be("A query title");
            ((AutosuggestQueryLocation)obj.Autosuggest).Href.Should().Be("Query href");
        }

        /// <summary>
        /// Tests the autosuggest entity location is correctly parsed.
        /// </summary>
        [Fact]
        public void CorrectlyParseAutosuggesEntityLocation()
        {
            var obj = JsonConvert.DeserializeObject<AutosuggestObject>("{\"Autosuggest\":{\"distance\":123,\"address\":{\"label\":\"An entity label\",\"countryName\":\"EntityLand\"},\"title\":\"An entity title\",\"id\":\"67890\"}}");
            obj.Autosuggest.GetType().Should().Be(typeof(AutosuggestEntityLocation));
            ((AutosuggestEntityLocation)obj.Autosuggest).Id.Should().Be("67890");
            ((AutosuggestEntityLocation)obj.Autosuggest).Title.Should().Be("An entity title");
            ((AutosuggestEntityLocation)obj.Autosuggest).Distance.Should().Be(123);
            ((AutosuggestEntityLocation)obj.Autosuggest).Address.Should().NotBeNull();
            ((AutosuggestEntityLocation)obj.Autosuggest).Address.Label.Should().Be("An entity label");
            ((AutosuggestEntityLocation)obj.Autosuggest).Address.CountryName.Should().Be("EntityLand");
        }
    }
}
