using api.DTOs.Account;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController: ControllerBase
{
    
    private readonly UserManager<AppUser> _userManager;
    
    
    
    public AccountController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
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
                    return Ok("User created successfully");
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