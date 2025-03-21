using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Domain.Commands;
using Finan.Service.Services;
using Finan.Service.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Finan.Domain.Parameters;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class UserController : ControllerBase
    {
        private IUserService _baseUserService;

        public UserController(IUserService baseUserService)
        {
            _baseUserService = baseUserService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCommand UserParameter)
        {
            return await ExecuteAsync(async () => await _baseUserService.Add<UserValidator>(
                new User { UserName = UserParameter.UserName,
                    Password = UserParameter.Password,
                    Email = UserParameter.Email,
                    Role = UserParameter.Role }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserCommand UserParameter)
        {
            return await ExecuteAsync(async () => await _baseUserService.UpdateAsync<UserValidator>(
                new User { Id = UserParameter.Id,
                    UserName = UserParameter.UserName,
                    Password  = UserParameter.Password,
                    Email = UserParameter.Email,
                    Role = UserParameter.Role }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseUserService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() 
        {
            try
            {
                var result = await _baseUserService.GetAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result.Select(x => new UserDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Role = x.Role
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        } 


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id) 
        {
            try
            {
                var result = await _baseUserService.GetByIdAsync(id);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(new UserDTO
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    Email = result.UserName,
                    Role = result.Role
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        } 


        private async Task<IActionResult> ExecuteAsync(Func<Task<object>> func)
        {
            try
            {
                var result = await func();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

