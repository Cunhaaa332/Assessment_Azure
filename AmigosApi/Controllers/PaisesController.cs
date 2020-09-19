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
    public class PaisesController : ControllerBase
    {
        private readonly ProjectContext _context;

        public PaisesController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Paises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises()
        {
            return await _context.Paises.Include(x => x.Estados).ToListAsync();
        }

        // GET: api/Paises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> GetPais(int id)
        {
            var pais = await _context.Paises.Include(x => x.Estados).FirstOrDefaultAsync(x => x.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            return pais;
        }

        // PUT: api/Paises/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPais(int id, Pais pais)
        {
            pais.Id = id;
            if (id != pais.Id) {
                return BadRequest();
            }
            var paisMod = _context.Paises.Find(id);
            paisMod.Nome = pais.Nome;
            paisMod.Bandeira = pais.Bandeira;

            _context.Paises.Update(paisMod);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!PaisExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Paises
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pais>> PostPais(Pais pais)
        {
            _context.Paises.Add(pais);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPais", new { id = pais.Id }, pais);
        }

        // DELETE: api/Paises/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pais>> DeletePais(int id) {
            var pais = await _context.Paises.Include(x => x.Estados).FirstOrDefaultAsync(x => x.Id == id);
            var amigos = await _context.Amigos.ToListAsync();

            if (pais == null) {
                return NotFound();
            }

            using (var transection = _context.Database.BeginTransaction()) {
                try {

                    foreach (var item in pais.Estados) {
                        _context.Estados.Remove(item);
                    }

                    //foreach (var item in amigos) {
                    //    if ((item.Pais.Id) == pais.Id)
                    //        _context.Amigos.Remove(item);
                    //}
                    _context.Paises.Remove(pais);
                    await _context.SaveChangesAsync();
                    transection.Commit();
                } catch {
                    transection.Rollback();
                }
            }
            return NoContent();
        }

        private bool PaisExists(int id)
        {
            return _context.Paises.Any(e => e.Id == id);
        }
    }
}
