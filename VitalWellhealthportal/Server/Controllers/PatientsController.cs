using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VitalWellhealthportal.Server.Data;
using VitalWellhealthportal.Server.IRepository;
using VitalWellhealthportal.Shared.Domain;

namespace VitalWellhealthportal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            //if (_context.Patients == null)
            //{
            //return NotFound();
            //}
            //return await _context.Patients.ToListAsync();
            var patients = await _unitOfWork.Patients.GetAll();
            return Ok(patients);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            //if (_context.Patients == null)
            //{
            // return NotFound();
            //}
            var patient = await _unitOfWork.Patients.Get(q => q.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            //_context.Entry(patient).State = EntityState.Modified;
            _unitOfWork.Patients.Update(patient);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!PatientExists(id))
                if (!await PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            //if (_context.Patients == null)
            //{
            //return Problem("Entity set 'ApplicationDbContext.Patients'  is null.");
            //}
            await _unitOfWork.Patients.Insert(patient);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            //if (_context.Patients == null)
            //{
            //return NotFound();
            //}
            var patient = await _unitOfWork.Patients.Get(q => q.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            await _unitOfWork.Patients.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> PatientExists(int id)
        {
            //return (_context.Patients?.Any(e => e.Id == id)).GetValueOrDefault();
            var patient = await _unitOfWork.Patients.Get(q => q.Id == id);
            return patient != null;
        }
    }
}