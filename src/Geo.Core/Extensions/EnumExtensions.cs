// <copyright file="EnumExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Extensions
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Extension methods for the <see cref="Enum"/> type.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts an enum to its string name.
        /// </summary>
        /// <typeparam name="T">The enum type to convert.</typeparam>
        /// <param name="value">The enum value to convert.</param>
        /// <returns>A string with the enum value name.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there is no <see cref="EnumMemberAttribute"/> on the enum.</exception>
        public static string ToEnumString<T>(this T value)
            where T : Enum
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var enumType = typeof(T);
            var attributes = (EnumMemberAttribute[])enumType
#if NET6_0_OR_GREATER
                .GetField(Enum.GetName(enumType, value) !) !
#else
                .GetField(Enum.GetName(enumType, value))
#endif
                .GetCustomAttributes(typeof(EnumMemberAttribute), true);

            if (attributes.Length == 0)
            {
                throw new InvalidOperationException("There is no EnumMember attribute on the enum value");
            }

#if NET6_0_OR_GREATER
            return attributes.Single().Value!;
#else
            return attributes.Single().Value;
#endif
        }

        /// <summary>
        /// Gets the name of the enum.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value get the name of.</param>
        /// <returns>The name of the enum.</returns>
        public static string GetName<T>(this T value)
            where T : Enum
        {
#if NET6_0_OR_GREATER
            return Enum.GetName(typeof(T), value) !;
#else
            return Enum.GetName(typeof(T), value);
#endif
        }
    }
}
