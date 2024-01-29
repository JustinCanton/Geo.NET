// <copyright file="JsonStringEnumMemberConverterTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests.Converters
{
    using System.Text.Json;
    using FluentAssertions;
    using Geo.Core.Converters;
    using Geo.Core.Tests.Models;
    using Xunit;

    /// <summary>
    /// Tests for the <see cref="JsonStringEnumMemberConverter{T}"/>.
    /// </summary>
    public class JsonStringEnumMemberConverterTests
    {
        [Fact]
        public void Deserialize_WithNoEnumMember_CorrectlyParses()
        {
            var obj = JsonSerializer.Deserialize<TestEnumClass>("{\"Enum\":4}");
            obj.Enum.Should().Be(Enums.Test.DoesNotHaveEnumMember);
        }

        [Fact]
        public void Deserialize_WithEnumMember_CorrectlyParses()
        {
            var obj = JsonSerializer.Deserialize<TestEnumClass>("{\"Enum\":\"tests_google_services\"}");
            obj.Enum.Should().Be(Enums.Test.HasEnumMember2);
        }
    }
}
