using System.Security.Claims;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces.Service.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<IActionResult> UpdateUserAddress(AddressDTO address)
        {
            var user = await _userManager.FindByUserClaimsPrinciplelWithAddressAsync(HttpContext.User);

            user.Address = _mapper.Map<AddressDTO, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(_mapper.Map<Address,AddressDTO>(user.Address));
            }

            return BadRequest("Can not update Useraddress");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return Ok(new UserDTO
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400)); //TODO: adjust response
            }

            return Ok(new UserDTO
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            });
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<IActionResult> GetUserAddress()
        {

            var user = await _userManager.FindByUserClaimsPrinciplelWithAddressAsync(HttpContext.User);

            return Ok(_mapper.Map<Address, AddressDTO>(user.Address));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {

            var user = await _userManager.FindByUserClaimsPrinciple(HttpContext.User);

            return Ok(new UserDTO
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            });
        }

    }
}