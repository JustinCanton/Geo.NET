// <copyright file="TestException.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Core.Tests.Models
{
    using System;

    public class TestException : Exception
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
