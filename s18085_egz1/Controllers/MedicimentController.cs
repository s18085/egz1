using System;
using Microsoft.AspNetCore.Mvc;
using s18085_egz1.Model;
using s18085_egz1.Services;

namespace s18085_egz1.Controllers
{
    [ApiController]
    [Route("api/mediciment")]
    public class MedicimentController : ControllerBase
    {
        IDbService _dbService;
        public MedicimentController(IDbService dbService)
        {
            this._dbService = dbService;
        }

        [HttpGet("{id}")]
        public IActionResult GetMediciment(string id)
        {

            Mediciment mediciment = _dbService.FindMedicimentsWithPrescr(id);
            if (mediciment == null)
            {
                return NotFound("Mediciment not found");
            }
            return Ok(mediciment);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient (string id)
        {
            var deleted = _dbService.DeletePatient(id);
            if (!deleted) return NotFound("Patiante was now found in db");
            return Ok();
        }
    }
}
