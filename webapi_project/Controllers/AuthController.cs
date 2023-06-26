using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using webapi_project.Data;
using webapi_project.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webapi_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly NoteDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(NoteDbContext context,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<IdentityResult>> Signup(SignUp userInfo)
        {
            if (userInfo == null || userInfo.username.IsNullOrEmpty() || userInfo.password.IsNullOrEmpty())
            {
                return NotFound();
            }

            // make an object for the new user to store in DB
            var newUser = new ApplicationUser();
            newUser.Email = userInfo.email;
            newUser.UserName = userInfo.username;

            // create user with the sent email, username, & password
            var newCreatedUser = await _userManager.CreateAsync(newUser, userInfo.password);

            return Ok(new
            {
                newCreatedUser.Succeeded,
                newCreatedUser.Errors
            });
        }

        [HttpPost("Signin")]
        public async Task<ActionResult<ApplicationUser>> Signin(SignIn userInfo)
        {
            if (userInfo == null || userInfo.username.IsNullOrEmpty() || userInfo.password.IsNullOrEmpty())
            {
                return Unauthorized();
            }

            var applicationUser = new ApplicationUser();
            applicationUser.UserName = userInfo.username;

            var user = await _userManager.FindByNameAsync(userInfo.username);
            if (user == null || await _userManager.CheckPasswordAsync(applicationUser, userInfo.password))
            {
                return Unauthorized(new { succeeded = false });
            }

            var userRoles = await _userManager.GetRolesAsync(applicationUser);
            var userId = await _userManager.GetUserIdAsync(applicationUser);

            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);


            return Ok(new
            {
                succeeded = true,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMonths(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
