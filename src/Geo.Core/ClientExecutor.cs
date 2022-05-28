﻿// <copyright file="ClientExecutor.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Localization;
    using Newtonsoft.Json;

    /// <summary>
    /// A base class used for calls to APIs.
    /// </summary>
    public class ClientExecutor
    {
        private readonly HttpClient _client;
        private readonly IStringLocalizer<ClientExecutor> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientExecutor"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the APIs.</param>
        /// <param name="localizer">A <see cref="IStringLocalizer{T}"/> used for localizing log or exception messages.</param>
        public ClientExecutor(HttpClient client, IStringLocalizer<ClientExecutor> localizer)
        {
            _client = client;
            _localizer = localizer;
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
        /// <exception cref="TException">Thrown when any exception occurs and wraps the original exception.</exception>
        public async Task<TResult> CallAsync<TResult, TException>(Uri uri, string apiName, CancellationToken cancellationToken = default)
            where TResult : class
            where TException : Exception
        {
            (TResult Result, string JSON) response;

            try
            {
                response = await CallAsync<TResult>(uri, cancellationToken).ConfigureAwait(false);
            }
            catch (ArgumentNullException ex)
            {
                throw Activator.CreateInstance(typeof(TException), _localizer["Null Uri", apiName].ToString(), ex) as TException;
            }
            catch (InvalidOperationException ex)
            {
                throw Activator.CreateInstance(typeof(TException), _localizer["Invalid Uri", apiName].ToString(), ex) as TException;
            }
            catch (HttpRequestException ex)
            {
                throw Activator.CreateInstance(typeof(TException), _localizer["Request Failed", apiName].ToString(), ex) as TException;
            }
            catch (TaskCanceledException ex)
            {
                throw Activator.CreateInstance(typeof(TException), _localizer["Request Cancelled", apiName].ToString(), ex) as TException;
            }
            catch (JsonReaderException ex)
            {
                throw Activator.CreateInstance(typeof(TException), _localizer["Reader Failed To Parse", apiName].ToString(), ex) as TException;
            }
            catch (JsonSerializationException ex)
            {
                throw Activator.CreateInstance(typeof(TException), _localizer["Serializer Failed To Parse", apiName].ToString(), ex) as TException;
            }
            catch (Exception ex)
            {
                throw Activator.CreateInstance(typeof(TException), _localizer["Request Failed Exception", apiName].ToString(), ex) as TException;
            }

            if (response.Result is null || !string.IsNullOrEmpty(response.JSON))
            {
                var ex = Activator.CreateInstance(typeof(TException), _localizer["Request Failure", apiName].ToString()) as TException;
                ex.Data.Add("responseBody", response.JSON);
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
        internal async Task<(TResult Result, string JSON)> CallAsync<TResult>(Uri uri, CancellationToken cancellationToken = default)
            where TResult : class
        {
            var response = await _client.GetAsync(uri, cancellationToken).ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return (null, json);
            }

            return (JsonConvert.DeserializeObject<TResult>(json), string.Empty);
        }
    }
}
