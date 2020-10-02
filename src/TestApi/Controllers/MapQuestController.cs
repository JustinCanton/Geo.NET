// <copyright file="MapQuestController.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace TestApi.Controllers
{
    using System.Threading.Tasks;
    using Geo.MapQuest.Abstractions;
    using Geo.MapQuest.Models.Parameters;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class MapQuestController : ControllerBase
    {
        private readonly IMapQuestGeocoding _mapQuestGeocoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapQuestController"/> class.
        /// </summary>
        /// <param name="mapQuestGeocoding">A <see cref="IMapQuestGeocoding"/> used for MapQuest geocoding.</param>
        public MapQuestController(IMapQuestGeocoding mapQuestGeocoding)
        {
            _mapQuestGeocoding = mapQuestGeocoding;
        }

        [HttpGet("geocoding")]
        public async Task<IActionResult> GetGeocodingResults([FromQuery] GeocodingParameters parameters)
        {
            var results = await _mapQuestGeocoding.GeocodingAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }

        [HttpGet("reverse-geocoding")]
        public async Task<IActionResult> GetReverseGeocodingResults([FromQuery] ReverseGeocodingParameters parameters)
        {
            var results = await _mapQuestGeocoding.ReverseGeocodingAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }
    }
}
