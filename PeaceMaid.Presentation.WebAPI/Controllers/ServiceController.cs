using Microsoft.AspNetCore.Mvc;
using PeaceMaid.Application;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController(IService service) : ControllerBase
    {
        private readonly IService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            
            var data = await _service.GetAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServicec(int id)
        {
            var data = await _service.GetServiceAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostService([FromBody]Service sv)
        {
            var result = await _service.AddAsync(sv);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateServicec([FromBody]Service sv)
        {
            var result = await _service.UpdateAsync(sv);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
