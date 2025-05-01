using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Abstactions;
using RealState.Abstactions.Consts;
using RealState.Api.Contracts;
using RealState.Contracts.Property;
using RealState.Services;

namespace RealState.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PropertyController(IPropertyService propertyService) : ControllerBase
    {
        private readonly IPropertyService _propertyService = propertyService;
      

        [HttpPost("AddSell")]
        //[Authorize(Roles = DefaultRoles.Admin)]
        public async Task<IActionResult> AddPropertyForSell([FromForm] PropertyForSellRequest request, [FromForm] UploadImageRequest uploadImageRequest)
        {
            var result = await _propertyService.AddPropertyForSell(request, uploadImageRequest);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("AddRent")]
        //[Authorize(Roles = DefaultRoles.Admin)]

        public async Task<IActionResult> AddPropertyForRent([FromForm] PropertyForRentRequest request, [FromForm] UploadImageRequest uploadImageRequest)
        {
            var result = await _propertyService.AddPropertyForRent(request, uploadImageRequest);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpDelete("DeleteSell/{id}")]
        //[Authorize(Roles = DefaultRoles.Admin)]

        public async Task<IActionResult> DeletePropertyForSell(int id)
        {
            var result = await _propertyService.DeletePropertyForSell(id);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }
        //[Authorize(Roles = DefaultRoles.Admin)]

        [HttpDelete("DeleteRent/{id}")]
        public async Task<IActionResult> DeletePropertyForRent(int id)
        {
            var result = await _propertyService.DeletePropertyForRent(id);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPut("EditSell/{id}")]
        //[Authorize(Roles = DefaultRoles.Admin)]

        public async Task<IActionResult> EditPropertyForSell(int id, [FromForm] PropertyForSellRequest request, [FromForm] UploadImageRequest uploadImageRequest)
        {
            var result = await _propertyService.EditPropertyForSell(id, request, uploadImageRequest);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPut("EditRent/{id}")]
        //[Authorize(Roles = DefaultRoles.Admin)]

        public async Task<IActionResult> EditPropertyForRent(int id, [FromForm] PropertyForRentRequest request, [FromForm] UploadImageRequest uploadImageRequest)
        {
            var result = await _propertyService.EditPropertyForRent(id, request, uploadImageRequest);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("GetSell/{id}")]
        public async Task<IActionResult> GetPropertyForSell(int id)
        {
            var result = await _propertyService.GetPropertyForSell(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("GetRent/{id}")]
        public async Task<IActionResult> GetPropertyForRent(int id)
        {
            var result = await _propertyService.GetPropertyForRent(id);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("GetAllSell")]
        public async Task<IActionResult> GetAllPropertiesForSell()
        {
            var result = await _propertyService.GetAllPropertiesForSell();
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("GetAllRent")]
        public async Task<IActionResult> GetAllPropertiesForRent()
        {
            var result = await _propertyService.GetAllPropertiesForRent();
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }  
        [HttpPost("Search")]
        public async Task<IActionResult> Search(PropertySearchRequest searchRequest)
        {
            var result = await _propertyService.SearchProperties(searchRequest);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
