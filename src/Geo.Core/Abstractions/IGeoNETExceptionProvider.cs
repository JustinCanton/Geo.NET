// <copyright file="IExceptionProvider.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using Geo.Core.Models.Exceptions;

    /// <summary>
    /// An interface used to provide an exception based on the exception type.
    /// </summary>
    public interface IGeoNETExceptionProvider
    {
        /// <summary>
        /// Gets an exception of type <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The type of the exception to get.</typeparam>
        /// <param name="message">The message the exception should have.</param>
        /// <param name="innerException">Optional. The inner exception to include in the exception.</param>
        /// <returns>An exception of type <typeparamref name="TException"/>.</returns>
        TException GetException<TException>(string message, Exception innerException = null)
            where TException : GeoCoreException;
    }
}
