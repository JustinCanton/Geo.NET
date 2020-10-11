﻿// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    /// <summary>
    /// The base parameters that are used with all here requests.
    /// </summary>
    public class BaseParameters
    {
        /// <summary>
        /// Gets or sets the language to be used for result rendering from a list of BCP47 compliant Language Codes.
        /// </summary>
        public string Language { get; set; }
    }
}