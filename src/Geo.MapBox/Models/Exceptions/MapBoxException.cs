// <copyright file="MapBoxException.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Exceptions
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Geo.Core.Models.Exceptions;

    /// <summary>
    /// A wrapper exception for any exceptions thrown in the MapBox functionality. The current exceptions wrapped by this exception are listed.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the parameter object is null or the request uri is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the required parameter for the Google request is null or invalid.</exception>
    /// <exception cref="HttpRequestException">
    /// Thrown when the request failed due to an underlying issue such as network connectivity,
    /// DNS failure, server certificate validation or timeout.
    /// </exception>
    /// <exception cref="TaskCanceledException">Thrown when the MapBox request is cancelled.</exception>
    /// <exception cref="JsonException">Thrown when when an error occurs during JSON deserialization.</exception>
    public sealed class MapBoxException : GeoCoreException
    {
        private const string DefaultMessage = "{0} See the inner exception for more information.";

        /// <summary>
        /// Initializes a new instance of the <see cref="MapBoxException"/> class.
        /// </summary>
        public MapBoxException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapBoxException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MapBoxException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapBoxException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public MapBoxException(string message, Exception inner)
            : base(string.Format(CultureInfo.InvariantCulture, DefaultMessage, message), inner)
        {
        }
    }
}
