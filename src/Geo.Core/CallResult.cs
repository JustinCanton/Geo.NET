// <copyright file="CallResult.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System.Net;

    /// <summary>
    /// A results class used to store information relating to a call to an API.
    /// </summary>
    /// <typeparam name="TResult">The type of the expected results.</typeparam>
    internal class CallResult<TResult>
        where TResult : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallResult{TResult}"/> class.
        /// </summary>
        /// <param name="result">The result from the call.</param>
        /// <param name="statusCode">The status code of the call.</param>
        public CallResult(TResult result, HttpStatusCode statusCode)
        {
            IsSuccessful = true;
            Result = result;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallResult{TResult}"/> class.
        /// </summary>
        /// <param name="statusCode">The status code of the call.</param>
        /// <param name="body">The string body of the call.</param>
        public CallResult(HttpStatusCode statusCode, string body)
        {
            IsSuccessful = false;
            StatusCode = statusCode;
            Body = body;
        }

        /// <summary>
        /// Gets a value indicating whether the call was successful or not.
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// Gets the result of the call.
        /// </summary>
#if NETSTANDARD2_0
        public TResult Result { get; } = null;
#else
        public TResult? Result { get; } = null;
#endif

        /// <summary>
        /// Gets the status code resulting from the call.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the body of the result.
        /// </summary>
#if NETSTANDARD2_0
        public string Body { get; } = null;
#else
        public string? Body { get; } = null;
#endif
    }
}
