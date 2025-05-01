using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Abstactions;
using RealState.Abstactions.Consts;
using RealState.Authentication.Filters;
using RealState.Contracts.Users;
using RealState.Services;

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("")]

        [Authorize(Roles = DefaultRoles.Admin)]

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {

            return Ok(await _userService.GetAllAsync(cancellationToken));
        }
        [HttpGet("GetAllEmployees")]
        [Authorize(Roles = DefaultRoles.Admin)]

        public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
        {

            return Ok(await _userService.GetAllEmployeesAsync(cancellationToken));
        }


        [HttpGet("{id}")]
        [Authorize(Roles = DefaultRoles.Admin)]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken)
        {

            var result = await _userService.GetAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }



        [HttpPut("{id}/toggle-status")]
        [Authorize(Roles = DefaultRoles.Admin)]
        public async Task<IActionResult> Toggle(string id, CancellationToken cancellationToken)
        {

            var result = await _userService.ToggleStatusAsync(id, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPost("add-role")]
        [Authorize(Roles = DefaultRoles.Admin)]
        public async Task<IActionResult> AddRoleToUser([FromBody] AddRoleToUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.AddRoleToUserAsync(request.UserId, request.RoleName, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPost("remove-role")]
        [Authorize(Roles = DefaultRoles.Admin)]
        public async Task<IActionResult> RemovedRoleToUser([FromBody] AddRoleToUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.RemoveRoleToUserAsync(request.UserId, request.RoleName, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
