using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Votus.Pessoa.API;
using Votus.Pessoa.API.Domain;
using Votus.Pessoa.API.Events;

namespace Votus.Pessoa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaDbContext _context;
        private readonly IMediator mediator;

        public PessoaController(PessoaDbContext context, IMediator mediator)
        {
            _context = context;
            this.mediator = mediator;
        }


        /// <summary>
        /// Lista todas pessoas registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Pessoa>>> GetPessoas()
        {
          if (_context.Pessoas == null)
          {
              return NotFound();
          }
            return await _context.Pessoas.ToListAsync();
        }

        /// <summary>
        /// Obtêm uma Pessoa dado um Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Pessoa>> GetPessoa(Guid id)
        {
          if (_context.Pessoas == null)
          {
              return NotFound();
          }
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        /// <summary>
        /// Atualiza o registro de uma Pessoa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(Guid id, Domain.Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await mediator.Publish(new PessoaChangedEvent(id, pessoa));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
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
        /// Cria uma nova Pessoa
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Domain.Pessoa>> PostPessoa(Domain.Pessoa pessoa)
        {
          if (_context.Pessoas == null)
          {
              return Problem("Entity set 'PessoaDbContext.Pessoas'  is null.");
          }
            _context.Pessoas.Add(pessoa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PessoaExists(pessoa.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPessoa", new { id = pessoa.Id }, pessoa);
        }

        /// <summary>
        /// Deleta uma Pessoa dado um Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(Guid id)
        {
            if (_context.Pessoas == null)
            {
                return NotFound();
            }
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(Guid id)
        {
            return (_context.Pessoas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
