using MedPrestige.BLL;
using MedPrestige.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedPrestige.UI.Controllers.Api.Admin
{
    [ApiController]
    [Route("api/admin/appointments")]
    public class AdminAppointmentsController : ControllerBase
    {
        private readonly BusinessLogic _bl;
        private readonly ILogger<AdminAppointmentsController> _logger;

        public AdminAppointmentsController(BusinessLogic bl, ILogger<AdminAppointmentsController> logger)
        {
            _bl = bl;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("GET /api/admin/appointments - fetching all appointments");
                var appointments = _bl.Appointments.GetAll();
                _logger.LogInformation("GET /api/admin/appointments - returned {Count} appointments", appointments.Count);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/admin/appointments - error fetching appointments");
                return StatusCode(500, new { message = "Failed to fetch appointments", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.LogInformation("GET /api/admin/appointments/{Id} - fetching appointment", id);
                var appointment = _bl.Appointments.GetById(id);
                if (appointment == null)
                {
                    _logger.LogWarning("GET /api/admin/appointments/{Id} - not found", id);
                    return NotFound();
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/admin/appointments/{Id} - error fetching appointment", id);
                return StatusCode(500, new { message = "Failed to fetch appointment", error = ex.Message });
            }
        }

        [HttpGet("doctor/{doctorId}")]
        public IActionResult GetByDoctor(int doctorId)
        {
            try
            {
                _logger.LogInformation("GET /api/admin/appointments/doctor/{DoctorId}", doctorId);
                var appointments = _bl.Appointments.GetByDoctorId(doctorId);
                _logger.LogInformation("GET /api/admin/appointments/doctor/{DoctorId} - returned {Count}", doctorId, appointments.Count);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/admin/appointments/doctor/{DoctorId} - error", doctorId);
                return StatusCode(500, new { message = "Failed to fetch appointments", error = ex.Message });
            }
        }

        [HttpGet("patient/{patientId}")]
        public IActionResult GetByPatient(int patientId)
        {
            try
            {
                _logger.LogInformation("GET /api/admin/appointments/patient/{PatientId}", patientId);
                var appointments = _bl.Appointments.GetByPatientId(patientId);
                _logger.LogInformation("GET /api/admin/appointments/patient/{PatientId} - returned {Count}", patientId, appointments.Count);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/admin/appointments/patient/{PatientId} - error", patientId);
                return StatusCode(500, new { message = "Failed to fetch appointments", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] AppointmentDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _logger.LogInformation("POST /api/admin/appointments - creating appointment");
                _bl.Appointments.Add(dto);
                _logger.LogInformation("POST /api/admin/appointments - created successfully");
                return Ok(new { message = "Programare adăugată." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST /api/admin/appointments - error creating appointment");
                return StatusCode(500, new { message = "Failed to create appointment", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AppointmentDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _logger.LogInformation("PUT /api/admin/appointments/{Id} - updating", id);
                dto.AppointmentId = id;
                _bl.Appointments.Update(dto);
                _logger.LogInformation("PUT /api/admin/appointments/{Id} - updated successfully", id);
                return Ok(new { message = "Programare actualizată." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT /api/admin/appointments/{Id} - error updating", id);
                return StatusCode(500, new { message = "Failed to update appointment", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("DELETE /api/admin/appointments/{Id}", id);
                _bl.Appointments.Delete(id);
                _logger.LogInformation("DELETE /api/admin/appointments/{Id} - deleted successfully", id);
                return Ok(new { message = "Programare ștearsă." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /api/admin/appointments/{Id} - error", id);
                return StatusCode(500, new { message = "Failed to delete appointment", error = ex.Message });
            }
        }
    }
}
