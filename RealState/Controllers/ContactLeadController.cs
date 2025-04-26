using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Abstactions;
using RealState.Abstactions.Consts;
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
        [Authorize(Roles = DefaultRoles.Admin)]
        [HttpPost("Assign")]
        public async Task<IActionResult> AssignToEmployee([FromQuery] int id, [FromQuery] string employeeId)
        {
            var result = await _contactLeadService.AssignToEmployee(id, employeeId);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.Employee}")]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllContactLead()
        {
            var result = await _contactLeadService.GetAllContactLead();
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.Employee}")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactLead(int id)
        {
            var result = await _contactLeadService.GetContactLead(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.Employee}")]

        [HttpPut("MarkAsDone/{id}")]
        public async Task<IActionResult> MarkAsDone(int id)
        {
            var result = await _contactLeadService.MarkAsDone(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
