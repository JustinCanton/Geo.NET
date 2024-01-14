// <copyright file="Photo.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains a reference to an image.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Gets or sets the maximum height of the image.
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the maximum width of the image.
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets a string used to identify the photo when you perform a Photo request.
        /// </summary>
        [JsonPropertyName("photo_reference")]
        public string PhotoReference { get; set; }

        /// <summary>
        /// Gets a list that contains any required attributions. This field will always be present, but may be empty.
        /// </summary>
        [JsonPropertyName("html_attributions")]
        public IList<string> HtmlAttributes { get; } = new List<string>();
    }
}
