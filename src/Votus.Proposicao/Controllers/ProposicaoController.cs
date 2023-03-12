using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Votus.Proposicao.API;
using Votus.Proposicao.API.Domain;

namespace Votus.Proposicao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposicaoController : ControllerBase
    {
        private readonly ProposicaoDbContext _context;

        public ProposicaoController(ProposicaoDbContext context)
        {
            _context = context;
        }

        // GET: api/Proposicao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Proposicao>>> GetProposicoes()
        {
          if (_context.Proposicoes == null)
          {
              return NotFound();
          }
            return await _context.Proposicoes.ToListAsync();
        }

        // GET: api/Proposicao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Proposicao>> GetProposicao(Guid id)
        {
          if (_context.Proposicoes == null)
          {
              return NotFound();
          }
            var proposicao = await _context.Proposicoes.FindAsync(id);

            if (proposicao == null)
            {
                return NotFound();
            }

            return proposicao;
        }

        // PUT: api/Proposicao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProposicao(Guid id, Domain.Proposicao proposicao)
        {
            if (id != proposicao.Id)
            {
                return BadRequest();
            }

            _context.Entry(proposicao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposicaoExists(id))
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

        // POST: api/Proposicao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Domain.Proposicao>> PostProposicao(Domain.Proposicao proposicao)
        {
          if (_context.Proposicoes == null)
          {
              return Problem("Entity set 'ProposicaoDbContext.Proposicoes'  is null.");
          }
            _context.Proposicoes.Add(proposicao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProposicao", new { id = proposicao.Id }, proposicao);
        }

        // DELETE: api/Proposicao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposicao(Guid id)
        {
            if (_context.Proposicoes == null)
            {
                return NotFound();
            }
            var proposicao = await _context.Proposicoes.FindAsync(id);
            if (proposicao == null)
            {
                return NotFound();
            }

            _context.Proposicoes.Remove(proposicao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProposicaoExists(Guid id)
        {
            return (_context.Proposicoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
