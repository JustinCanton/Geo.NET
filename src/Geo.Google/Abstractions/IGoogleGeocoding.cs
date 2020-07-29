// <copyright file="IGoogleGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Abstractions
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Google.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// An interface for calling the Google geocoding methods.
    /// </summary>
    public interface IGoogleGeocoding
    {
        /// <summary>
        /// Calls the Google geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="GeocodingResponse"/> with the response from Google.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the parameter object is null or the request uri is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the 'Address' parameter is null or empty.</exception>
        /// <exception cref="HttpRequestException">
        /// Thrown when the request failed due to an underlying issue such as network connectivity,
        /// DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="TaskCanceledException">Thrown when the Google request is cancelled.</exception>
        /// <exception cref="JsonReaderException">Thrown when an error occurs while reading the return JSON text.</exception>
        /// <exception cref="JsonSerializationException">Thrown when when an error occurs during JSON deserialization.</exception>
        Task<GeocodingResponse> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Google reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="GeocodingResponse"/> with the response from Google.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the parameter object is null or the request uri is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the 'Coordinate' parameter is null.</exception>
        /// <exception cref="HttpRequestException">
        /// Thrown when the request failed due to an underlying issue such as network connectivity,
        /// DNS failure, server certificate validation or timeout.
        /// </exception>
        /// <exception cref="TaskCanceledException">Thrown when the Google request is cancelled.</exception>
        /// <exception cref="JsonReaderException">Thrown when an error occurs while reading the return JSON text.</exception>
        /// <exception cref="JsonSerializationException">Thrown when when an error occurs during JSON deserialization.</exception>
        Task<GeocodingResponse> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);
    }
}
