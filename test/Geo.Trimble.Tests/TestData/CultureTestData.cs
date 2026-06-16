// <copyright file="CultureTestData.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Test data when testing different cultures. This test data returns a representative set of cultures
    /// to test culture-invariant behaviour without running tests for every culture in dotnet.
    /// Covers: invariant, period-decimal (en-US), comma-decimal (de-DE, fr-FR, ru-RU), Arabic, and Chinese.
    /// </summary>
    public class CultureTestData : IEnumerable<object[]>
    {
        /// <summary>
        /// Gets the enumerator for the test data.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> of <see cref="object"/>[].</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { CultureInfo.InvariantCulture };
            yield return new object[] { new CultureInfo("en-US") };
            yield return new object[] { new CultureInfo("de-DE") };
            yield return new object[] { new CultureInfo("fr-FR") };
            yield return new object[] { new CultureInfo("ar-SA") };
            yield return new object[] { new CultureInfo("zh-CN") };
            yield return new object[] { new CultureInfo("ru-RU") };
        }

        /// <summary>
        /// Gets the enumerator for the test data.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
