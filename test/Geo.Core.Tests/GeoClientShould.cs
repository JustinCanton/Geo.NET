// <copyright file="GeoClientShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Geo.Core.Models.Exceptions;
    using Geo.Core.Tests.Models;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ClientExecutor"/> class.
    /// </summary>
    public class GeoClientShould : IDisposable
    {
        private const string ApiName = "Test";
        private readonly HttpClient _httpClient;
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoClientShould"/> class.
        /// </summary>
        public GeoClientShould()
        {
            var mockHandler = new Mock<HttpMessageHandler>();

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/ArgumentNullException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new ArgumentNullException("requestUri"));

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/InvalidOperationException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new InvalidOperationException("requestUri"));

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/HttpRequestException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new HttpRequestException());

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/TaskCanceledException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new TaskCanceledException());

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/JsonException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new JsonException());

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new StringContent("{'Message':'Access denied'}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/Failure")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"TestField\":1}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/Success")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            _httpClient = new HttpClient(mockHandler.Object);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Checks an exception is thrown when null is passed for the uri.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnNullUri()
        {
            var sut = new TestGeoClient(_httpClient);

            sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/ArgumentNullException")))
                .Should()
                .ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'requestUri')");
        }

        /// <summary>
        /// Checks an exception is thrown an invalid uri is passed in.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnInvalidUri()
        {
            var sut = new TestGeoClient(_httpClient);

            sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/InvalidOperationException")))
                .Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("requestUri");
        }

        /// <summary>
        /// Checks an exception is thrown on an http failure.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnHttpFailure()
        {
            var sut = new TestGeoClient(_httpClient);

            sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/HttpRequestException")))
                .Should()
                .ThrowAsync<HttpRequestException>()
                .WithMessage("Exception of type 'System.Net.Http.HttpRequestException' was thrown.");
        }

        /// <summary>
        /// Checks an exception is thrown when the task is cancelled.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnCancelledRequest()
        {
            var sut = new TestGeoClient(_httpClient);

            sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/TaskCanceledException")))
                .Should()
                .ThrowAsync<TaskCanceledException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnInvalidJson2()
        {
            var sut = new TestGeoClient(_httpClient);

            sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/JsonException")))
                .Should()
                .ThrowAsync<JsonException>();
        }

        /// <summary>
        /// Test the return contains the error json when the status code is not successful.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task ReturnsErrorJson()
        {
            var sut = new TestGeoClient(_httpClient);

            (await sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/Failure")))
                .Should()
                .ThrowAsync<GeoNETException>())
                .WithMessage("The Test request failed.")
                .And.Data["responseBody"].Should().Be("{'Message':'Access denied'}");
        }

        /// <summary>
        /// Test the return object is properly parsed and returned.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task SuccesfullyReturnObject()
        {
            var sut = new TestGeoClient(_httpClient);

            var result = await sut.GetAsync<TestClass>(new Uri("http://test.com/Success"));
            result.TestField.Should().Be(1);
        }

        /// <summary>
        /// Checks an exception is thrown when null is passed for the uri.
        /// </summary>
        [Fact]
        public async Task ThrowWrappedExceptionOnNullUri()
        {
            var sut = new TestGeoClient(_httpClient);

            (await sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/ArgumentNullException")))
                .Should()
                .ThrowAsync<GeoNETException>())
                .WithInnerException<ArgumentNullException>();
        }

        /// <summary>
        /// Checks an exception is thrown an invalid uri is passed in.
        /// </summary>
        [Fact]
        public async Task ThrowWrappedExceptionOnInvalidUri()
        {
            var sut = new TestGeoClient(_httpClient);

            (await sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/InvalidOperationException")))
                .Should()
                .ThrowAsync<GeoNETException>())
                .WithInnerException<InvalidOperationException>();
        }

        /// <summary>
        /// Checks an exception is thrown on an http failure.
        /// </summary>
        [Fact]
        public async Task ThrowWrappedExceptionOnHttpFailure()
        {
            var sut = new TestGeoClient(_httpClient);

            (await sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/HttpRequestException")))
                .Should()
                .ThrowAsync<GeoNETException>())
                .WithInnerException<HttpRequestException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the task is cancelled.
        /// </summary>
        [Fact]
        public async Task ThrowWrappedExceptionOnCancelledRequest()
        {
            var sut = new TestGeoClient(_httpClient);

            (await sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/TaskCanceledException")))
                .Should()
                .ThrowAsync<GeoNETException>())
                .WithInnerException<TaskCanceledException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Fact]
        public async Task ThrowWrappedExceptionOnInvalidJson()
        {
            var sut = new TestGeoClient(_httpClient);

            (await sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/JsonException")))
                .Should()
                .ThrowAsync<GeoNETException>())
                .WithInnerException<JsonException>();
        }

        /// <summary>
        /// Test the return contains the error json when the status code is not successful.
        /// </summary>
        [Fact]
        public void ThrowWrappedExceptionOnErrorJson()
        {
            var sut = new TestGeoClient(_httpClient);

            sut.Invoking(x => x.GetAsync<TestClass>(new Uri("http://test.com/Failure")))
                .Should()
                .ThrowAsync<GeoNETException>()
                .Where(x =>
                    x.Data.Count == 3 &&
                    x.Data["responseBody"].ToString() == "{'Message':'Access denied'}" &&
                    (HttpStatusCode)x.Data["responseStatusCode"] == HttpStatusCode.Forbidden &&
                    x.Data["uri"].ToString() == "http://test.com/Failure");
        }

        /// <summary>
        /// Test the return object is properly parsed and returned.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task SuccesfullyReturnOnlyObject()
        {
            var sut = new TestGeoClient(_httpClient);

            var result = await sut.GetAsync<TestClass>(new Uri("http://test.com/Success"));
            result.TestField.Should().Be(1);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">A boolean flag indicating whether or not to dispose of objects.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _httpClient?.Dispose();

                foreach (var message in _responseMessages)
                {
                    message?.Dispose();
                }
            }

            _disposed = true;
        }
    }
}