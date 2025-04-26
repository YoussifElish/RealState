using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Abstactions;
using RealState.Contracts.ContactLead;
using RealState.Services;

namespace RealState.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactLeadController(IContactLeadService _contactLeadService) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddContactLead([FromBody] ContactLeadRequest request)
        {
            var result = await _contactLeadService.AddContactLead(request);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPost("Assign")]
        public async Task<IActionResult> AssignToEmployee([FromQuery] int id, [FromQuery] string employeeId)
        {
            var result = await _contactLeadService.AssignToEmployee(id, employeeId);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllContactLead()
        {
            var result = await _contactLeadService.GetAllContactLead();
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactLead(int id)
        {
            var result = await _contactLeadService.GetContactLead(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("MarkAsDone/{id}")]
        public async Task<IActionResult> MarkAsDone(int id)
        {
            var result = await _contactLeadService.MarkAsDone(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
