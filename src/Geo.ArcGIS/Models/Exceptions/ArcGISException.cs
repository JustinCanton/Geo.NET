﻿// <copyright file="ArcGISException.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Exceptions
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// A wrapper exception for any exceptions thrown in the ArcGIS functionality. The current exceptions wrapped by this exception are listed.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the parameter object is null or the request uri is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the required parameter for the Google request is null or invalid.</exception>
    /// <exception cref="HttpRequestException">
    /// Thrown when the request failed due to an underlying issue such as network connectivity,
    /// DNS failure, server certificate validation or timeout.
    /// </exception>
    /// <exception cref="TaskCanceledException">Thrown when the ArcGIS request is cancelled.</exception>
    /// <exception cref="JsonReaderException">Thrown when an error occurs while reading the return JSON text.</exception>
    /// <exception cref="JsonSerializationException">Thrown when when an error occurs during JSON deserialization.</exception>
    public class ArcGISException : Exception
    {
        private static readonly string _defaultMessage = "{0} Please see the inner exception for more information.";

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISException"/> class.
        /// </summary>
        public ArcGISException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ArcGISException(string message)
            : base(string.Format(CultureInfo.InvariantCulture, _defaultMessage, message))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ArcGISException(string message, Exception inner)
            : base(string.Format(CultureInfo.InvariantCulture, _defaultMessage, message), inner)
        {
        }
    }
}