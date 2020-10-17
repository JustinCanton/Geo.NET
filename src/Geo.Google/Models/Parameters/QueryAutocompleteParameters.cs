// <copyright file="AutocompleteParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    /// <summary>
    /// Parameters used in an autocomplete request.
    /// </summary>
    public class QueryAutocompleteParameters : CoordinateParameters
    {
        /// <summary>
        /// Gets or sets the text string on which to search.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Gets or sets the position, in the input term, of the last character that the service uses to match predictions.
        /// For example, if the input is 'Google' and the offset is 3, the service will match on 'Goo'.
        /// The string determined by the offset is matched against the first word in the input term only.
        /// For example, if the input term is 'Google abc' and the offset is 3, the service will attempt to match against 'Goo abc'.
        /// If no offset is supplied, the service will use the whole term.
        /// The offset should generally be set to the position of the text caret.
        /// </summary>
        public uint Offset { get; set; } = 0;
    }
}
