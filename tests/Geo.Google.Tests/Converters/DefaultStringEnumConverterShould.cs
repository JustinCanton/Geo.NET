// <copyright file="DefaultStringEnumConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Tests.Services
{
    using FluentAssertions;
    using Geo.Google.Converters;
    using Geo.Google.Tests.Enums;
    using Geo.Google.Tests.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="DefaultStringEnumConverter{T}"/> class.
    /// </summary>
    [TestFixture]
    public class DefaultStringEnumConverterShould
    {
        /// <summary>
        /// Tests the default value is returned for an unknown name.
        /// </summary>
        [Test]
        public void ReturnUnknownForRandomName()
        {
            var obj = JsonConvert.DeserializeObject<EnumObject>("{'Value':'notProperlySet'}");
            obj.Value.Should().Be(Test.Unknown);
        }

        /// <summary>
        /// Tests the proper value is returned for an known name.
        /// </summary>
        [Test]
        public void ReturnProperNameForExistingName()
        {
            var obj = JsonConvert.DeserializeObject<EnumObject>("{'Value':'tests_google_services'}");
            obj.Value.Should().Be(Test.HasEnumMember2);
        }
    }
}