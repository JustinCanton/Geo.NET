// <copyright file="ArcGISKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
    public class ArcGISKeyContainerShould
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