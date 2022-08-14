// <copyright file="LoggerExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Extension methods for the <see cref="ILogger"/> class.
    /// </summary>
    internal static class LoggerExtensions
    {
        private static readonly Action<ILogger, string, Exception> _apiCallFailed = LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(1, nameof(ClientExecutor)),
            "ClientExecutor: The call to the API {URI} failed.");

        /// <summary>
        /// "ClientExecutor: The call to the API {API} failed.".
        /// </summary>
        /// <param name="logger">An <see cref="ILogger"/> used to log the error message.</param>
        /// <param name="uri">The uri that failed.</param>
        /// <param name="ex">The exception that occured.</param>
        public static void ApiCallFailed(this ILogger logger, Uri uri, Exception ex)
        {
            _apiCallFailed(logger, uri.ToString(), ex);
        }
    }
}
