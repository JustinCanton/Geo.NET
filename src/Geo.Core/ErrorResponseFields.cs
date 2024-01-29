// <copyright file="ErrorResponseFields.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    /// <summary>
    /// The fields added to a response exception in cases of failure.
    /// </summary>
    internal static class ErrorResponseFields
    {
        /// <summary>
        /// The uri of the endpoint called.
        /// </summary>
        public const string Uri = "uri";

        /// <summary>
        /// The body of the request sent to the endpoint.
        /// </summary>
        public const string RequestBody = "requestBody";

        /// <summary>
        /// The status code of the response from the endpoint.
        /// </summary>
        public const string ResponseStatusCode = "responseStatusCode";

        /// <summary>
        /// The body of the response from the endpoint.
        /// </summary>
        public const string ResponseBody = "responseBody";
    }
}
