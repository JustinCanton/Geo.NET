// <copyright file="ClientExecutorShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Geo.Core;
    using Geo.Core.Tests.Models;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ClientExecutor"/> class.
    /// </summary>
    public class ClientExecutorShould : IDisposable
    {
        private const string ApiName = "Test";
        private readonly HttpClient _httpClient;
        private readonly IGeoNETExceptionProvider _exceptionProvider;
        private readonly IGeoNETResourceStringProviderFactory _resourceStringProviderFactory;
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientExecutorShould"/> class.
        /// </summary>
        public ClientExecutorShould()
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

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("<html></html>"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/JsonReaderException")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/JsonSerializationException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new JsonSerializationException());

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
                .ReturnsAsync(_responseMessages[^1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'TestField':1}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/Success")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            _resourceStringProviderFactory = new GeoNETResourceStringProviderFactory();
            _httpClient = new HttpClient(mockHandler.Object);
            _exceptionProvider = new GeoNETExceptionProvider();
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
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/ArgumentNullException")))
                .Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'requestUri')");
        }

        /// <summary>
        /// Checks an exception is thrown an invalid uri is passed in.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnInvalidUri()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/InvalidOperationException")))
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("requestUri");
        }

        /// <summary>
        /// Checks an exception is thrown on an http failure.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnHttpFailure()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/HttpRequestException")))
                .Should()
                .Throw<HttpRequestException>()
                .WithMessage("Exception of type 'System.Net.Http.HttpRequestException' was thrown.");
        }

        /// <summary>
        /// Checks an exception is thrown when the task is cancelled.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnCancelledRequest()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/TaskCanceledException")))
                .Should()
                .Throw<TaskCanceledException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnInvalidJson1()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/JsonReaderException")))
                .Should()
                .Throw<JsonReaderException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Fact]
        public void ThrowExceptionOnInvalidJson2()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/JsonSerializationException")))
                .Should()
                .Throw<JsonSerializationException>();
        }

        /// <summary>
        /// Test the return contains the error json when the status code is not successful.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task ReturnsErrorJson()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);
            var result = await sut.CallAsync<TestClass>(new Uri("http://test.com/Failure")).ConfigureAwait(false);
            result.IsSuccessful.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Body.Should().Be("{'Message':'Access denied'}");
        }

        /// <summary>
        /// Test the return object is properly parsed and returned.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task SuccesfullyReturnObject()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            var result = await sut.CallAsync<TestClass>(new Uri("http://test.com/Success")).ConfigureAwait(false);
            result.IsSuccessful.Should().BeTrue();
            result.Result.TestField.Should().Be(1);
        }

        /// <summary>
        /// Checks an exception is thrown when null is passed for the uri.
        /// </summary>
        [Fact]
        public void ThrowWrappedExceptionOnNullUri()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/ArgumentNullException"), ApiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<ArgumentNullException>();
        }

        /// <summary>
        /// Checks an exception is thrown an invalid uri is passed in.
        /// </summary>
        [Fact]
        public void ThrowWrappedExceptionOnInvalidUri()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/InvalidOperationException"), ApiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<InvalidOperationException>();
        }

        /// <summary>
        /// Checks an exception is thrown on an http failure.
        /// </summary>
        [Fact]
        public void ThrowWrappedExceptionOnHttpFailure()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/HttpRequestException"), ApiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<HttpRequestException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the task is cancelled.
        /// </summary>
        [Fact]
        public void ThrowWrappedExceptionOnCancelledRequest()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/TaskCanceledException"), ApiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<TaskCanceledException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Fact]
        public void ThrowWrappedExceptionOnInvalidJson1()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/JsonReaderException"), ApiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<JsonReaderException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Fact]
        public void ThrowWrappedExceptionOnInvalidJson2()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/JsonSerializationException"), ApiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<JsonSerializationException>();
        }

        /// <summary>
        /// Test the return contains the error json when the status code is not successful.
        /// </summary>
        [Fact]
        public void ThrowWrappedExceptionOnErrorJson()
        {
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            sut.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/Failure"), ApiName))
                .Should()
                .Throw<TestException>()
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
            var sut = new TestClientExecutor(_httpClient, _exceptionProvider, _resourceStringProviderFactory);

            var result = await sut.CallAsync<TestClass, TestException>(new Uri("http://test.com/Success"), ApiName).ConfigureAwait(false);
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