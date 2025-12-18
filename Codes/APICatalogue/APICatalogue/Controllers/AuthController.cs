using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APICatalogue.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(ITokenService tokenService, IConfiguration config, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _tokenService = tokenService;
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Username!);

            if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var accessToken = _tokenService.GenerateAccessToken(authClaims, _config);

                var refreshToken = _tokenService.GenerateRefreshToken();

                _ = int.TryParse(_config["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

                user.RefreshToken = refreshToken;

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    RefreshToken = refreshToken,
                    Expiration = accessToken.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);

            if (userExists is not null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken ?? throw new ArgumentNullException(nameof(tokenModel));

            string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentNullException(nameof(tokenModel));

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _config);

            if (principal is null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            string? username = principal.Identity?.Name;

            var user = await _userManager.FindByNameAsync(username!);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _config);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                ãccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string? username)
        {
            var user = await _userManager.FindByNameAsync(username!);

            if (user is null)
            {
                return BadRequest("Invalid user name");
            }

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
