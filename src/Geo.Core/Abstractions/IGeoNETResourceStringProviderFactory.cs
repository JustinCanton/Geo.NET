// <copyright file="IGeoNETResourceStringProviderFactory.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using System.Reflection;

    /// <summary>
    /// A factory for creating <see cref="IGeoNETResourceStringProvider"/>.
    /// </summary>
    public interface IGeoNETResourceStringProviderFactory
    {
        /// <summary>
        /// Creates an <see cref="IGeoNETResourceStringProvider"/> based on a type.
        /// </summary>
        /// <typeparam name="TResource">The type of the resource to create an <see cref="IGeoNETResourceStringProvider"/> for.</typeparam>
        /// <returns>An <see cref="IGeoNETResourceStringProvider"/>.</returns>
        IGeoNETResourceStringProvider CreateResourceStringProvider<TResource>();

        /// <summary>
        /// Creates an <see cref="IGeoNETResourceStringProvider"/> based on a type.
        /// </summary>
        /// <param name="resourceType">The type of the resource to create an <see cref="IGeoNETResourceStringProvider"/> for.</param>
        /// <returns>An <see cref="IGeoNETResourceStringProvider"/>.</returns>
        IGeoNETResourceStringProvider CreateResourceStringProvider(Type resourceType);

        /// <summary>
        /// Creates an <see cref="IGeoNETResourceStringProvider"/> based on the resouce file name and assembly.
        /// </summary>
        /// <param name="resourceFileName">The name of the resource file.</param>
        /// <param name="assembly">Optional. The assembly the resource file is located in. If not passed in, the current assembly will be used.</param>
        /// <returns>An <see cref="IGeoNETResourceStringProvider"/>.</returns>
        IGeoNETResourceStringProvider CreateResourceStringProvider(string resourceFileName, Assembly assembly = null);
    }
}
