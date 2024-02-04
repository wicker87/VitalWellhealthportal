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
    public class PaymentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            //if (_context.Payments == null)
            //{
            //return NotFound();
            //}
            //return await _context.Payments.ToListAsync();
            var payments = await _unitOfWork.Payments.GetAll();
            return Ok(payments);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            //if (_context.Payments == null)
            //{
            // return NotFound();
            //}
            var payment = await _unitOfWork.Payments.Get(q => q.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            //_context.Entry(payment).State = EntityState.Modified;
            _unitOfWork.Payments.Update(payment);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!PaymentExists(id))
                if (!await PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            //if (_context.Payments == null)
            //{
            //return Problem("Entity set 'ApplicationDbContext.Payments'  is null.");
            //}
            await _unitOfWork.Payments.Insert(payment);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetAppointment", new { id = payment.Id }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            //if (_context.Payments == null)
            //{
            //return NotFound();
            //}
            var payment = await _unitOfWork.Payments.Get(q => q.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            await _unitOfWork.Payments.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> PaymentExists(int id)
        {
            //return (_context.Payments?.Any(e => e.Id == id)).GetValueOrDefault();
            var payment = await _unitOfWork.Payments.Get(q => q.Id == id);
            return payment != null;
        }
    }
}