using DigitalLibrary.Data;
using DigitalLibrary.DTOs.AdminDTOs;
using DigitalLibrary.Interfaces;
using DigitalLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalLibrary.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        /// <summary>
        /// Get a JWT token if the name and secret are correct
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Token([FromQuery] string adminId, [FromQuery] string secret)
        {
            string? token = _authRepository.Authenticate(adminId, secret);

            ArgumentNullException.ThrowIfNull(token);

            return Ok(await Task.FromResult(new AuthDTO(token)));
        }
    }
}
