// <copyright file="GeoNETResourceStringProviderFactory.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// An implementation for the <see cref="IGeoNETResourceStringProviderFactory"/>.
    /// </summary>
    public sealed class GeoNETResourceStringProviderFactory : IGeoNETResourceStringProviderFactory
    {
        private static IDictionary<(string resourceFileName, Assembly assembly), ResourceManager> _resourceManagers = new ConcurrentDictionary<(string resourceFileName, Assembly assembly), ResourceManager>();

        /// <inheritdoc/>
        public IGeoNETResourceStringProvider CreateResourceStringProvider<TResource>()
        {
            return CreateResourceStringProvider(typeof(TResource));
        }

        /// <inheritdoc/>
        public IGeoNETResourceStringProvider CreateResourceStringProvider(Type resourceType)
        {
            if (resourceType == null)
            {
                throw new ArgumentNullException(nameof(resourceType));
            }

            return CreateResourceStringProvider(CreateResourceBaseName(resourceType), resourceType.Assembly);
        }

        /// <inheritdoc/>
        public IGeoNETResourceStringProvider CreateResourceStringProvider(string resourceFileName, Assembly assembly = null)
        {
            if (string.IsNullOrEmpty(resourceFileName))
            {
                throw new ArgumentException("The resource file name cannot be null or empty", nameof(resourceFileName));
            }

            if (_resourceManagers.TryGetValue((resourceFileName, assembly), out var resourceManager))
            {
                return new GeoNETResourceStringProvider(resourceManager);
            }

            resourceManager = new ResourceManager(resourceFileName, assembly == null ? Assembly.GetCallingAssembly() : assembly);
            _resourceManagers.Add((resourceFileName, assembly), resourceManager);

            return new GeoNETResourceStringProvider(resourceManager);
        }

        private static string CreateResourceBaseName(Type resourceType)
        {
            var assemblyName = resourceType.Assembly.GetName().Name;
            var resourceName = resourceType.FullName;
            var resourceFolderName = assemblyName + ".Resources";
            return resourceName.Replace(assemblyName, resourceFolderName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
