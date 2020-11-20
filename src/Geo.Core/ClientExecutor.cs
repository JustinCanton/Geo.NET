// <copyright file="ClientExecutor.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Core
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// A base class used for calls to APIs.
    /// </summary>
    public abstract class ClientExecutor
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientExecutor"/> class.
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> used for placing calls to the APIs.</param>
        public ClientExecutor(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Places a call to the API based on a uri.
        /// </summary>
        /// <typeparam name="T">The return type to parse the response into.</typeparam>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request uri is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the request uri is invalid.</exception>
        /// <exception cref="HttpRequestException">
        /// Thrown when the request failed due to an underlying issue such as network connectivity,
        /// DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="TaskCanceledException">Thrown when the request is cancelled.</exception>
        /// <exception cref="JsonReaderException">Thrown when an error occurs while reading the return JSON text.</exception>
        /// <exception cref="JsonSerializationException">Thrown when when an error occurs during JSON deserialization.</exception>
        public async Task<T> CallAsync<T>(Uri uri, CancellationToken cancellationToken = default)
        {
            var response = await _client.GetAsync(uri, cancellationToken).ConfigureAwait(false);

            var json = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
