﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUser user) : ControllerBase
    {
        private readonly IUser _user = user;

        /// <summary>
        /// Get All users
        /// </summary>
        /// <returns>A List of users</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var data = await _user.GetAllAsync();
            return Ok(data);
        }

        /// <summary>
        /// Get a specific user from the Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServiceResponse</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _user.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Adds a user to the Database
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>ServiceResponse</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody] User userDTO)
        {
            if (userDTO == null)
                return BadRequest("Data of type null cannot be accepted.");

            var result = await _user.AddAsync(userDTO);
            return Ok(result);
        }

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>ServiceResponse</returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User userDTO)
        {
            if (userDTO == null)
                return BadRequest("Data of type null cannot be accepted.");

            var result = await _user.UpdateAsync(userDTO);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a User from the Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServiceResponse</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _user.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserDTO userDTO)
        {
            if (userDTO == null || string.IsNullOrWhiteSpace(userDTO.Email) || string.IsNullOrWhiteSpace(userDTO.Password))
                return BadRequest("Username and Password are required.");

            var result = await _user.LoginAsync(userDTO);

            if (result.Message.Contains("Not Logged in"))
                return Unauthorized("Invalid credentials");

            var data  = result.Data;
            var token = data.GetType().GetProperty("Token")?.GetValue(data, null).ToString();

            if (token != null)
            {
                // Handle Cookies
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                };

                Response.Cookies.Append("authToken", token, cookieOptions);
            }


            return Ok(data);
        }
    }
}
