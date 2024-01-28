// <copyright file="FindPlacesParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Google.Enums;

    /// <summary>
    /// Parameters used for the find places request.
    /// </summary>
    public class FindPlacesParameters : BaseParameters, IKeyParameters
    {
        /// <summary>
        /// Gets or sets the text string on which to search.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Gets or sets the input type.
        /// Phone numbers must be in international format (prefixed by a plus sign ("+"), followed by the country code, then the phone number itself).
        /// The default is text query.
        /// </summary>
        public InputType InputType { get; set; } = InputType.TextQuery;

        /// <summary>
        /// Gets the list of fields to return from the request.
        /// </summary>
        public IList<string> Fields { get; } = new List<string>();

        /// <summary>
        /// Gets or sets information that will prefer results in a specified area.
        /// If this parameter is not specified, the API uses IP address biasing by default.
        /// This parameter can be set to one of:
        /// <see cref="Coordinate"/>
        /// <see cref="Boundaries"/>
        /// <see cref="Circle"/>
        /// and will be passed on using that shape as the bounding information.
        /// </summary>
        public BaseBounding LocationBias { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
