using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthunticationService
    {
        //Login , Register
        public Task<UserDto> LoginAsync(LoginDto loginDto);
        public Task<UserDto> RegisterAsync(RegisterDto registerDto);

        public Task<bool> CheckEmailAsync(string Email);
        public Task<AddressDto> GetCurrentUserAddress(string Email);
        public Task<AddressDto> UpdateCurrentUserAddress(AddressDto addressDto, string Email);
        public Task<UserDto> GetCurrentUser(string Email);

    }
}
