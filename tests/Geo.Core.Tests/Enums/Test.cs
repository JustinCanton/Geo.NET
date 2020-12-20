// <copyright file="Test.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Test enum values.
    /// </summary>
    public enum Test
    {
        /// <summary>
        /// A default enum value.
        /// </summary>
        Unknown,

        /// <summary>
        /// An enum value with an enum member attribute.
        /// </summary>
        [EnumMember(Value = "test_state")]
        HasEnumMember1,

        /// <summary>
        /// An enum value with an enum member attribute.
        /// </summary>
        [EnumMember(Value = "tests_google_services")]
        HasEnumMember2,

        /// <summary>
        /// An enum value with an enum member attribute.
        /// </summary>
        [EnumMember(Value = "test")]
        HasEnumMember3,

        /// <summary>
        /// An enum value without an enum member attribute.
        /// </summary>
        DoesNotHaveEnumMember,
    }
}
