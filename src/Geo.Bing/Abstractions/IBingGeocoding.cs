// <copyright file="IBingGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Abstractions
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Bing.Models.Parameters;
    using Geo.Bing.Models.Responses;
    using Newtonsoft.Json;

    /// <summary>
    /// An interface for calling the Bing geocoding API.
    /// </summary>
    public interface IBingGeocoding
    {
        /// <summary>
        /// Calls the Bing geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response"/> with the response from Bing.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the parameter object is null or the request uri is empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the 'Query' parameter is null or empty.</exception>
        /// <exception cref="HttpRequestException">
        /// Thrown when the request failed due to an underlying issue such as network connectivity,
        /// DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="TaskCanceledException">Thrown when the Bing request is cancelled.</exception>
        /// <exception cref="JsonReaderException">Thrown when an error occurs while reading the return JSON text.</exception>
        /// <exception cref="JsonSerializationException">Thrown when when an error occurs during JSON deserialization.</exception>
        Task<Response> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Bing reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response"/> with the response from Bing.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the parameter object is null or the request uri is empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the 'Point' parameter is null or invalid.</exception>
        /// <exception cref="HttpRequestException">
        /// Thrown when the request failed due to an underlying issue such as network connectivity,
        /// DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="TaskCanceledException">Thrown when the Bing request is cancelled.</exception>
        /// <exception cref="JsonReaderException">Thrown when an error occurs while reading the return JSON text.</exception>
        /// <exception cref="JsonSerializationException">Thrown when when an error occurs during JSON deserialization.</exception>
        Task<Response> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Bing address geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="AddressGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response"/> with the response from Bing.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the parameter object is null or the request uri is empty.</exception>
        /// <exception cref="ArgumentException">Thrown when there are no parameters passed in.</exception>
        /// <exception cref="HttpRequestException">
        /// Thrown when the request failed due to an underlying issue such as network connectivity,
        /// DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="TaskCanceledException">Thrown when the Bing request is cancelled.</exception>
        /// <exception cref="JsonReaderException">Thrown when an error occurs while reading the return JSON text.</exception>
        /// <exception cref="JsonSerializationException">Thrown when when an error occurs during JSON deserialization.</exception>
        Task<Response> AddressGeocodingAsync(AddressGeocodingParameters parameters, CancellationToken cancellationToken = default);
    }
}
