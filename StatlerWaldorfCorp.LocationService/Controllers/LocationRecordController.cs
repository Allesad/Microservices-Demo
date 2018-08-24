using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Persistence;

namespace StatlerWaldorfCorp.LocationService.Controllers
{
    [Route("locations/{memberId}")]
    [ApiController]
    public class LocationRecordController : ControllerBase
    {
        private readonly ILocationRepository _repository;

        public LocationRecordController(ILocationRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult AddLocation(Guid memberId, [FromBody] LocationRecord record)
        {
            _repository.Add(record);

            return this.Created($"/locations/{memberId}/{record.Id}", record);
        }

        [HttpGet]
        public IActionResult GetLocationsForMember(Guid memberId)
        {
            return Ok(_repository.AllForMember(memberId));
        }

        [HttpGet("latest")]
        public IActionResult GetLatestForMember(Guid memberId)
        {
            return Ok(_repository.GetLatestForMember(memberId));
        }
    }
}
