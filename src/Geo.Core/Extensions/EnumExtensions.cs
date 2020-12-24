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
    /// Extension methods for the enum type.
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
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, value);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }
    }
}
