using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using ServiceProvider = PeaceMaid.Domain.Entities.ServiceProvider;


namespace PeaceMaid.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController(ISProvider sProvider) : ControllerBase
    {
        private readonly ISProvider _sProvider = sProvider;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _sProvider.GetAsync();
            return  Ok(data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] ServiceProviderDTO serviceProviderDTO)
        {
            if (serviceProviderDTO == null)
                return BadRequest("Service provider data cannot be null.");

            if (serviceProviderDTO.ProfilePicture == null)
                return BadRequest("Profile picture is required!");

            using var memoryStream = new MemoryStream();
            await serviceProviderDTO.ProfilePicture.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();

            var result = await _sProvider.AddAsync(serviceProviderDTO, fileBytes);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvider(int id)
        {
            var data = await _sProvider.GetProviderAsync(id);
            return Ok(data);
        }

        [HttpGet("services/{spId}")]
        public async Task<IActionResult> GetServices(int spId)
        {
            var data = await _sProvider.GetServicesAsync(spId);
            return Ok(data);
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ServiceProvider sProviderDTO)
        {
            if (sProviderDTO == null)
                return BadRequest("Data of type null cannot be accepted.");

            var result = await _sProvider.UpdateAsync(sProviderDTO);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delet(int id)
        {
            var result = await _sProvider.DeleteAsync(id);
            return Ok(result);
        }
    }
}
