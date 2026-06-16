// <copyright file="IOpenRouteServiceGeocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService
{
    using System.Threading;
    using System.Threading.Tasks;
    using Geo.Core.Models.Exceptions;
    using Geo.OpenRouteService.Models.Parameters;
    using Geo.OpenRouteService.Models.Responses;

    /// <summary>
    /// An interface for calling the OpenRouteService geocoding API.
    /// </summary>
    public interface IOpenRouteServiceGeocoding
    {
        /// <summary>
        /// Performs a forward geocoding search, converting a text query to coordinates.
        /// </summary>
        /// <param name="parameters">A <see cref="SearchParameters"/> with the search parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="FeatureCollection"/> containing the matching results.</returns>
        /// <exception cref="GeoNETException">Thrown when the parameters are null or invalid, or the API call fails.</exception>
        Task<FeatureCollection> SearchAsync(SearchParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs an autocomplete search for type-ahead suggestions.
        /// </summary>
        /// <param name="parameters">An <see cref="AutocompleteParameters"/> with the autocomplete parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="FeatureCollection"/> containing the suggestions.</returns>
        /// <exception cref="GeoNETException">Thrown when the parameters are null or invalid, or the API call fails.</exception>
        Task<FeatureCollection> AutocompleteAsync(AutocompleteParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs a structured geocoding search using individual address components.
        /// </summary>
        /// <param name="parameters">A <see cref="StructuredSearchParameters"/> with the address component parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="FeatureCollection"/> containing the matching results.</returns>
        /// <exception cref="GeoNETException">Thrown when the parameters are null or invalid, or the API call fails.</exception>
        Task<FeatureCollection> StructuredSearchAsync(StructuredSearchParameters parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Performs a reverse geocoding search, converting coordinates to an address.
        /// </summary>
        /// <param name="parameters">A <see cref="ReverseSearchParameters"/> with the reverse geocoding parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to cancel the request.</param>
        /// <returns>A <see cref="FeatureCollection"/> containing the matching results.</returns>
        /// <exception cref="GeoNETException">Thrown when the parameters are null or invalid, or the API call fails.</exception>
        Task<FeatureCollection> ReverseAsync(ReverseSearchParameters parameters, CancellationToken cancellationToken = default);
    }
}
