using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Injecting the UserManager
        private readonly UserManager<AppUser> _userManager;
        
        // Injecting the TokenService
        private readonly ITokenService _tokenService;

        // Constructor
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                
                var createdUser = await _userManager.CreateAsync(user, model.Password);
                
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    
                    if (roleResult.Succeeded)
                    {
                        var newUser = new
                        {
                            user.UserName,
                            user.Email,
                            Token = _tokenService.CreateToken(user)
                        };
                        
                        return Ok(newUser);
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }
                
                return BadRequest(createdUser.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
