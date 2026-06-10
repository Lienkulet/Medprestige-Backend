using MedPrestige.BLL;
using MedPrestige.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MedPrestige.UI.Controllers.Api.Admin
{
    [ApiController]
    [Route("api/admin/services")]
    public class AdminServicesController : ControllerBase
    {
        private readonly BusinessLogic _bl;
        private readonly ILogger<AdminServicesController> _logger;

        public AdminServicesController(BusinessLogic bl, ILogger<AdminServicesController> logger)
        {
            _bl = bl;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("GET /api/admin/services - fetching all services");
                var services = _bl.Services.GetAll();
                _logger.LogInformation("GET /api/admin/services - returned {Count} services", services.Count);
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/admin/services - error fetching services");
                return StatusCode(500, new { message = "Failed to fetch services", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.LogInformation("GET /api/admin/services/{Id} - fetching service", id);
                var service = _bl.Services.GetById(id);
                if (service == null)
                {
                    _logger.LogWarning("GET /api/admin/services/{Id} - not found", id);
                    return NotFound();
                }
                return Ok(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/admin/services/{Id} - error fetching service", id);
                return StatusCode(500, new { message = "Failed to fetch service", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] ServiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _logger.LogInformation("POST /api/admin/services - creating service: {Name}", dto.Name);
                _bl.Services.Add(dto);
                _logger.LogInformation("POST /api/admin/services - created successfully");
                return Ok(new { message = "Serviciu adăugat." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST /api/admin/services - error creating service");
                return StatusCode(500, new { message = "Failed to create service", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ServiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _logger.LogInformation("PUT /api/admin/services/{Id} - updating service", id);
                dto.ServiceId = id;
                _bl.Services.Update(dto);
                _logger.LogInformation("PUT /api/admin/services/{Id} - updated successfully", id);
                return Ok(new { message = "Serviciu actualizat." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT /api/admin/services/{Id} - error updating service", id);
                return StatusCode(500, new { message = "Failed to update service", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation("DELETE /api/admin/services/{Id} - deleting service", id);
                _bl.Services.Delete(id);
                _logger.LogInformation("DELETE /api/admin/services/{Id} - deleted successfully", id);
                return Ok(new { message = "Serviciu șters." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE /api/admin/services/{Id} - error deleting service", id);
                return StatusCode(500, new { message = "Failed to delete service", error = ex.Message });
            }
        }
    }
}
