using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository.Mapping.Context;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParceirosController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ParceirosController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Parceiros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parceiro>>> GetParceiros()
        {
            return await _context.Parceiros.Include(x => x.Amigo).ToListAsync();
        }

        // GET: api/Parceiros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parceiro>> GetParceiro(int id)
        {
            var parceiro = await _context.Parceiros.Include(x => x.Amigo).FirstOrDefaultAsync(x => x.Id == id);

            if (parceiro == null)
            {
                return NotFound();
            }

            return parceiro;
        }

        // PUT: api/Parceiros/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParceiro(int id, Parceiro parceiro)
        {
            parceiro.Id = id;
            if (id != parceiro.Id) {
                return BadRequest();
            }
            var parceiroMod = _context.Parceiros.Find(id);
            parceiroMod.Nome = parceiro.Nome;
            parceiroMod.Sobrenome = parceiro.Sobrenome;
            parceiroMod.Email = parceiro.Email;
            parceiroMod.Telefone = parceiro.Telefone;

            _context.Parceiros.Update(parceiroMod);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ParceiroExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Parceiros
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Parceiro>> PostParceiro(ParceiroResponse parceiroResponse)
        {
            var amigoTake = await _context.Amigos.FirstOrDefaultAsync(x => x.Id == parceiroResponse.Amigo.Id);
            parceiroResponse.Amigo = amigoTake;
            Parceiro parceiro = new Parceiro { Nome = parceiroResponse.Nome, Sobrenome = parceiroResponse.Sobrenome, Email = parceiroResponse.Email, Telefone = parceiroResponse.Telefone, Amigo = parceiroResponse.Amigo };
            _context.Parceiros.Add(parceiro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParceiro", new { id = parceiro.Id }, parceiro);
        }

        // DELETE: api/Parceiros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Parceiro>> DeleteParceiro(int id)
        {
            var parceiro = await _context.Parceiros.FindAsync(id);
            if (parceiro == null)
            {
                return NotFound();
            }

            _context.Parceiros.Remove(parceiro);
            await _context.SaveChangesAsync();

            return parceiro;
        }

        private bool ParceiroExists(int id)
        {
            return _context.Parceiros.Any(e => e.Id == id);
        }
    }
}
