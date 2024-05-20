// <copyright file="GeoClient.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core.Models.Exceptions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// A base class used for calls to APIs.
    /// </summary>
    public abstract class GeoClient
    {
        private static readonly JsonSerializerOptions _options = GetJsonSerializerOptions();

        private readonly ILogger<GeoClient> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoClient"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the APIs.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        protected GeoClient(
            HttpClient client,
#if NETSTANDARD2_0
            ILoggerFactory loggerFactory = null)
#else
            ILoggerFactory? loggerFactory = null)
#endif
        {
            Resources.GeoClient.Culture = CultureInfo.InvariantCulture;
            Client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = loggerFactory?.CreateLogger<GeoClient>() ?? NullLogger<GeoClient>.Instance;
        }

        /// <summary>
        /// Gets the http client associated with the <see cref="GeoClient"/>.
        /// </summary>
        protected HttpClient Client { get; private set; }

        /// <summary>
        /// Gets the name of the API being called for exception logging purposes.
        /// </summary>
        protected abstract string ApiName { get; }

        /// <summary>
        /// Places a call to an API based on a uri.
        /// </summary>
        /// <typeparam name="TResult">The return type to parse the response into.</typeparam>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <typeparamref name="TResult"/>.</returns>
        /// <exception>An exception of type <see cref="GeoNETException"/> thrown when any exception occurs and wraps the original exception.</exception>
        public Task<TResult> GetAsync<TResult>(
            Uri uri,
            CancellationToken cancellationToken = default)
            where TResult : class
        {
            if (uri is null)
            {
                throw new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Null_Uri, ApiName), new ArgumentNullException(nameof(uri)));
            }

            return CallAsync<TResult>(uri, HttpMethod.Get, null, cancellationToken);
        }

        /// <summary>
        /// Places a call to an API based on a uri.
        /// </summary>
        /// <typeparam name="TBody">The type of the <paramref name="body"/>.</typeparam>
        /// <typeparam name="TResult">The return type to parse the response into.</typeparam>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="body">The body to include in the POST request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <typeparamref name="TResult"/>.</returns>
        /// <exception>An exception of type <see cref="GeoNETException"/> thrown when any exception occurs and wraps the original exception.</exception>
        public Task<TResult> PostAsync<TBody, TResult>(
            Uri uri,
            TBody body,
            CancellationToken cancellationToken = default)
            where TBody : class
            where TResult : class
        {
            if (uri is null)
            {
                throw new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Null_Uri, ApiName), new ArgumentNullException(nameof(uri)));
            }

            if (body is null)
            {
                throw new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Null_Body, ApiName), new ArgumentNullException(nameof(body)));
            }

            return PostInternalAsync<TBody, TResult>(uri, body, cancellationToken);
        }

        /// <summary>
        /// Generates a default <see cref="JsonSerializerOptions"/>.
        /// </summary>
        /// <returns>The default <see cref="JsonSerializerOptions"/>.</returns>
        internal static JsonSerializerOptions GetJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return options;
        }

        /// <summary>
        /// Parses an <see cref="HttpResponseMessage"/> to a <see cref="CallResult{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">The return type to parse the response into.</typeparam>
        /// <param name="response">The <see cref="HttpResponseMessage"/> to parse.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <see cref="CallResult{TResult}"/> of <typeparamref name="TResult"/> containing the result of the parsing.</returns>
        /// <exception cref="JsonException">Thrown when when an error occurs during JSON deserialization.</exception>
        internal static async Task<CallResult<TResult>> ParseResponseAsync<TResult>(
            HttpResponseMessage response,
            CancellationToken cancellationToken)
            where TResult : class
        {
            if (!response.IsSuccessStatusCode)
            {
#if NETSTANDARD2_0
                return new CallResult<TResult>(response.StatusCode, await response.Content.ReadAsStringAsync().ConfigureAwait(false));
#else
                return new CallResult<TResult>(response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
#endif
            }

#if NETSTANDARD2_0
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
#else
            var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#endif

#if NETSTANDARD2_0
            return new CallResult<TResult>(await JsonSerializer.DeserializeAsync<TResult>(stream, _options, cancellationToken).ConfigureAwait(false), response.StatusCode);
#else
            return new CallResult<TResult>((await JsonSerializer.DeserializeAsync<TResult>(stream, _options, cancellationToken).ConfigureAwait(false))!, response.StatusCode);
#endif
        }

        /// <summary>
        /// An asynchronous call to an API based on a uri.
        /// </summary>
        /// <typeparam name="TBody">The type of the <paramref name="body"/>.</typeparam>
        /// <typeparam name="TResult">The return type to parse the response into.</typeparam>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="body">The body to include in the POST request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <typeparamref name="TResult"/>.</returns>
        /// <exception>An exception of type <see cref="GeoNETException"/> thrown when any exception occurs and wraps the original exception.</exception>
        internal async Task<TResult> PostInternalAsync<TBody, TResult>(
            Uri uri,
            TBody body,
            CancellationToken cancellationToken = default)
            where TBody : class
            where TResult : class
        {
            var payload = JsonSerializer.Serialize(body, typeof(TBody), _options);
            using (var content = new StringContent(payload, Encoding.UTF8, "application/json"))
            {
                return await CallAsync<TResult>(uri, HttpMethod.Post, content, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Places a call to the API based on a uri.
        /// </summary>
        /// <typeparam name="TResult">The return type to parse the response into.</typeparam>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="method">The http method to use when calling the API.</param>
        /// <param name="content">The content to pass when performing a POST request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <typeparamref name="TResult"/>.</returns>
        /// <exception>An exception of type <see cref="GeoNETException"/> thrown when any exception occurs and wraps the original exception.</exception>
        internal async Task<TResult> CallAsync<TResult>(
            Uri uri,
            HttpMethod method,
#if NETSTANDARD2_0
            HttpContent content,
#else
            HttpContent? content,
#endif
            CancellationToken cancellationToken)
            where TResult : class
        {
            HttpResponseMessage message;

            try
            {
                message = await HttpCallAsync(uri, method, content, cancellationToken).ConfigureAwait(false);
            }
            catch (InvalidOperationException ex)
            {
                throw new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Invalid_Uri, ApiName), ex);
            }
            catch (HttpRequestException ex)
            {
                throw new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Request_Failed, ApiName), ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Request_Cancelled, ApiName), ex);
            }
            catch (Exception ex)
            {
                throw new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Request_Failed_Exception, ApiName), ex);
            }

            CallResult<TResult> response;

            try
            {
                response = await ParseResponseAsync<TResult>(message, cancellationToken).ConfigureAwait(false);
            }
            catch (JsonException ex)
            {
                throw new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Reader_Failed_To_Parse, ApiName), ex);
            }
            catch (Exception ex)
            {
                throw new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Parser_Failed, ApiName), ex);
            }

            if (!response.IsSuccessful)
            {
                var ex = new GeoNETException(string.Format(CultureInfo.InvariantCulture, Resources.GeoClient.Request_Failed, ApiName));
                ex.Data.Add(ErrorResponseFields.Uri, uri);

                if (method == HttpMethod.Post && content != null)
                {
#if NETSTANDARD2_0
                    ex.Data.Add(ErrorResponseFields.RequestBody, await content.ReadAsStringAsync().ConfigureAwait(false));
#else
                    ex.Data.Add(ErrorResponseFields.RequestBody, await content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
#endif
                }

                ex.Data.Add(ErrorResponseFields.ResponseStatusCode, response.StatusCode);
                ex.Data.Add(ErrorResponseFields.ResponseBody, response.Body);

                _logger.ApiCallFailed(uri, ex);

                throw ex;
            }

#if NETSTANDARD2_0
            return response.Result;
#else
            return response.Result!;
#endif
        }

        /// <summary>
        /// Places a call to the API based on a uri.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="method">The http method to use when calling the API.</param>
        /// <param name="content">The content to pass when performing a POST request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <see cref="HttpResponseMessage"/> containing the response from the API.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the request uri is invalid.</exception>
        /// <exception cref="HttpRequestException">
        /// Thrown when the request failed due to an underlying issue such as network connectivity,
        /// DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="TaskCanceledException">Thrown when the request is cancelled.</exception>
        internal async Task<HttpResponseMessage> HttpCallAsync(
            Uri uri,
            HttpMethod method,
#if NETSTANDARD2_0
            HttpContent content,
#else
            HttpContent? content,
#endif
            CancellationToken cancellationToken)
        {
            if (method.Method == HttpMethod.Get.Method)
            {
                return await Client.GetAsync(uri, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                return await Client.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
