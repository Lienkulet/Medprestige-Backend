using MedPrestige.BLL;
using MedPrestige.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedPrestige.UI.Controllers.Api
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly BusinessLogic _bl;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(BusinessLogic bl, IConfiguration config, ILogger<AuthController> logger)
        {
            _bl = bl;
            _config = config;
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            try
            {
                _logger.LogInformation("POST /api/auth/register - registering: {Email}", dto.Email);
                var existing = _bl.Users.GetByEmail(dto.Email);
                if (existing != null)
                    return Conflict(new { message = "Email already in use." });

                _bl.Users.Add(new UserDto
                {
                    Email = dto.Email,
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Status = "active"
                }, dto.Password);

                var user = _bl.Users.GetByEmail(dto.Email);
                _bl.Patients.Add(new PatientDto { UserId = user.UserId });
                var patient = _bl.Patients.GetByUserId(user.UserId);

                var token = GenerateToken(user, patient?.PatientId);
                _logger.LogInformation("POST /api/auth/register - registered userId: {UserId}", user.UserId);
                return Ok(new { token, user = new { user.UserId, user.Name, user.Email, patientId = patient?.PatientId, role = "patient" } });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST /api/auth/register - error registering {Email}", dto.Email);
                return StatusCode(500, new { message = "Registration failed", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            try
            {
                _logger.LogInformation("POST /api/auth/login - login attempt: {Email}", dto.Email);
                var user = _bl.Users.Login(dto.Email, dto.Password);
                if (user == null)
                {
                    _logger.LogWarning("POST /api/auth/login - invalid credentials for {Email}", dto.Email);
                    return Unauthorized(new { message = "Invalid email or password." });
                }

                var patient = _bl.Patients.GetByUserId(user.UserId);
                var doctor = _bl.Doctors.GetAll().FirstOrDefault(d => d.UserId == user.UserId);

                var role = doctor != null ? "doctor" : patient != null ? "patient" : "admin";
                var patientId = patient?.PatientId;
                var doctorId = doctor?.DoctorId;

                var token = GenerateToken(user, patientId, doctorId, role);
                _logger.LogInformation("POST /api/auth/login - success userId: {UserId}, role: {Role}", user.UserId, role);
                return Ok(new { token, user = new { user.UserId, user.Name, user.Email, patientId, doctorId, role } });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST /api/auth/login - error for {Email}", dto.Email);
                return StatusCode(500, new { message = "Login failed", error = ex.Message });
            }
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            var header = Request.Headers["Authorization"].FirstOrDefault();
            if (header == null || !header.StartsWith("Bearer "))
            {
                _logger.LogWarning("GET /api/auth/me - missing or invalid Authorization header");
                return Unauthorized(new { message = "No token provided." });
            }

            try
            {
                _logger.LogInformation("GET /api/auth/me - validating token");
                var token = header.Substring(7);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["Jwt:Audience"],
                    ValidateLifetime = true
                }, out var validated);

                var jwt = (JwtSecurityToken)validated;
                var userId = int.Parse(jwt.Claims.First(c => c.Type == "userId").Value);
                _logger.LogInformation("GET /api/auth/me - token valid for userId: {UserId}", userId);
                return Ok(new
                {
                    UserId = userId,
                    Name = jwt.Claims.First(c => c.Type == "name").Value,
                    Email = jwt.Claims.First(c => c.Type == "email").Value,
                    Role = jwt.Claims.First(c => c.Type == "role").Value,
                    PatientId = jwt.Claims.FirstOrDefault(c => c.Type == "patientId")?.Value,
                    DoctorId = jwt.Claims.FirstOrDefault(c => c.Type == "doctorId")?.Value,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/auth/me - token validation failed: {Error}", ex.Message);
                return Unauthorized(new { message = "Invalid or expired token.", error = ex.Message });
            }
        }

        private string GenerateToken(UserDto user, int? patientId = null, int? doctorId = null, string role = "patient")
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var claims = new List<Claim>
            {
                new("userId", user.UserId.ToString()),
                new("name", user.Name ?? ""),
                new("email", user.Email ?? ""),
                new("role", role),
            };
            if (patientId.HasValue) claims.Add(new("patientId", patientId.Value.ToString()));
            if (doctorId.HasValue) claims.Add(new("doctorId", doctorId.Value.ToString()));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:ExpirationDays"]!)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public record RegisterDto(string Name, string Email, string Phone, string Password);
    public record LoginDto(string Email, string Password);
}
