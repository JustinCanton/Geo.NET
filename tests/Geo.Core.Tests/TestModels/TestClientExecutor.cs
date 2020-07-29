// <copyright file="TestClientExecutor.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Core.Tests.TestModels
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
        public TestClientExecutor(HttpClient client)
            : base(client)
        {
        }
    }
}
