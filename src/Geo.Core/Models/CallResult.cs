// <copyright file="CallResult.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Models
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
        /// <param name="body">The string body of the call.</param>
        public CallResult(TResult result, HttpStatusCode statusCode, string body)
        {
            IsSuccessful = true;
            Result = result;
            StatusCode = statusCode;
            Body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallResult{TResult}"/> class.
        /// </summary>
        /// <param name="statusCode">The status code of the call.</param>
        /// <param name="body">The string body of the call.</param>
        public CallResult(HttpStatusCode statusCode, string body)
        {
            IsSuccessful = false;
            Result = null;
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
        public TResult Result { get; }

        /// <summary>
        /// Gets the status code resulting from the call.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the body of the result.
        /// </summary>
        public string Body { get; }
    }
}
