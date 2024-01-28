// <copyright file="MapQuestOptions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Settings
{
    /// <summary>
    /// A builder used to configure an instance of the MapQuest geocoding.
    /// </summary>
    public class MapQuestOptions : KeyOptions<IMapQuestGeocoding>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to use the licensed data endpoints with the MapQuest API calls.
        /// The default is false.
        /// </summary>
        public bool UseLicensedEndpoint { get; set; } = false;
    }
}
