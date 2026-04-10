using MedPrestige.BLL;
using MedPrestige.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedPrestige.UI.Controllers.Api
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : ControllerBase
    {
        private readonly BusinessLogic _bl;

        public ServicesController(BusinessLogic bl)
        {
            _bl = bl;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var services = _bl.Services.GetAll();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var service = _bl.Services.GetById(id);
            if (service == null) return NotFound();
            return Ok(service);
        }

        [HttpGet("{id}/doctors")]
        public IActionResult GetDoctors(int id)
        {
            var doctors = _bl.Doctors.GetByServiceId(id);
            return Ok(doctors);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ServiceDto dto)
        {
            _bl.Services.Add(dto);
            var latest = _bl.Services.GetAll().OrderByDescending(s => s.ServiceId).First();
            return CreatedAtAction(nameof(GetById), new { id = latest.ServiceId }, latest);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ServiceDto dto)
        {
            var existing = _bl.Services.GetById(id);
            if (existing == null) return NotFound();
            dto.ServiceId = id;
            _bl.Services.Update(dto);
            return Ok(_bl.Services.GetById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _bl.Services.GetById(id);
            if (existing == null) return NotFound();
            _bl.Services.Delete(id);
            return NoContent();
        }
    }
}
