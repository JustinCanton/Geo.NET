// <copyright file="IHereGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Here.Models.Exceptions;
    using Geo.Here.Models.Parameters;
    using Geo.Here.Models.Responses;

    /// <summary>
    /// An interface for calling the HERE geocoding methods.
    /// </summary>
    public interface IHereGeocoding
    {
        /// <summary>
        /// Calls the HERE geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="GeocodeParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="GeocodingResponse"/> with the response from HERE.</returns>
        /// <exception cref="HereException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<GeocodingResponse> GeocodingAsync(GeocodeParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the HERE reverse geocoding API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseGeocodeParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="ReverseGeocodingResponse"/> with the response from HERE.</returns>
        /// <exception cref="HereException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<ReverseGeocodingResponse> ReverseGeocodingAsync(ReverseGeocodeParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the HERE discover API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="DiscoverParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="DiscoverResponse"/> with the response from HERE.</returns>
        /// <exception cref="HereException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<DiscoverResponse> DiscoverAsync(DiscoverParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the HERE autosuggest API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="AutosuggestParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="AutosuggestResponse"/> with the response from HERE.</returns>
        /// <exception cref="HereException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<AutosuggestResponse> AutosuggestAsync(AutosuggestParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the HERE browse API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="BrowseParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="BrowseResponse"/> with the response from HERE.</returns>
        /// <exception cref="HereException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<BrowseResponse> BrowseAsync(BrowseParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calls the HERE lookup API and returns the results.
        /// </summary>
        /// <param name="parameters">A <see cref="LookupParameters"/> with the parameters of the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="LookupResponse"/> with the response from HERE.</returns>
        /// <exception cref="HereException">Thrown for multiple different reasons. Check the inner exception for more information.</exception>
        Task<LookupResponse> LookupAsync(LookupParameters parameters, CancellationToken cancellationToken = default);
    }
}
