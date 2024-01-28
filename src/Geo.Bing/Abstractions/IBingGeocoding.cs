// <copyright file="IBingGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Bing.Models.Parameters;
    using Geo.Bing.Models.Responses;
    using Geo.Core.Models.Exceptions;

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
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<Response> GeocodingAsync(GeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Bing reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response"/> with the response from Bing.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<Response> ReverseGeocodingAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Bing address geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="AddressGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="Response"/> with the response from Bing.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<Response> AddressGeocodingAsync(AddressGeocodingParameters parameters, CancellationToken cancellationToken = default);
    }
}
