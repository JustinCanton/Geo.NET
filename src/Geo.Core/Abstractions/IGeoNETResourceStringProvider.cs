// <copyright file="IGeoNETResourceStringProvider.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    /// <summary>
    /// A provider that reads resource files and returns strings.
    /// </summary>
    public interface IGeoNETResourceStringProvider
    {
        /// <summary>
        /// Gets a formatted resource string.
        /// </summary>
        /// <param name="resourceName">The name of the resource string to fetch.</param>
        /// <param name="args">Optional. Parameter arguments corresponding to the parameters of the resource string.</param>
        /// <returns>A formatted resource string.</returns>
        string GetString(string resourceName, params object[] args);
    }
}
