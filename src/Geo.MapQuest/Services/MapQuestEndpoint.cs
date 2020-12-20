// <copyright file="MapQuestEndpoint.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Services
{
    using Geo.MapQuest.Abstractions;

    /// <summary>
    /// A container class for keeping the information about whether the MapQuest should call the lisenced endpoints or not.
    /// </summary>
    public class MapQuestEndpoint : IMapQuestEndpoint
    {
        private readonly bool _useLisencedEndpoint = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapQuestEndpoint"/> class.
        /// </summary>
        /// <param name="useLisencedEndpoint">A flag indicating whether or not to call the lisenced endpoints or not.</param>
        public MapQuestEndpoint(bool useLisencedEndpoint)
        {
            _useLisencedEndpoint = useLisencedEndpoint;
        }

        /// <inheritdoc/>
        public bool UseLicensedEndpoint()
        {
            return _useLisencedEndpoint;
        }
    }
}
