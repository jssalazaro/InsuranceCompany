using FluentAssertions.Common;
using InsuranceCompany.WebApi.Configuration;
using InsuranceCompany.WebApi.DTOs.UserDTO;
using InsuranceCompnay.Abstractions;
using InsuranceCompnay.Services.JwtConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InsuranceCompany.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHandlerService _tokenHandlerService;

        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService tokenHandlerService)
        {
            _userManager = userManager;
            _tokenHandlerService = tokenHandlerService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDTO user)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _userManager.FindByEmailAsync(user.Email);
                if(userExist != null)
                {
                    return BadRequest("The email already exists.");
                }

                var isCreated = await _userManager.CreateAsync(new IdentityUser() { Email = user.Email, UserName = user.Email }, user.Password);

                if (isCreated.Succeeded)
                {
                    return Ok();
                }else
                {
                    return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
                }
            }
            return BadRequest("User creation errors");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginRequestDTO user)
        {
            if (ModelState.IsValid)
            {
                var UserExists = await _userManager.FindByEmailAsync(user.Email);
                if (UserExists == null)
                {
                    return BadRequest(new UserLoginResponseDTO()
                    {
                        Login = false,
                        Errors = new List<String>()
                        {
                            "Incorrect user or password!"
                        }
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(UserExists, user.Password);

                if (isCorrect)
                {
                    var pars = new TokenParameters()
                    {
                        Id = UserExists.Id,
                        PasswordHash = UserExists.PasswordHash,
                        UserName = UserExists.UserName

                    };

                    var jwtToken = _tokenHandlerService.GenerateJwtToken(pars);

                    return Ok(new UserLoginResponseDTO()
                    {
                        Login = true,
                        Token = jwtToken
                    });

                }
                else
                {
                    return BadRequest(new UserLoginResponseDTO()
                    {
                        Login = false,
                        Errors = new List<String>()
                        {
                            "Incorrect user or password!"
                        }
                    });
                }

            }
            else
            {
                return BadRequest(new UserLoginResponseDTO()
                {
                    Login = false,
                    Errors = new List<String>()
                        {
                            "Incorrect user or password!"
                        }
                });
            }
        }
    }
}
