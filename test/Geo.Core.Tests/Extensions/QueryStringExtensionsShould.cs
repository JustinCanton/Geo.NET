// <copyright file="QueryStringExtensionsShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests.Extensions
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Geo.Core.Extensions;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="QueryStringExtensions"/> class.
    /// </summary>
    public class QueryStringExtensionsShould
    {
        private class TestAdditionalParameters : IAdditionalParameters
        {
            public IDictionary<string, string> AdditionalParameters { get; } = new Dictionary<string, string>();
        }

        private class NullDictionaryAdditionalParameters : IAdditionalParameters
        {
#pragma warning disable CS8603 // Possible null reference return.
            public IDictionary<string, string> AdditionalParameters => null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        /// <summary>
        /// Tests that a null parameters object causes the original query to be returned unchanged.
        /// </summary>
        [Fact]
        public void ReturnUnchangedQueryString_WhenParametersIsNull()
        {
            var query = QueryString.Empty.Add("existing", "value");

            var result = query.AddAdditionalParameters(null);

            result.Should().Be(query);
        }

        /// <summary>
        /// Tests that a null AdditionalParameters dictionary causes the original query to be returned unchanged.
        /// </summary>
        [Fact]
        public void ReturnUnchangedQueryString_WhenAdditionalParametersDictionaryIsNull()
        {
            var query = QueryString.Empty.Add("existing", "value");
            var parameters = new NullDictionaryAdditionalParameters();

            var result = query.AddAdditionalParameters(parameters);

            result.Should().Be(query);
        }

        /// <summary>
        /// Tests that an empty AdditionalParameters dictionary causes the original query to be returned unchanged.
        /// </summary>
        [Fact]
        public void ReturnUnchangedQueryString_WhenAdditionalParametersDictionaryIsEmpty()
        {
            var query = QueryString.Empty.Add("existing", "value");
            var parameters = new TestAdditionalParameters();

            var result = query.AddAdditionalParameters(parameters);

            result.Should().Be(query);
        }

        /// <summary>
        /// Tests that a single additional key/value pair is appended to the query string.
        /// </summary>
        [Fact]
        public void AddSingleKeyValuePair_ToQueryString()
        {
            var parameters = new TestAdditionalParameters();
            parameters.AdditionalParameters.Add("key1", "value1");

            var result = QueryString.Empty.AddAdditionalParameters(parameters);

            result.Value.Should().Contain("key1=value1");
        }

        /// <summary>
        /// Tests that multiple additional key/value pairs are all appended to the query string.
        /// </summary>
        [Fact]
        public void AddMultipleKeyValuePairs_ToQueryString()
        {
            var parameters = new TestAdditionalParameters();
            parameters.AdditionalParameters.Add("key1", "value1");
            parameters.AdditionalParameters.Add("key2", "value2");
            parameters.AdditionalParameters.Add("key3", "value3");

            var result = QueryString.Empty.AddAdditionalParameters(parameters);

            result.Value.Should().Contain("key1=value1");
            result.Value.Should().Contain("key2=value2");
            result.Value.Should().Contain("key3=value3");
        }

        /// <summary>
        /// Tests that an entry with a null value is skipped and not appended to the query string.
        /// </summary>
        [Fact]
        public void SkipEntry_WhenValueIsNull()
        {
            var parameters = new TestAdditionalParameters();
            parameters.AdditionalParameters.Add("nullkey", null);

            var result = QueryString.Empty.AddAdditionalParameters(parameters);

            result.Should().Be(QueryString.Empty);
        }

        /// <summary>
        /// Tests that only null-valued entries are skipped while non-null entries are added.
        /// </summary>
        [Fact]
        public void SkipOnlyNullValueEntries_WhenMixedWithNonNullValues()
        {
            var parameters = new TestAdditionalParameters();
            parameters.AdditionalParameters.Add("key1", "value1");
            parameters.AdditionalParameters.Add("nullkey", null);
            parameters.AdditionalParameters.Add("key3", "value3");

            var result = QueryString.Empty.AddAdditionalParameters(parameters);

            result.Value.Should().Contain("key1=value1");
            result.Value.Should().NotContain("nullkey");
            result.Value.Should().Contain("key3=value3");
        }

        /// <summary>
        /// Tests that additional parameters are appended after existing query parameters.
        /// </summary>
        [Fact]
        public void AppendAdditionalParameters_AfterExistingQueryParameters()
        {
            var query = QueryString.Empty.Add("existing", "value");
            var parameters = new TestAdditionalParameters();
            parameters.AdditionalParameters.Add("extra", "data");

            var result = query.AddAdditionalParameters(parameters);

            result.Value.Should().Contain("existing=value");
            result.Value.Should().Contain("extra=data");
        }
    }
}
