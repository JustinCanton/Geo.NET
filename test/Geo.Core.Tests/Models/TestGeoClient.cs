// <copyright file="TestGeoClient.cs" company="Geo.NET">
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
    public class TestGeoClient : GeoClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestGeoClient"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for calls.</param>
        public TestGeoClient(
            HttpClient client)
            : base(client)
        {
        }

        protected override string ApiName => "Test";
    }
}
