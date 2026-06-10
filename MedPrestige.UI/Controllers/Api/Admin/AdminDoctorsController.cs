using MedPrestige.BLL;
using MedPrestige.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedPrestige.UI.Controllers.Api.Admin
{
    [ApiController]
    [Route("api/admin/doctors")]
    public class AdminDoctorsController : ControllerBase
    {
        private readonly BusinessLogic _bl;
        private readonly ILogger<AdminDoctorsController> _logger;

        public AdminDoctorsController(BusinessLogic bl, ILogger<AdminDoctorsController> logger)
        {
            _bl = bl;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("GET /api/admin/doctors - fetching all doctors");
                var doctors = _bl.Doctors.GetAll();
                _logger.LogInformation("GET /api/admin/doctors - returned {Count} doctors", doctors.Count);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/admin/doctors - error fetching doctors");
                return StatusCode(500, new { message = "Failed to fetch doctors", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.LogInformation("GET /api/admin/doctors/{Id} - fetching doctor", id);
                var doctor = _bl.Doctors.GetById(id);
                if (doctor == null)
                {
                    _logger.LogWarning("GET /api/admin/doctors/{Id} - not found", id);
                    return NotFound();
                }
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/admin/doctors/{Id} - error fetching doctor", id);
                return StatusCode(500, new { message = "Failed to fetch doctor", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] DoctorDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _logger.LogInformation("POST /api/admin/doctors - creating doctor: {Email}", dto.Email);
                _bl.Doctors.Add(dto);
                _logger.LogInformation("POST /api/admin/doctors - created successfully");
                return Ok(new { message = "Doctor adăugat." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST /api/admin/doctors - error creating doctor");
                return StatusCode(500, new { message = "Failed to create doctor", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DoctorDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _logger.LogInformation("PUT /api/admin/doctors/{Id} - updating doctor", id);
                dto.DoctorId = id;
                _bl.Doctors.Update(dto);
                _logger.LogInformation("PUT /api/admin/doctors/{Id} - updated successfully", id);
                return Ok(new { message = "Doctor actualizat." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT /api/admin/doctors/{Id} - error updating doctor", id);
                return StatusCode(500, new { message = "Failed to update doctor", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("DELETE /api/admin/doctors/{Id} - deleting doctor", id);
                _bl.Doctors.Delete(id);
                _logger.LogInformation("DELETE /api/admin/doctors/{Id} - deleted successfully", id);
                return Ok(new { message = "Doctor șters." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /api/admin/doctors/{Id} - error deleting doctor", id);
                return StatusCode(500, new { message = "Failed to delete doctor", error = ex.Message });
            }
        }
    }
}
