// <copyright file="TestClientExecutor.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests.Models
{
    using System.Net.Http;
    using Geo.Core;

    /// <summary>
    /// A test executor class for making http calls.
    /// </summary>
    public class TestClientExecutor : ClientExecutor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestClientExecutor"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for calls.</param>
        /// <param name="exceptionProvider">An <see cref="IGeoNETExceptionProvider"/> used to provide exceptions based on an exception type.</param>
        /// <param name="resourceStringProviderFactory">An <see cref="IGeoNETResourceStringProviderFactory"/> used to create a resource string provider for log or exception messages.</param>
        public TestClientExecutor(
            HttpClient client,
            IGeoNETExceptionProvider exceptionProvider,
            IGeoNETResourceStringProviderFactory resourceStringProviderFactory)
            : base(client, exceptionProvider, resourceStringProviderFactory)
        {
        }
    }
}
