﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Post([FromBody] ServiceProvider sProviderDTO)
        {
            if (sProviderDTO == null)
                return BadRequest("Data of type null cannot be accepted.");

            var result = await _sProvider.AddAsync(sProviderDTO);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvider(int id)
        {
            var data = await _sProvider.GetProviderAsync(id);
            return Ok(data);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ServiceProvider sProviderDTO)
        {
            if (sProviderDTO == null)
                return BadRequest("Data of type null cannot be accepted.");

            var result = await _sProvider.UpdateAsync(sProviderDTO);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delet(int id)
        {
            var result = await _sProvider.DeleteAsync(id);
            return Ok(result);
        }
    }
}