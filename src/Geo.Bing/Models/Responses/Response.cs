// <copyright file="Response.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The response object from the Bing API.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets a copyright notice.
        /// </summary>
        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        /// <summary>
        /// Gets or sets a URL that references a brand image to support contractual branding requirements.
        /// </summary>
        [JsonPropertyName("brandLogoUri")]
        public Uri BrandLogoUri { get; set; }

        /// <summary>
        /// Gets or sets the HTTP Status code for the request.
        /// </summary>
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the HTTP status code.
        /// </summary>
        [JsonPropertyName("statusDescription")]
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets a status code that offers additional information about authentication success or failure.
        /// It can be one of the following values:
        /// ValidCredentials
        /// InvalidCredentials
        /// CredentialsExpired
        /// NotAuthorized
        /// NoCredentials
        /// None.
        /// </summary>
        [JsonPropertyName("authenticationResultCode")]
        public string AuthenticationResultCode { get; set; }

        /// <summary>
        /// Gets or sets a collection of error descriptions.
        /// For example, ErrorDetails can identify parameter values that are not valid or missing.
        /// </summary>
        [JsonPropertyName("errorDetails")]
        public IList<string> ErrorDetails { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets a unique identifier for the request.
        /// </summary>
        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }

        /// <summary>
        /// Gets or sets a collection of ResourceSet objects.
        /// A ResourceSet is a container of Resources returned by the request. For more information, see the ResourceSet section below.
        /// </summary>
        [JsonPropertyName("resourceSets")]
        public IList<ResourceSet> ResourceSets { get; set; } = new List<ResourceSet>();
    }
}
