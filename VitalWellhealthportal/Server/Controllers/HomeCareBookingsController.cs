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
    public class HomeCareBookingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeCareBookingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/HomeCareBookings
        [HttpGet]
        public async Task<IActionResult> GetHomeCareBookings()
        {
            //if (_context.HomeCareBookings == null)
            //{
            //return NotFound();
            //}
            //return await _context.HomeCareBookings.ToListAsync();
            var homeCareBookings = await _unitOfWork.HomeCareBookings.GetAll(includes: q=>q.Include(x=>x.Patient));
            return Ok(homeCareBookings);
        }

        // GET: api/HomeCareBookings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeCareBooking(int id)
        {
            //if (_context.HomeCareBookings == null)
            //{
            // return NotFound();
            //}
            var homeCareBooking = await _unitOfWork.HomeCareBookings.Get(q => q.Id == id);

            if (homeCareBooking == null)
            {
                return NotFound();
            }

            return Ok(homeCareBooking);
        }

        // PUT: api/HomeCareBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomeCareBooking(int id, HomeCareBooking homeCareBooking)
        {
            if (id != homeCareBooking.Id)
            {
                return BadRequest();
            }

            //_context.Entry(homeCareBooking).State = EntityState.Modified;
            _unitOfWork.HomeCareBookings.Update(homeCareBooking);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!HomeCareBookingExists(id))
                if (!await HomeCareBookingExists(id))
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

        // POST: api/HomeCareBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HomeCareBooking>> PostHomeCareBooking(HomeCareBooking homeCareBooking)
        {
            //if (_context.HomeCareBookings == null)
            //{
            //return Problem("Entity set 'ApplicationDbContext.HomeCareBookings'  is null.");
            //}
            await _unitOfWork.HomeCareBookings.Insert(homeCareBooking); 
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetHomeCareBooking", new { id = homeCareBooking.Id }, homeCareBooking);
        }

        // DELETE: api/HomeCareBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomeCareBooking(int id)
        {
            //if (_context.HomeCareBookings == null)
            //{
            //return NotFound();
            //}
            var homeCareBooking = await _unitOfWork.HomeCareBookings.Get(q => q.Id == id);
            if (homeCareBooking == null)
            {
                return NotFound();
            }

            await _unitOfWork.HomeCareBookings.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> HomeCareBookingExists(int id)
        {
            //return (_context.HomeCareBookings?.Any(e => e.Id == id)).GetValueOrDefault();
            var homeCareBooking = await _unitOfWork.HomeCareBookings.Get(q => q.Id == id);
            return homeCareBooking != null;
        }
    }
}
