﻿// <copyright file="DefaultStringEnumConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Tests.Converters
{
    using FluentAssertions;
    using Geo.Google.Tests.Enums;
    using Geo.Google.Tests.Models;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="DefaultStringEnumConverter{T}"/> class.
    /// </summary>
    public class DefaultStringEnumConverterShould
    {
        /// <summary>
        /// Tests the default value is returned for an unknown name.
        /// </summary>
        [Fact]
        public void ReturnUnknownForRandomName()
        {
            var obj = JsonConvert.DeserializeObject<EnumObject>("{'Value':'notProperlySet'}");
            obj.Value.Should().Be(Test.Unknown);
        }

        /// <summary>
        /// Tests the proper value is returned for an known name.
        /// </summary>
        [Fact]
        public void ReturnProperNameForExistingName()
        {
            var obj = JsonConvert.DeserializeObject<EnumObject>("{'Value':'tests_google_services'}");
            obj.Value.Should().Be(Test.HasEnumMember2);
        }
    }
}