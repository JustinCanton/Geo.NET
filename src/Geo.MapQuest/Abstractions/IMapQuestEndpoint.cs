// <copyright file="IMapQuestEndpoint.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Abstractions
{
    /// <summary>
    /// A container class for keeping which MapQuest endpoint to use.
    /// </summary>
    public interface IMapQuestEndpoint
    {
        /// <summary>
        /// Gets whether or not to use the licensed endpoint or not.
        /// </summary>
        /// <returns>A boolean flag indicating whether or not to call the licensed endpoints or not.</returns>
        bool UseLicensedEndpoint();
    }
}
