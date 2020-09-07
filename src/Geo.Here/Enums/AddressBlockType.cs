// <copyright file="AddressBlockType.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
