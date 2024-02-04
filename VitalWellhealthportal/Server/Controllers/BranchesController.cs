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
    public class BranchesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<IActionResult> GetBranches()
        {
            //if (_context.Branchs == null)
            //{
            //return NotFound();
            //}
            //return await _context.Branchs.ToListAsync();
            var branches = await _unitOfWork.Branches.GetAll();
            return Ok(branches);
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(int id)
        {
            //if (_context.Branches == null)
            //{
            // return NotFound();
            //}
            var branch = await _unitOfWork.Branches.Get(q => q.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(int id, Branch branch)
        {
            if (id != branch.Id)
            {
                return BadRequest();
            }

            //_context.Entry(branch).State = EntityState.Modified;
            _unitOfWork.Branches.Update(branch);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!BranchExists(id))
                if (!await BranchExists(id))
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

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Branch>> PostBranch(Branch branch)
        {
            //if (_context.Branchs == null)
            //{
            //return Problem("Entity set 'ApplicationDbContext.Branches'  is null.");
            //}
            await _unitOfWork.Branches.Insert(branch);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetBranch", new { id = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            //if (_context.Branches == null)
            //{
            //return NotFound();
            //}
            var branch = await _unitOfWork.Branches.Get(q => q.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            await _unitOfWork.Branches.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> BranchExists(int id)
        {
            //return (_context.Branches?.Any(e => e.Id == id)).GetValueOrDefault();
            var branch = await _unitOfWork.Branches.Get(q => q.Id == id);
            return branch != null;
        }
    }
}
