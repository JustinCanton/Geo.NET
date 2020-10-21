// <copyright file="Response.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models.Responses
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The response object from the Bing API.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets a copyright notice.
        /// </summary>
        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        /// <summary>
        /// Gets or sets a URL that references a brand image to support contractual branding requirements.
        /// </summary>
        [JsonProperty("brandLogoUri")]
        public Uri BrandLogoUri { get; set; }

        /// <summary>
        /// Gets or sets the HTTP Status code for the request.
        /// </summary>
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the HTTP status code.
        /// </summary>
        [JsonProperty("statusDescription")]
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
        [JsonProperty("authenticationResultCode")]
        public string AuthenticationResultCode { get; set; }

        /// <summary>
        /// Gets a collection of error descriptions.
        /// For example, ErrorDetails can identify parameter values that are not valid or missing.
        /// </summary>
        [JsonProperty("errorDetails")]
        public List<string> ErrorDetails = new List<string>();

        /// <summary>
        /// Gets or sets a unique identifier for the request.
        /// </summary>
        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        /// <summary>
        /// Gets a collection of ResourceSet objects.
        /// A ResourceSet is a container of Resources returned by the request. For more information, see the ResourceSet section below.
        /// </summary>
        [JsonProperty("resourceSets")]
        public List<ResourceSet> ResourceSets { get; } = new List<ResourceSet>();
    }
}
