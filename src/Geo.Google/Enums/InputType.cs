// <copyright file="InputType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Possible input types.
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// The input type is a text query.
        /// </summary>
        [EnumMember(Value = "textquery")]
        TextQuery,

        /// <summary>
        /// The input type is a phone number.
        /// </summary>
        [EnumMember(Value = "phonenumber")]
        PhoneNumber,
    }
}
