using Api.Jwt;
using Application.DTOs;
using Application.DTOs.Auth;
using Application.Helpers;
using Application.Services.Users;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Api.Controllers
{
    [Route(ApiConstants.EndPoint + ApiConstants.AuthRoute)]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IJwtUtils _jwtUtils;

        public AuthController(IUserService userService, IConfiguration configuration, IJwtUtils jwtUtils)
        {
            _userService = userService;
            _configuration = configuration;
            _jwtUtils = jwtUtils;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(ApiConstants.LoginRoute)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse<LoginResponse>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<JsonResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.GetByEmailAsync(request.Email);
            if (user != null && PasswordHelper.VerifyPassword(request.Password, user.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = _jwtUtils.GenerateJwtToken(authClaims);
                var refreshToken = _jwtUtils.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                await _userService.UpdateAsync(user);

                return this.response(new LoginResponse()
                {
                    Email = user.Email,
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = token,
                    RefreshToken = refreshToken,
                }, StatusCodes.Status200OK, "Logged in successfully");
            }
            return this.response("", StatusCodes.Status401Unauthorized, "Wrong email or password");
        }

        [HttpPost]
        [Route(ApiConstants.RefreshTokenRoute)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseResponse<Token>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<JsonResult> RefreshToken(Token request)
        {
            if (request is null)
            {
                return this.response("", StatusCodes.Status400BadRequest, "Token malformed");
            }

            string? accessToken = request.AccessToken;
            string? refreshToken = request.RefreshToken;

            var principal = _jwtUtils.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return this.response("", StatusCodes.Status400BadRequest, "Token malformed");
            }

            string email = principal.Identity.Name;

            var user = await _userService.GetByEmailAsync(email);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return this.response("", StatusCodes.Status400BadRequest, "Token malformed");
            }

            var newAccessToken = _jwtUtils.GenerateJwtToken(principal.Claims.ToList());
            var newRefreshToken = _jwtUtils.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userService.UpdateAsync(user);
            return this.response(new Token
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            }, StatusCodes.Status200OK, "Successfully refreshed token");
        }

        [HttpPost, Authorize]
        [Route(ApiConstants.RevokeTokenRoute)]
        public async Task<JsonResult> RevokeAsync()
        {
            var email = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(email);
            if (user == null) return this.response("", StatusCodes.Status400BadRequest);
            user.RefreshToken = null;
            await _userService.UpdateAsync(user);
            return this.response("", StatusCodes.Status200OK, "Successfully revoked refresh token");
        }
    }
}