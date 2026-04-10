using MedPrestige.BLL;
using Microsoft.AspNetCore.Mvc;

namespace MedPrestige.UI.Controllers.Api
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly BusinessLogic _bl;

        public DoctorsController(BusinessLogic bl)
        {
            _bl = bl;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _bl.Doctors.GetAll();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var doctor = _bl.Doctors.GetById(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        public IActionResult Create([FromBody] DoctorDto dto)
        {
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
            return CreatedAtAction(nameof(GetById), new { id = latest.DoctorId }, latest);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DoctorDto dto)
        {
            var existing = _bl.Doctors.GetById(id);
            if (existing == null) return NotFound();

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

            return Ok(_bl.Doctors.GetById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var doctor = _bl.Doctors.GetById(id);
            if (doctor == null) return NotFound();

            var userId = doctor.UserId;
            _bl.Doctors.Delete(id);
            if (userId.HasValue)
                _bl.Users.Delete(userId.Value);

            return NoContent();
        }
    }
}
