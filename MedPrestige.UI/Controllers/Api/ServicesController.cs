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
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(BusinessLogic bl, ILogger<ServicesController> logger)
        {
            _bl = bl;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("GET /api/services - fetching all services");
                var services = _bl.Services.GetAll();
                _logger.LogInformation("GET /api/services - returned {Count} services", services.Count);
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/services - error fetching services");
                return StatusCode(500, new { message = "Failed to fetch services", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.LogInformation("GET /api/services/{Id} - fetching service", id);
                var service = _bl.Services.GetById(id);
                if (service == null)
                {
                    _logger.LogWarning("GET /api/services/{Id} - not found", id);
                    return NotFound();
                }
                _logger.LogInformation("GET /api/services/{Id} - found service: {Name}", id, service.Name);
                return Ok(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/services/{Id} - error fetching service", id);
                return StatusCode(500, new { message = "Failed to fetch service", error = ex.Message });
            }
        }

        [HttpGet("{id}/doctors")]
        public IActionResult GetDoctors(int id)
        {
            try
            {
                _logger.LogInformation("GET /api/services/{Id}/doctors - fetching doctors for service", id);
                var doctors = _bl.Doctors.GetByServiceId(id);
                _logger.LogInformation("GET /api/services/{Id}/doctors - returned {Count} doctors", id, doctors.Count);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/services/{Id}/doctors - error fetching doctors for service", id);
                return StatusCode(500, new { message = "Failed to fetch doctors for service", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] ServiceDto dto)
        {
            try
            {
                _logger.LogInformation("POST /api/services - creating service: {Name}", dto.Name);
                _bl.Services.Add(dto);
                var latest = _bl.Services.GetAll().OrderByDescending(s => s.ServiceId).First();
                _logger.LogInformation("POST /api/services - created service with id: {ServiceId}", latest.ServiceId);
                return CreatedAtAction(nameof(GetById), new { id = latest.ServiceId }, latest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST /api/services - error creating service");
                return StatusCode(500, new { message = "Failed to create service", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ServiceDto dto)
        {
            try
            {
                _logger.LogInformation("PUT /api/services/{Id} - updating service", id);
                var existing = _bl.Services.GetById(id);
                if (existing == null)
                {
                    _logger.LogWarning("PUT /api/services/{Id} - not found", id);
                    return NotFound();
                }
                dto.ServiceId = id;
                _bl.Services.Update(dto);
                _logger.LogInformation("PUT /api/services/{Id} - updated successfully", id);
                return Ok(_bl.Services.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT /api/services/{Id} - error updating service", id);
                return StatusCode(500, new { message = "Failed to update service", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("DELETE /api/services/{Id} - deleting service", id);
                var existing = _bl.Services.GetById(id);
                if (existing == null)
                {
                    _logger.LogWarning("DELETE /api/services/{Id} - not found", id);
                    return NotFound();
                }
                _bl.Services.Delete(id);
                _logger.LogInformation("DELETE /api/services/{Id} - deleted successfully", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /api/services/{Id} - error deleting service", id);
                return StatusCode(500, new { message = "Failed to delete service", error = ex.Message });
            }
        }
    }
}
