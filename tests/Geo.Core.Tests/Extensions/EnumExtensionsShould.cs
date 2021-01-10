// <copyright file="EnumExtensionsShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests.Extensions
{
    using System;
    using FluentAssertions;
    using Geo.Core.Extensions;
    using Geo.Core.Tests.Enums;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="EnumExtensions"/> class.
    /// </summary>
    [TestFixture]
    public class EnumExtensionsShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Test]
        public void ThrowExceptionWhenNoAttribute()
        {
            Action act = () => Test.DoesNotHaveEnumMember.ToEnumString<Test>();

            act.Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Sequence contains no elements");
        }

        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Test]
        public void GetProperName()
        {
            Test.HasEnumMember1
                .ToEnumString<Test>()
                .Should()
                .Be("test_state");

            Test.HasEnumMember2
                .ToEnumString<Test>()
                .Should()
                .Be("tests_google_services");

            Test.HasEnumMember3
                .ToEnumString<Test>()
                .Should()
                .Be("test");
        }
    }
}