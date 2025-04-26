using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Abstactions;
using RealState.Abstactions.Consts;
using RealState.Api.Contracts;
using RealState.Contracts.Launch;
using RealState.Services;

namespace RealState.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LaunchController : ControllerBase
    {
        private readonly ILaunchService _launchService;

        public LaunchController(ILaunchService launchService)
        {
            _launchService = launchService;
        }

        [Authorize(Roles = DefaultRoles.Admin)]
        [HttpPost("Add")]
        public async Task<IActionResult> AddLaunch([FromForm] LaunchRequest request, [FromForm] LaunchUploadImageRequest uploadImageRequest)
        {
            var result = await _launchService.AddLaunchAsync(request, uploadImageRequest);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [Authorize(Roles = DefaultRoles.Admin)]
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditLaunch(int id, [FromForm] LaunchRequest request)
        {
            var result = await _launchService.EditLaunchAsync(id, request);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [Authorize(Roles = DefaultRoles.Admin)]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteLaunch(int id)
        {
            var result = await _launchService.RemoveLaunchAsync(id);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [Authorize(Roles = DefaultRoles.Admin)]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetLaunch(int id)
        {
            var result = await _launchService.GetLaunchByIdAsync(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [Authorize(Roles = DefaultRoles.Admin)]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllLaunches()
        {
            var result = await _launchService.GetAllLaunchesAsync();
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
