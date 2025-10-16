// <copyright file="INominatimGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core.Models.Exceptions;
    using Geo.Nominatim.Models.Parameters;
    using Geo.Nominatim.Models.Responses;

    /// <summary>
    /// An interface for calling the Nominatim geocoding methods.
    /// </summary>
    public interface INominatimGeocoding
    {
        /// <summary>
        /// Calls the Nominatim search API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="SearchParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="SearchResponse"/> with the response from Nominatim.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<SearchResponse> SearchAsync(SearchParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Nominatim reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodingParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="ReverseGeocodingResponse"/> with the response from Nominatim.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<ReverseGeocodingResponse> ReverseGeocodeAsync(ReverseGeocodingParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the Nominatim lookup API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="LookupParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="LookupResponse"/> with the response from Nominatim.</returns>
        /// <exception cref="GeoNETException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<LookupResponse> LookupAsync(LookupParameters parameters, CancellationToken cancellationToken = default);
    }
}
