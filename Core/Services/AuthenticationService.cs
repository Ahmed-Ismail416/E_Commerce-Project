using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTOs.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,
                                        IConfiguration _config,
                                        IMapper _mapper) : IAuthunticationService
    {
        public async Task<bool> CheckEmailAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            return user is not null;
        }

        public async Task<AddressDto> GetCurrentUserAddress(string Email)
        {
            var user  =  await _userManager.Users.Include(u => u.Address)
                                             .FirstOrDefaultAsync(u => u.Email == Email) ?? throw new UserNotFoundException(Email);
           if(user.Address is null)
                throw new AddressNotFoundException(user.DisplayName);
            return _mapper.Map<Address, AddressDto>(user.Address!);
        }
        public async Task<AddressDto> UpdateCurrentUserAddress(AddressDto addressDto, string Email)
        {
            var user =  _userManager.Users.Include(u => u.Address)
                                             .FirstOrDefault(u => u.Email == Email) ?? throw new UserNotFoundException(Email);
            if (user.Address is null)
            {
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }else
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;    
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
            }
            await  _userManager.UpdateAsync(user);
            return _mapper.Map<Address, AddressDto>(user.Address!);
        }

        public async Task<UserDto> GetCurrentUser(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserDto() { DisplayName = user.DisplayName, Email = user.Email!, Token = await CreateTokenAync(user) };

        }
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            // step1: validate user
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                throw new UserNotFoundException(loginDto.Email);

            // step2: validate password
            var PasswordIsValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (PasswordIsValid)
                return new UserDto()
                {

                    Email = user.Email!,
                    Token = await CreateTokenAync(user),
                    DisplayName = user.DisplayName
                };
            throw new UnUthorizedException();
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Token =await CreateTokenAync(user),
                    Email = user.Email!
                };
            else
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        private async Task<string> CreateTokenAync(ApplicationUser user)
        {
            var Claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.NameIdentifier, user.Id)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SekretKey = _config.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SekretKey!));
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
            var Token = new JwtSecurityToken(
                issuer: _config.GetSection("JWTOptions")["Issuer"],
                audience: _config.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: Credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);


        }

    }
}
