// <copyright file="Photo.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains a reference to an image.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Gets or sets the maximum height of the image.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the maximum width of the image.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets a string used to identify the photo when you perform a Photo request.
        /// </summary>
        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }

        /// <summary>
        /// Gets a list that contains any required attributions. This field will always be present, but may be empty.
        /// </summary>
        [JsonProperty("html_attributions")]
        public List<string> HtmlAttributes { get; } = new List<string>();
    }
}
