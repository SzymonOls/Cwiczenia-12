using EFTest.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace EFTest.Services
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _service;
        public TripsController(ITripService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetTrips(page, pageSize);
            return Ok(result);
        }

        [HttpDelete("/api/clients/{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            var success = await _service.DeleteClient(idClient);
            return success ? NoContent() : BadRequest("Client doesnt exist");
        }

        [HttpPost("/api/trips/{idTrip}/clients")]
        public async Task<IActionResult> AddClientToTrip(int idTrip, AddClientToTripRequest request)
        {
            var result = await _service.AddClientToTrip(idTrip, request);
            return result == "Client added" ? Ok(result) : BadRequest(result);
        }
    }
}