using MedPrestige.BLL;
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
    }
}
