// <copyright file="ClientExecutor.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core.Models;
    using Geo.Core.Models.Exceptions;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Newtonsoft.Json;

    /// <summary>
    /// A base class used for calls to APIs.
    /// </summary>
    public class ClientExecutor
    {
        private readonly HttpClient _client;
        private readonly IGeoNETExceptionProvider _exceptionProvider;
        private readonly IStringLocalizer _localizer;
        private readonly ILogger<ClientExecutor> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientExecutor"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the APIs.</param>
        /// <param name="exceptionProvider">An <see cref="IGeoNETExceptionProvider"/> used to provide exceptions based on an exception type.</param>
        /// <param name="localizerFactory">An <see cref="IStringLocalizerFactory"/> used to create a localizer for localizing log or exception messages.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> used to create a logger used for logging information.</param>
        public ClientExecutor(
            HttpClient client,
            IGeoNETExceptionProvider exceptionProvider,
            IStringLocalizerFactory localizerFactory,
            ILoggerFactory loggerFactory = null)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _exceptionProvider = exceptionProvider ?? throw new ArgumentNullException(nameof(exceptionProvider));
            _localizer = localizerFactory?.Create(typeof(ClientExecutor)) ?? throw new ArgumentNullException(nameof(localizerFactory));
            _logger = loggerFactory?.CreateLogger<ClientExecutor>() ?? NullLogger<ClientExecutor>.Instance;
        }

        /// <summary>
        /// Places a call to the API based on a uri.
        /// </summary>
        /// <typeparam name="TResult">The return type to parse the response into.</typeparam>
        /// <typeparam name="TException">The exception type to return in case of any failure.</typeparam>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="apiName">The name of the API being called for exception logging purposes.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <typeparamref name="TResult"/>.</returns>
        /// <exception>An exception of type <typeparamref name="TException"/> thrown when any exception occurs and wraps the original exception.</exception>
        public async Task<TResult> CallAsync<TResult, TException>(Uri uri, string apiName, CancellationToken cancellationToken = default)
            where TResult : class
            where TException : GeoCoreException
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            CallResult<TResult> response;

            try
            {
                response = await CallAsync<TResult>(uri, cancellationToken).ConfigureAwait(false);
            }
            catch (ArgumentNullException ex)
            {
                throw _exceptionProvider.GetException<TException>(_localizer["Null Uri", apiName].ToString(), ex);
            }
            catch (InvalidOperationException ex)
            {
                throw _exceptionProvider.GetException<TException>(_localizer["Invalid Uri", apiName].ToString(), ex);
            }
            catch (HttpRequestException ex)
            {
                throw _exceptionProvider.GetException<TException>(_localizer["Request Failed", apiName].ToString(), ex);
            }
            catch (TaskCanceledException ex)
            {
                throw _exceptionProvider.GetException<TException>(_localizer["Request Cancelled", apiName].ToString(), ex);
            }
            catch (JsonReaderException ex)
            {
                throw _exceptionProvider.GetException<TException>(_localizer["Reader Failed To Parse", apiName].ToString(), ex);
            }
            catch (JsonSerializationException ex)
            {
                throw _exceptionProvider.GetException<TException>(_localizer["Serializer Failed To Parse", apiName].ToString(), ex);
            }
            catch (Exception ex)
            {
                throw _exceptionProvider.GetException<TException>(_localizer["Request Failed Exception", apiName].ToString(), ex);
            }

            if (!response.IsSuccessful)
            {
                var ex = _exceptionProvider.GetException<TException>(_localizer["Request Failure", apiName].ToString(), null);
                ex.Data.Add("uri", uri);
                ex.Data.Add("responseStatusCode", response.StatusCode);
                ex.Data.Add("responseBody", response.Body);

                _logger.ApiCallFailed(uri, ex);

                throw ex;
            }

            return response.Result;
        }

        /// <summary>
        /// Places a call to the API based on a uri.
        /// </summary>
        /// <typeparam name="TResult">The return type to parse the response into.</typeparam>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A named <see cref="Tuple{T1, T2}"/> with the Result <typeparamref name="TResult"/> if successful or the JSON string if unsuccessful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request uri is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the request uri is invalid.</exception>
        /// <exception cref="HttpRequestException">
        /// Thrown when the request failed due to an underlying issue such as network connectivity,
        /// DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="TaskCanceledException">Thrown when the request is cancelled.</exception>
        /// <exception cref="JsonReaderException">Thrown when an error occurs while reading the return JSON text.</exception>
        /// <exception cref="JsonSerializationException">Thrown when when an error occurs during JSON deserialization.</exception>
        internal async Task<CallResult<TResult>> CallAsync<TResult>(Uri uri, CancellationToken cancellationToken = default)
            where TResult : class
        {
            var response = await _client.GetAsync(uri, cancellationToken).ConfigureAwait(false);

#if NET5_0_OR_GREATER
            var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif

            if (!response.IsSuccessStatusCode)
            {
                return new CallResult<TResult>(response.StatusCode, json);
            }

            return new CallResult<TResult>(JsonConvert.DeserializeObject<TResult>(json), response.StatusCode, json);
        }
    }
}
