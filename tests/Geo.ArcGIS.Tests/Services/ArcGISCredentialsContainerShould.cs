// <copyright file="ArcGISCredentialsContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Tests.Services
{
    using FluentAssertions;
    using Geo.ArcGIS.Services;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="ArcGISCredentialsContainer"/> class.
    /// </summary>
    [TestFixture]
    public class ArcGISCredentialsContainerShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Test]
        public void SetKey()
        {
            var credentialsContainer = new ArcGISCredentialsContainer("abc123", "secret123");
            var credentials = credentialsContainer.GetCredentials();
            credentials.ClientId.Should().Be("abc123");
            credentials.ClientSecret.Should().Be("secret123");
        }
    }
}