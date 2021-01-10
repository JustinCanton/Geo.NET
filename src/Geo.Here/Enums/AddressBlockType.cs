// <copyright file="AddressBlockType.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Enums
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Possible address block types.
    /// </summary>
    public enum AddressBlockType
    {
        /// <summary>
        /// Indicates the address block is the entire block.
        /// </summary>
        [EnumMember(Value = "block")]
        Block = 1,

        /// <summary>
        /// Indicates the address block is a subblock of another block.
        /// </summary>
        [EnumMember(Value = "subblock")]
        Subblock,
    }
}
