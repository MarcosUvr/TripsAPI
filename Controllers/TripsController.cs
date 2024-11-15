using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;
using TripsAPI.Data;
using TripsAPI.Models.Domain;
using TripsAPI.Models.DTO;

namespace TripsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;

        public TripsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
        {
            try
            {
                var trips = await _context.Trips
                    .Include(t => t.Origin)
                    .Include(t => t.Destination)
                    .Include(t => t.Operator)
                    .Include(t => t.Status)
                    .ToListAsync();

                return Ok(trips);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error getting trips");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> GetTrip(int id)
        {
            try
            {
                var trip = await _context.Trips
                    .Include(t => t.Origin)
                    .Include(t => t.Destination)
                    .Include(t => t.Operator)
                    .Include(t => t.Status)
                    .FirstOrDefaultAsync(t => t.Id == id);
                if (trip == null)
                {
                    return NotFound("Trip not found");
                }

                return Ok(trip);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error getting trip");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Trip>> PostTrip(PostTripDTO tripDTO)
        {
            try
            {
                var trip = new Trip
                {
                    IdOrigin = tripDTO.IdOrigin,
                    IdDestination = tripDTO.IdDestination,
                    IdOperator = tripDTO.IdOperator,
                    IdStatus = 1, // Pending
                    ScheduledStartDateTime = tripDTO.ScheduledStartDateTime,
                    ScheduledEndDateTime = tripDTO.ScheduledEndDateTime,
                    CreatedDate = DateTime.UtcNow
                };

                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTrip), new { id = trip.Id }, trip);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error creating trip");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip(int id, TripDTO tripDTO)
        {
            try
            {
                var tripToUpdate = await _context.Trips.FindAsync(id);
                if (tripToUpdate == null)
                {
                    return NotFound("Trip not found");
                }

                tripToUpdate.IdOrigin = tripDTO.IdOrigin;
                tripToUpdate.IdDestination = tripDTO.IdDestination;
                tripToUpdate.IdOperator = tripDTO.IdOperator;
                tripToUpdate.IdStatus = tripDTO.IdStatus;
                tripToUpdate.ScheduledStartDateTime = tripDTO.ScheduledStartDateTime;
                tripToUpdate.ScheduledEndDateTime = tripDTO.ScheduledEndDateTime;
                tripToUpdate.ModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(tripToUpdate);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error updating trip");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            try
            {
                var trip = await _context.Trips.FindAsync(id);
                if (trip == null)
                {
                    return NotFound("Trip not found");
                }
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error deleting trip");
            }
        }
    }
}
