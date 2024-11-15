using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripsAPI.Data;
using TripsAPI.Models.Domain;
using TripsAPI.Models.DTO;

namespace TripsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        public readonly DataContext _context;

        public LocationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            try
            {
                var locations = await _context.Locations.ToListAsync();
                return Ok(locations);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error getting locations");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            try
            {
                var location = await _context.Locations.FindAsync(id);
                if (location == null)
                {
                    return NotFound();
                }
                return Ok(location);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error getting location");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(LocationDTO locationDTO)
        {
            try
            {
                var location = new Location {
                    Name = locationDTO.Name,
                    LocationType = locationDTO.LocationType,
                    Country = locationDTO.Country,
                    State = locationDTO.State,
                    Latitude = locationDTO.Latitude,
                    Longitude = locationDTO.Longitude
                };

                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                return Ok(location);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error creating location");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Location>> PutLocation(Location location)
        {
            try
            {
                var updatedLocation = await _context.Locations.FindAsync(location.Id);
                if (updatedLocation == null)
                {
                    return NotFound("Location not found");
                }
                updatedLocation.Name = location.Name;
                updatedLocation.LocationType = location.LocationType;
                updatedLocation.Country = location.Country;
                updatedLocation.State = location.State;
                updatedLocation.Latitude = location.Latitude;
                updatedLocation.Longitude = location.Longitude;

                await _context.SaveChangesAsync();

                return Ok(location);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error updating location");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> DeleteLocation(int id)
        {
            try
            {
                var location = await _context.Locations.FindAsync(id);
                if (location == null)
                {
                    return NotFound();
                }
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
                return Ok(location);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error deleting location");
            }
        }
    }
}
