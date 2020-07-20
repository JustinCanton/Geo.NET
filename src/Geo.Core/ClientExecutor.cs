// <copyright file="Client.cs" company="Geo.NET">
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
        /// <typeparam name="T">The return type to pasre the response into.</typeparam>
        /// <param name="uri">The <see cref="Uri"/> to call.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for cancelling the request.</param>
        /// <returns>A <typeparamref name="T"/>.</returns>
        public async Task<T> CallAsync<T>(Uri uri, CancellationToken cancellationToken = default)
        {
            TaskCompletionSource<T> task = new TaskCompletionSource<T>();

            var response = _client.GetAsync(uri, cancellationToken);

            try
            {
                await response.ConfigureAwait(false);
            }
            catch (ArgumentNullException ex)
            {
                task.SetException(ex);

                return await task.Task.ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                task.SetException(ex);

                return await task.Task.ConfigureAwait(false);
            }

            if (response.IsFaulted)
            {
                task.SetException(response.Exception);
            }
            else if (response.IsCanceled)
            {
                task.SetCanceled();
            }
            else
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                task.SetResult(JsonConvert.DeserializeObject<T>(json));
            }

            return await task.Task.ConfigureAwait(false);
        }
    }
}
