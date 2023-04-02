// <copyright file="CultureTestData.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Test data when testing different cultures. This test data returns all cultures in dotnet.
    /// </summary>
    public class CultureTestData : IEnumerable<object[]>
    {
        /// <summary>
        /// Gets the enumerator for the test data.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/> of <see cref="object"/>[].</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (var culture in cultures)
            {
                yield return new object[] { culture };
            }
        }

        /// <summary>
        /// Gets the enumerator for the test data.
        /// </summary>
        /// <returns>A <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
