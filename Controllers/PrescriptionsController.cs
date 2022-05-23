using cw_8_22c.Models.DTOs;
using cw_8_22c.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cw_8_22c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrescription(int id)
        {
            var prescription = _prescriptionService.GetPrescriptionId(id).Result;

            if (prescription == null) return NotFound("prescription does not exist.");

            var doctor = _prescriptionService.GetDoctorFromPrescription(prescription).Result;
            var patient = _prescriptionService.GetPatientFromPrescription(prescription).Result;
            var meds = _prescriptionService.GetMedicamentsFromPrescriptionId(id).Result;

            var returnDTO = new PrescriptionReturnDTO();
            returnDTO.patient = patient;
            returnDTO.doctor = doctor;
            returnDTO.meds = meds;
            await _prescriptionService.SaveDatabase();

            return Ok(returnDTO);
        }
    }
}
