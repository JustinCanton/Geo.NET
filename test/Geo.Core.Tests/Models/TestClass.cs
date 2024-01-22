// <copyright file="TestClass.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A test data class used during testing.
    /// </summary>
    public class TestClass
    {
        /// <summary>
        /// Gets or sets the test field.
        /// </summary>
        [JsonPropertyName("TestField")]
        public int TestField { get; set; }
    }
}
