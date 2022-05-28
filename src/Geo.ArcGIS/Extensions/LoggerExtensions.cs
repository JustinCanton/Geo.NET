// <copyright file="LoggerExtensions.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS
{
    using System;
    using Geo.ArcGIS.Services;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Extension methods for the <see cref="ILogger"/> class.
    /// </summary>
    internal static class LoggerExtensions
    {
        private static readonly Action<ILogger, string, Exception> _error = LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(1, nameof(ArcGISGeocoding)),
            "ArcGISGeocoding: {ErrorMessage}");

        private static readonly Action<ILogger, string, Exception> _warning = LoggerMessage.Define<string>(
            LogLevel.Warning,
            new EventId(2, nameof(ArcGISGeocoding)),
            "ArcGISGeocoding: {WarningMessage}");

        private static readonly Action<ILogger, string, Exception> _debug = LoggerMessage.Define<string>(
            LogLevel.Debug,
            new EventId(3, nameof(ArcGISGeocoding)),
            "ArcGISGeocoding: {DebugMessage}");

        /// <summary>
        /// "ArcGISGeocoding: {ErrorMessage}".
        /// </summary>
        /// <param name="logger">An <see cref="ILogger"/> used to log the error message.</param>
        /// <param name="errorMessage">The error message to log.</param>
        public static void ArcGISError(this ILogger logger, string errorMessage)
        {
            _error(logger, errorMessage, null);
        }

        /// <summary>
        /// "ArcGISGeocoding: {WarningMessage}".
        /// </summary>
        /// <param name="logger">An <see cref="ILogger"/> used to log the warning message.</param>
        /// <param name="warningMessage">The warning message to log.</param>
        public static void ArcGISWarning(this ILogger logger, string warningMessage)
        {
            _warning(logger, warningMessage, null);
        }

        /// <summary>
        /// "ArcGISGeocoding: {DebugMessage}".
        /// </summary>
        /// <param name="logger">An <see cref="ILogger"/> used to log the debug message.</param>
        /// <param name="debugMessage">The debug message to log.</param>
        public static void ArcGISDebug(this ILogger logger, string debugMessage)
        {
            _debug(logger, debugMessage, null);
        }
    }
}
