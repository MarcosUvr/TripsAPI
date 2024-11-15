using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripsAPI.Data;
using TripsAPI.Models.Domain;
using TripsAPI.Models.DTO;

namespace TripsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly DataContext _context;

        public OperatorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operator>>> GetOperators()
        {
            try
            {
                var operators = await _context.Operators.ToListAsync();
                return Ok(operators);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error getting operators");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Operator>> GetOperator(int id)
        {
            try
            {
                var operatorModel = await _context.Operators.FindAsync(id);
                if (operatorModel == null)
                {
                    return NotFound("Operator not found");
                }

                return Ok(operatorModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error getting operator");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Operator>> PostOperator(OperatorDTO operatorDTO)
        {
            try
            {
                var operatorModel = new Operator
                {
                    Name = operatorDTO.Name,
                    LastName = operatorDTO.LastName,
                    Phone = operatorDTO.Phone,
                    Email = operatorDTO.Email,
                    IsActive = operatorDTO.IsActive
                };
                _context.Operators.Add(operatorModel);
                await _context.SaveChangesAsync();

                return Ok(operatorModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error creating operator");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Operator>> PutOperator(Operator operatorModel)
        {
            try
            {
                var operatorModelToUpdate = await _context.Operators.FindAsync(operatorModel.Id);
                if (operatorModelToUpdate == null)
                {
                    return NotFound("Operator not found");
                }
                operatorModelToUpdate.Name = operatorModel.Name;
                operatorModelToUpdate.LastName = operatorModel.LastName;
                operatorModelToUpdate.Phone = operatorModel.Phone;
                operatorModelToUpdate.Email = operatorModel.Email;
                operatorModelToUpdate.IsActive = operatorModel.IsActive;
                await _context.SaveChangesAsync();

                return Ok(operatorModelToUpdate);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error updating operator");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOperator(int id)
        {
            try
            {
                var operatorModel = await _context.Operators.FindAsync(id);
                if (operatorModel == null)
                {
                    return NotFound();
                }
                _context.Operators.Remove(operatorModel);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error deleting operator");
            }
        }
    }
}
