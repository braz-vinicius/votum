﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Votus.Voto.API;
using Votus.Voto.API.Domain;

namespace Votus.Voto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly VotoDbContext _context;

        public VotoController(VotoDbContext context)
        {
            _context = context;
        }

        // GET: api/Voto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Voto>>> GetVotos()
        {
          if (_context.Votos == null)
          {
              return NotFound();
          }
            return await _context.Votos.ToListAsync();
        }

        // GET: api/Voto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Voto>> GetVoto(Guid id)
        {
          if (_context.Votos == null)
          {
              return NotFound();
          }
            var voto = await _context.Votos.FindAsync(id);

            if (voto == null)
            {
                return NotFound();
            }

            return voto;
        }

        // PUT: api/Voto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoto(Guid id, Domain.Voto voto)
        {
            if (id != voto.Id)
            {
                return BadRequest();
            }

            _context.Entry(voto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VotoExists(id))
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

        // POST: api/Voto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Domain.Voto>> PostVoto(Domain.Voto voto)
        {
          if (_context.Votos == null)
          {
              return Problem("Entity set 'VotoDbContext.Votos'  is null.");
          }
            _context.Votos.Add(voto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoto", new { id = voto.Id }, voto);
        }

        // DELETE: api/Voto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoto(Guid id)
        {
            if (_context.Votos == null)
            {
                return NotFound();
            }
            var voto = await _context.Votos.FindAsync(id);
            if (voto == null)
            {
                return NotFound();
            }

            _context.Votos.Remove(voto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VotoExists(Guid id)
        {
            return (_context.Votos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
