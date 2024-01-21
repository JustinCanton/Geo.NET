// <copyright file="TypeExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Extension methods for the <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets an attribute from a type based on a property name.
        /// </summary>
        /// <typeparam name="T">The type of the attribute to return.</typeparam>
        /// <param name="type">The type to fetch the attribute from.</param>
        /// <param name="propertyName">The name of the property to get the attribute for.</param>
        /// <returns><typeparamref name="T"/> if the attribute is found, otherwise null.</returns>
        public static T GetAttribute<T>(this Type type, string propertyName)
            where T : Attribute
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var property = type
                .GetProperties()
                .Where(x => x.Name == propertyName)
                .FirstOrDefault();

            if (property == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The property '{0}' does not exist on the type '{1}'", propertyName, type.Name));
            }

            return property
                .GetCustomAttributes(typeof(T), true)
                .FirstOrDefault() as T;
        }
    }
}
