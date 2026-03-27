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
    }
}
