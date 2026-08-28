// <copyright file="Bias.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters.Biases
{
    /// <summary>
    /// The base class for the Geoapify location biases. Biases prefer, but do not restrict to, the area they describe.
    /// When multiple biases are provided, they are combined with OR logic.
    /// </summary>
    public abstract class Bias
    {
        /// <summary>
        /// Returns the bias in the format expected by the Geoapify <c>bias</c> query parameter.
        /// </summary>
        /// <returns>A <see cref="string"/> with the Geoapify representation of the bias.</returns>
        public abstract override string ToString();
    }
}
