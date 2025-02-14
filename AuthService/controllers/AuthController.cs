using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

   [HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterModel model)
{
    if (!ModelState.IsValid) return BadRequest(ModelState);

    // Создаем пользователя с логином
    var user = new IdentityUser
    {
        UserName = model.Username, // Теперь логин будет отдельным полем
        Email = model.Email
    };

    var result = await _userManager.CreateAsync(user, model.Password);

    if (result.Succeeded)
    {
        return Ok(new { message = "Регистрация успешна!" });
    }

    return BadRequest(result.Errors);
}

}

public class RegisterModel
{
    public string Username { get; set; } 
    public string Email { get; set; }
    public string Password { get; set; }
}
