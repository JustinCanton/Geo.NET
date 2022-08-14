// <copyright file="TestException.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests.Models
{
    using System;
    using Geo.Core.Models.Exceptions;

    public sealed class TestException : GeoCoreException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestException"/> class.
        /// </summary>
        public TestException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public TestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
