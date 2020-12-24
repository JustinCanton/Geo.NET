// <copyright file="MapQuestEndpoint.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Services
{
    using Geo.MapQuest.Abstractions;

    /// <summary>
    /// A container class for keeping the information about whether the MapQuest should call the licensed endpoints or not.
    /// </summary>
    public class MapQuestEndpoint : IMapQuestEndpoint
    {
        private readonly bool _uselicensedEndpoint = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapQuestEndpoint"/> class.
        /// </summary>
        /// <param name="uselicensedEndpoint">A flag indicating whether or not to call the licensed endpoints or not.</param>
        public MapQuestEndpoint(bool uselicensedEndpoint)
        {
            _uselicensedEndpoint = uselicensedEndpoint;
        }

        /// <inheritdoc/>
        public bool UseLicensedEndpoint()
        {
            return _uselicensedEndpoint;
        }
    }
}
