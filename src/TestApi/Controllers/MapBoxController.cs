// <copyright file="MapBoxController.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace TestApi.Controllers
{
    using System.Threading.Tasks;
    using Geo.MapBox.Abstractions;
    using Geo.MapBox.Models.Parameters;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class MapBoxController : ControllerBase
    {
        private readonly IMapBoxGeocoding _mapBoxGeocoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapBoxController"/> class.
        /// </summary>
        /// <param name="mapBoxGeocoding">A <see cref="IMapBoxGeocoding"/> used for MapBox geocoding.</param>
        public MapBoxController(IMapBoxGeocoding mapBoxGeocoding)
        {
            _mapBoxGeocoding = mapBoxGeocoding;
        }

        [HttpGet("geocoding")]
        public async Task<IActionResult> GetGeocodingResults([FromQuery] GeocodingParameters parameters)
        {
            var results = await _mapBoxGeocoding.GeocodingAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }

        [HttpGet("reverse-geocoding")]
        public async Task<IActionResult> GetReverseGeocodingResults([FromQuery] ReverseGeocodingParameters parameters)
        {
            var results = await _mapBoxGeocoding.ReverseGeocodingAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }
    }
}
