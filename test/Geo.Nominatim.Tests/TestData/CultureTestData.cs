// <copyright file="CultureTestData.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Test data for testing with different cultures.
    /// </summary>
    public class CultureTestData : IEnumerable<object[]>
    {
        /// <inheritdoc/>
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new CultureInfo("en-US") };
            yield return new object[] { new CultureInfo("de-DE") };
            yield return new object[] { new CultureInfo("fr-FR") };
            yield return new object[] { new CultureInfo("es-ES") };
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
