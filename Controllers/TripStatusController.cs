using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripsAPI.Data;
using TripsAPI.Models.Domain;
using TripsAPI.Models.DTO;

namespace TripsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripStatusController : ControllerBase
    {
        public readonly DataContext _context;

        public TripStatusController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripStatus>>> GetTripStatuses()
        {
            try
            {
                var tripStatuses = await _context.TripStatuses.ToListAsync();
                return Ok(tripStatuses);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error getting trip statuses");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TripStatus>> GetTripStatus(int id)
        {
            try
            {
                var tripStatus = await _context.TripStatuses.FindAsync(id);
                if (tripStatus == null)
                {
                    return NotFound("Trip status not found");
                }
                return Ok(tripStatus);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error getting trip status");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TripStatus>> PostTripStatus(TripStatusDTO tripStatusDTO)
        {
            try
            {
                var tripStatus = new TripStatus
                {
                    Name = tripStatusDTO.Name,
                    Description = tripStatusDTO.Description
                };
                _context.TripStatuses.Add(tripStatus);
                await _context.SaveChangesAsync();

                return Ok(tripStatus);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error creating trip status");
            }
        }

        [HttpPut]
        public async Task<ActionResult<TripStatus>> PutTripStatus(TripStatus tripStatus)
        {
            try
            {
                var tripStatusToUpdate = await _context.TripStatuses.FindAsync(tripStatus.Id);
                if (tripStatusToUpdate == null)
                {
                    return NotFound("Trip status not found");
                }

                tripStatusToUpdate.Name = tripStatus.Name;
                tripStatusToUpdate.Description = tripStatus.Description;

                await _context.SaveChangesAsync();

                return Ok(tripStatus);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error updating trip status");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TripStatus>> DeleteTripStatus(int id)
        {
            try
            {
                var tripStatus = await _context.TripStatuses.FindAsync(id);
                if (tripStatus == null)
                {
                    return NotFound("Trip status not found");
                }

                _context.TripStatuses.Remove(tripStatus);
                await _context.SaveChangesAsync();

                return Ok(tripStatus);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error deleting trip status");
            }
        }
    }
}
