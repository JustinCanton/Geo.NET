// <copyright file="BingController.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace TestApi.Controllers
{
    using System.Threading.Tasks;
    using Geo.Bing.Abstractions;
    using Geo.Bing.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class BingController : ControllerBase
    {
        private readonly IBingGeocoding _bingGeocoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingController"/> class.
        /// </summary>
        /// <param name="bingGeocoding">A <see cref="IBingGeocoding"/> used for Bing geocoding.</param>
        public BingController(IBingGeocoding bingGeocoding)
        {
            _bingGeocoding = bingGeocoding;
        }

        [HttpGet("geocoding")]
        public async Task<IActionResult> GetGeocodingResults([FromQuery] GeocodingParameters parameters)
        {
            var results = await _bingGeocoding.GeocodingAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }

        /*[HttpGet("reverse-geocoding")]
        public async Task<IActionResult> GetReverseGeocodingResults([FromQuery] ReverseGeocodingParameters parameters)
        {
            var results = await _googleGeocoding.ReverseGeocodingAsync(parameters).ConfigureAwait(false);

            return Ok(results);
        }*/
    }
}
