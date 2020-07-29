// <copyright file="ClientExecutorShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
    using Geo.Core.Tests.TestModels;
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
        private HttpClient _httpClient;

        /// <summary>
        /// One time setup information.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/ArgumentNullException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new ArgumentNullException("requestUri"));

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/HttpRequestException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new HttpRequestException());

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri("http://test.com/TaskCanceledException")),
                    ItExpr.IsAny<CancellationToken>())
                .Throws(new TaskCanceledException());

            handlerMock
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

            handlerMock
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

            _httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://testing.com/"),
            };
        }

        /// <summary>
        /// Checks an exception is thrown when null is passed for the uri.
        /// </summary>
        [Test]
        public void ThrowExceptionOnNullUri()
        {
            var executor = new TestClientExecutor(_httpClient);

            executor.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/ArgumentNullException")))
                .Should()
                .Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'requestUri')");
        }

        /// <summary>
        /// Checks an exception is thrown on an http failure.
        /// </summary>
        [Test]
        public void ThrowExceptionOnHttpFailure()
        {
            var executor = new TestClientExecutor(_httpClient);

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
            var executor = new TestClientExecutor(_httpClient);

            executor.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/TaskCanceledException")))
                .Should()
                .Throw<TaskCanceledException>()
                .WithMessage("A task was canceled.");
        }

        /// <summary>
        /// Checks an exception is thrown when the returned json is invalid.
        /// </summary>
        [Test]
        public void ThrowExceptionOnInvalidJson()
        {
            var executor = new TestClientExecutor(_httpClient);

            executor.Invoking(x => x.CallAsync<TestClass>(new Uri("http://test.com/JsonReaderException")))
                .Should()
                .Throw<JsonReaderException>();
        }

        /// <summary>
        /// Test the return object is properly parsed and returned.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task SuccesfullyReturnObject()
        {
            var executor = new TestClientExecutor(_httpClient);
            var result = await executor.CallAsync<TestClass>(new Uri("http://test.com/Success")).ConfigureAwait(false);
            result.TestField.Should().Be(1);
        }
    }
}