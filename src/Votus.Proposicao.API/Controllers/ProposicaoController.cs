using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Votus.Proposicao.API;
using Votus.Proposicao.API.Domain;
using Votus.Proposicao.API.Event;

namespace Votus.Proposicao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProposicaoController : ControllerBase
    {
        private readonly ProposicaoDbContext _context;
        private readonly IMediator mediator;

        public ProposicaoController(ProposicaoDbContext context, IMediator mediator)
        {
            _context = context;
            this.mediator = mediator;
        }

        /// <summary>
        /// Lista as proposições
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Proposicao>>> GetProposicoes()
        {
          if (_context.Proposicoes == null)
          {
              return NotFound();
          }
            return await _context.Proposicoes.ToListAsync();
        }

        /// <summary>
        /// Obtêm uma proposição através de um Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Atualiza uma proposição
        /// </summary>
        /// <param name="id"></param>
        /// <param name="proposicao"></param>
        /// <returns></returns>
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
                
                var proposicaoChangedEvent = new ProposicaoChangedEvent(proposicao.Id.ToString(), proposicao);

                await mediator.Publish(proposicaoChangedEvent);

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

        /// <summary>
        /// Cadastra uma nova proposição
        /// </summary>
        /// <param name="proposicao"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deleta uma proposição através de seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
