using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Finan.Infra.Data.Context;
using Finan.Service.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _baseUserService;

        public UserController(IUserService baseUserService, ISeedDataRepository seedDataRepository)
        {
            _baseUserService = baseUserService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCommand userCommand) => await ExecuteAsync(async () => await _baseUserService.CreateUser(userCommand));

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UserCommand UserParameter)
        {
            return await ExecuteAsync(async () => await _baseUserService.UpdateAsync<UserValidator>(
                new User { Id = UserParameter.Id,
                    UserName = UserParameter.UserName,
                    Password  = UserParameter.Password,
                    Email = UserParameter.Email }));
        }

        [HttpDelete("{id}")]
        [Authorize]
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
        [Authorize]
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
                    Email = x.Email
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
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
                    Email = result.UserName
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

