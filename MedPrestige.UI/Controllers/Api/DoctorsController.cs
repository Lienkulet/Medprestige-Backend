using MedPrestige.BLL;
using MedPrestige.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedPrestige.UI.Controllers.Api
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly BusinessLogic _bl;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(BusinessLogic bl, ILogger<DoctorsController> logger)
        {
            _bl = bl;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("GET /api/doctors - fetching all doctors");
                var doctors = _bl.Doctors.GetAll();
                _logger.LogInformation("GET /api/doctors - returned {Count} doctors", doctors.Count);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/doctors - error fetching doctors");
                return StatusCode(500, new { message = "Failed to fetch doctors", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.LogInformation("GET /api/doctors/{Id} - fetching doctor", id);
                var doctor = _bl.Doctors.GetById(id);
                if (doctor == null)
                {
                    _logger.LogWarning("GET /api/doctors/{Id} - not found", id);
                    return NotFound();
                }
                _logger.LogInformation("GET /api/doctors/{Id} - found doctor: {Name}", id, doctor.Name);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/doctors/{Id} - error fetching doctor", id);
                return StatusCode(500, new { message = "Failed to fetch doctor", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] DoctorDto dto)
        {
            try
            {
                _logger.LogInformation("POST /api/doctors - creating doctor with email: {Email}", dto.Email);
                _bl.Users.Add(new MedPrestige.Models.DTOs.UserDto
                {
                    Email = dto.Email,
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Status = "active"
                }, "Welcome1!");

                var createdUser = _bl.Users.GetByEmail(dto.Email);
                dto.UserId = createdUser.UserId;
                _bl.Doctors.Add(dto);

                var latest = _bl.Doctors.GetAll().OrderByDescending(d => d.DoctorId).First();
                _logger.LogInformation("POST /api/doctors - created doctor with id: {DoctorId}", latest.DoctorId);
                return CreatedAtAction(nameof(GetById), new { id = latest.DoctorId }, latest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST /api/doctors - error creating doctor");
                return StatusCode(500, new { message = "Failed to create doctor", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DoctorDto dto)
        {
            try
            {
                _logger.LogInformation("PUT /api/doctors/{Id} - updating doctor", id);
                var existing = _bl.Doctors.GetById(id);
                if (existing == null)
                {
                    _logger.LogWarning("PUT /api/doctors/{Id} - not found", id);
                    return NotFound();
                }

                dto.DoctorId = id;
                _bl.Doctors.Update(dto);

                var userDto = new MedPrestige.Models.DTOs.UserDto
                {
                    UserId = existing.UserId ?? 0,
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Status = dto.Status
                };
                if (userDto.UserId > 0)
                    _bl.Users.Update(userDto);

                _logger.LogInformation("PUT /api/doctors/{Id} - updated successfully", id);
                return Ok(_bl.Doctors.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT /api/doctors/{Id} - error updating doctor", id);
                return StatusCode(500, new { message = "Failed to update doctor", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("DELETE /api/doctors/{Id} - deleting doctor", id);
                var doctor = _bl.Doctors.GetById(id);
                if (doctor == null)
                {
                    _logger.LogWarning("DELETE /api/doctors/{Id} - not found", id);
                    return NotFound();
                }

                var userId = doctor.UserId;
                _bl.Doctors.Delete(id);
                if (userId.HasValue)
                    _bl.Users.Delete(userId.Value);

                _logger.LogInformation("DELETE /api/doctors/{Id} - deleted successfully", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /api/doctors/{Id} - error deleting doctor", id);
                return StatusCode(500, new { message = "Failed to delete doctor", error = ex.Message });
            }
        }
    }
}
