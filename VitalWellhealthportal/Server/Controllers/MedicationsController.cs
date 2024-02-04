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
    public class MedicationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Medications
        [HttpGet]
        public async Task<IActionResult> GetMedications()
        {
            //if (_context.Medications == null)
            //{
            //return NotFound();
            //}
            //return await _context.Medications.ToListAsync();
            var medications = await _unitOfWork.Medications.GetAll(includes:q=>q.Include(x=>x.Appointment));
            return Ok(medications);
        }

        // GET: api/Medications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedication(int id)
        {
            //if (_context.Medications == null)
            //{
            // return NotFound();
            //}
            var medication = await _unitOfWork.Medications.Get(q => q.Id == id);

            if (medication == null)
            {
                return NotFound();
            }

            return Ok(medication);
        }

        // PUT: api/Medications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedication(int id, Medication medication)
        {
            if (id != medication.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Medication).State = EntityState.Modified;
            _unitOfWork.Medications.Update(medication);
                
            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MedicationExists(id))
                if (!await MedicationExists(id))
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

        // POST: api/Medications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medication>> PostMedication(Medication medication)
        {
            //if (_context.Medications == null)
            //{
            //return Problem("Entity set 'ApplicationDbContext.Medications'  is null.");
            //}
            await _unitOfWork.Medications.Insert(medication);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMedication", new { id = medication.Id }, medication);
        }

        // DELETE: api/Medications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            //if (_context.Medications == null)
            //{
            //return NotFound();
            //}
            var medication = await _unitOfWork.Medications.Get(q => q.Id == id);
            if (medication == null)
            {
                return NotFound();
            }

            await _unitOfWork.Medications.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> MedicationExists(int id)
        {
            //return (_context.Medications?.Any(e => e.Id == id)).GetValueOrDefault();
            var medication = await _unitOfWork.Medications.Get(q => q.Id == id);
            return medication != null;
        }
    }
}