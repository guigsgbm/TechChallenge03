using App.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NewsAPI.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok();
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
        }

        return BadRequest(ModelState);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("users")]
    public async Task<IActionResult> Get()
    {
        var users = _userManager.Users.ToList();

        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (User.IsInRole("admin"))
            {
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                    return Ok();
                }
            }

            else
            {
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    return Ok();
                }
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return BadRequest(ModelState);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [Authorize]
    [HttpDelete("delete/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"User with ID {userId} not found.");
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return NoContent();
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return BadRequest(ModelState);
    }


}
