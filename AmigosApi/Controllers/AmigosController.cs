using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository.Mapping.Context;
using System.Security.Cryptography.X509Certificates;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigosController : ControllerBase
    {
        private readonly ProjectContext _context;

        public AmigosController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Amigos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amigo>>> GetAmigos()
        {
            return await _context.Amigos.Include(x => x.Amigos).ToListAsync();
        }

        // GET: api/Amigos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Amigo>> GetAmigo(int id)
        {
            var amigo = await _context.Amigos.Include(x => x.Amigos).FirstOrDefaultAsync(x => x.Id == id);

            if (amigo == null)
            {
                return NotFound();
            }

            return amigo;
        }

        // PUT: api/Amigos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmigo(int id, Amigo amigo)
        {
            amigo.Id = id;
            if (id != amigo.Id) {
                return BadRequest();
            }
            var amigoMod = _context.Amigos.Find(id);
            amigoMod.Nome = amigo.Nome;
            amigoMod.Sobrenome = amigo.Sobrenome;
            amigoMod.Foto = amigo.Foto;
            amigoMod.Email = amigo.Email;
            amigoMod.Telefone = amigo.Telefone;
            amigoMod.Birth = amigo.Birth;
            amigoMod.Pais = amigo.Pais;
            amigoMod.Estado = amigo.Estado;

            _context.Amigos.Update(amigoMod);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!AmigoExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Amigos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amigo>> PostAmigo(Amigo amigo)
        {
            _context.Amigos.Add(amigo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmigo", new { id = amigo.Id }, amigo);
        }

        // DELETE: api/Amigos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amigo>> DeleteAmigo(int id)
        {
            var amigo = _context.Amigos.Find(id);
            if (amigo == null) {
                return NotFound();
            }

            using (var transection = _context.Database.BeginTransaction()) {
                try {
                    _context.Amigos.Remove(amigo);
                    await _context.SaveChangesAsync();
                    transection.Commit();
                } catch {
                    transection.Rollback();
                }
            }
            return NoContent();
        }

        private bool AmigoExists(int id)
        {
            return _context.Amigos.Any(e => e.Id == id);
        }
    }
}
