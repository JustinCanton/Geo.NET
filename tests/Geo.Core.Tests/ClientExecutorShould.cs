// <copyright file="ClientExecutorShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Geo.Core;
    using Geo.Core.Tests.Models;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="ClientExecutor"/> class.
    /// </summary>
    [TestFixture]
    public class ClientExecutorShould
    {
        private const string _apiName = "Test";
        private Mock<HttpMessageHandler> _mockHandler;
        private IStringLocalizer<ClientExecutor> _localizer;

        /// <summary>
        /// One time setup information.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockHandler = new Mock<HttpMessageHandler>();

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/ArgumentNullException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new ArgumentNullException("requestUri"));

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/InvalidOperationException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new InvalidOperationException("requestUri"));

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/HttpRequestException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new HttpRequestException());

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/TaskCanceledException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new TaskCanceledException());

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/JsonReaderException")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("<html></html>"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/JsonSerializationException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new JsonSerializationException());

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/Failure")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Content = new StringContent("{'Message':'Access denied'}"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/Success")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'TestField':1}"),
                });

            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            _localizer = new StringLocalizer<ClientExecutor>(factory);
        }

        /// <summary>
        /// Checks an exception is thrown when null is passed for the uri.
        /// </summary>
        [Test]
        public void ThrowExceptionOnNullUri()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/ArgumentNullException")))
                .Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'requestUri')");
        }

        /// <summary>
        /// Checks an exception is thrown an invalid uri is passed in.
        /// </summary>
        [Test]
        public void ThrowExceptionOnInvalidUri()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/InvalidOperationException")))
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("requestUri");
        }

        /// <summary>
        /// Checks an exception is thrown on an http failure.
        /// </summary>
        [Test]
        public void ThrowExceptionOnHttpFailure()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/HttpRequestException")))
                .Should()
                .Throw<HttpRequestException>()
                .WithMessage("Exception of type 'System.Net.Http.HttpRequestException' was thrown.");
        }

        /// <summary>
        /// Checks an exception is thrown when the task is cancelled.
        /// </summary>
        [Test]
        public void ThrowExceptionOnCancelledRequest()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/TaskCanceledException")))
                .Should()
                .Throw<TaskCanceledException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Test]
        public void ThrowExceptionOnInvalidJson1()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/JsonReaderException")))
                .Should()
                .Throw<JsonReaderException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Test]
        public void ThrowExceptionOnInvalidJson2()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/JsonSerializationException")))
                .Should()
                .Throw<JsonSerializationException>();
        }

        /// <summary>
        /// Test the return contains the error json when the status code is not successful.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task ReturnsErrorJson()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);
            var result = await executor.CallAsync<TestClass>(new Uri("http://test.com/Failure")).ConfigureAwait(false);
            result.Item1.Should().BeNull();
            result.Item2.Should().Be("{'Message':'Access denied'}");
        }

        /// <summary>
        /// Test the return object is properly parsed and returned.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task SuccesfullyReturnObject()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);
            var result = await executor.CallAsync<TestClass>(new Uri("http://test.com/Success")).ConfigureAwait(false);
            result.Item1.TestField.Should().Be(1);
        }

        /// <summary>
        /// Checks an exception is thrown when null is passed for the uri.
        /// </summary>
        [Test]
        public void ThrowWrappedExceptionOnNullUri()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/ArgumentNullException"), _apiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<ArgumentNullException>();
        }

        /// <summary>
        /// Checks an exception is thrown an invalid uri is passed in.
        /// </summary>
        [Test]
        public void ThrowWrappedExceptionOnInvalidUri()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/InvalidOperationException"), _apiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<InvalidOperationException>();
        }

        /// <summary>
        /// Checks an exception is thrown on an http failure.
        /// </summary>
        [Test]
        public void ThrowWrappedExceptionOnHttpFailure()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/HttpRequestException"), _apiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<HttpRequestException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the task is cancelled.
        /// </summary>
        [Test]
        public void ThrowWrappedExceptionOnCancelledRequest()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/TaskCanceledException"), _apiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<TaskCanceledException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Test]
        public void ThrowWrappedExceptionOnInvalidJson1()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/JsonReaderException"), _apiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<JsonReaderException>();
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Test]
        public void ThrowWrappedExceptionOnInvalidJson2()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/JsonSerializationException"), _apiName))
                .Should()
                .Throw<TestException>()
                .WithInnerException<JsonSerializationException>();
        }

        /// <summary>
        /// Test the return contains the error json when the status code is not successful.
        /// </summary>
        [Test]
        public void ThrowWrappedExceptionOnErrorJson()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);

            executor.Invoking(x => x.CallAsync<TestClass, TestException>(new Uri("http://test.com/Failure"), _apiName))
                .Should()
                .Throw<TestException>()
                .Where(x => x.Data.Count == 1 && x.Data["responseBody"].ToString() == "{'Message':'Access denied'}");
        }

        /// <summary>
        /// Test the return object is properly parsed and returned.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task SuccesfullyReturnOnlyObject()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var executor = new TestClientExecutor(httpClient, _localizer);
            var result = await executor.CallAsync<TestClass, TestException>(new Uri("http://test.com/Success"), _apiName).ConfigureAwait(false);
            result.TestField.Should().Be(1);
        }
    }
}