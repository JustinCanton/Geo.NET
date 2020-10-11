// <copyright file="ContextText.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Models.Responses
{
    /// <summary>
    /// The context text information that is language specific.
    /// </summary>
    public class ContextText
    {
        /// <summary>
        /// Gets or sets the language of the context text.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the context text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the fontext text is the default language and text returned by the API.
        /// </summary>
        public bool Default { get; set; }
    }
}
