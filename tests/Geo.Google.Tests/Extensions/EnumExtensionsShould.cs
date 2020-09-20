﻿// <copyright file="EnumExtensionsShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Tests.Extensions
{
    using System;
    using FluentAssertions;
    using Geo.Google.Extensions;
    using Geo.Google.Tests.Enums;
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