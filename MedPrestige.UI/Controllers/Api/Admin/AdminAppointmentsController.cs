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

        public AdminAppointmentsController(BusinessLogic bl)
        {
            _bl = bl;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_bl.Appointments.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var appointment = _bl.Appointments.GetById(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpGet("doctor/{doctorId}")]
        public IActionResult GetByDoctor(int doctorId)
        {
            return Ok(_bl.Appointments.GetByDoctorId(doctorId));
        }

        [HttpGet("patient/{patientId}")]
        public IActionResult GetByPatient(int patientId)
        {
            return Ok(_bl.Appointments.GetByPatientId(patientId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] AppointmentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _bl.Appointments.Add(dto);
            return Ok(new { message = "Programare adăugată." });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AppointmentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            dto.AppointmentId = id;
            _bl.Appointments.Update(dto);
            return Ok(new { message = "Programare actualizată." });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bl.Appointments.Delete(id);
            return Ok(new { message = "Programare ștearsă." });
        }
    }
}
