using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : BaseController
    {
        //login and register
        [HttpGet("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userdto = await _serviceManager.authunticationService.LoginAsync(loginDto);
            return Ok(userdto);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userdto = await _serviceManager.authunticationService.RegisterAsync(registerDto);
            return Ok(userdto);
        }

        // Check Email, GetCurrentUser, GetCurrentUserAddress, UpdateCurrentUserAddress 
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _serviceManager.authunticationService.CheckEmailAsync(email);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("currentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userdto = await _serviceManager.authunticationService.GetCurrentUser(GetEmailFromToken());
            return Ok(userdto);
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var addressupdated = await _serviceManager.authunticationService.GetCurrentUserAddress(GetEmailFromToken());
            return Ok(addressupdated);
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var addressupdated = await _serviceManager.authunticationService.UpdateCurrentUserAddress( addressDto, GetEmailFromToken()!);
            return Ok(addressupdated);
        }
    }
}
