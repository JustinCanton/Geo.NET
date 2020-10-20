// <copyright file="IBingGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Bing.Models.Exceptions;
    using Geo.Bing.Models.Parameters;
    using Geo.Bing.Models.Responses;

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
        /// <exception cref="BingException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<Response> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Bing reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response"/> with the response from Bing.</returns>
        /// <exception cref="BingException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<Response> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Bing address geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="AddressGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response"/> with the response from Bing.</returns>
        /// <exception cref="BingException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<Response> AddressGeocodingAsync(AddressGeocodingParameters parameters, CancellationToken cancellationToken = default);
    }
}
