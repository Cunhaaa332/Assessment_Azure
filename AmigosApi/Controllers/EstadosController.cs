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
    public class EstadosController : ControllerBase
    {
        private readonly ProjectContext _context;

        public EstadosController(ProjectContext context)
        {
            _context = context;
        }

        // GET: api/Estados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            return await _context.Estados.Include(x => x.Pais).ToListAsync();
        }

        // GET: api/Estados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        // PUT: api/Estados/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estado estado)
        {
            estado.Id = id;
            if (id != estado.Id) {
                return BadRequest();
            }
            var estadoMod = _context.Estados.Find(id);
            estadoMod.Nome = estado.Nome;
            estadoMod.Bandeira = estado.Bandeira;

            _context.Estados.Update(estadoMod);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!EstadoExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Estados
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(EstadoResponse estadoResponse)
        {
           var paisTaker = await _context.Paises.FirstOrDefaultAsync(x => x.Id == estadoResponse.Pais.Id);
            estadoResponse.Pais = paisTaker;
            Estado estado = new Estado { Nome = estadoResponse.Nome, Bandeira = estadoResponse.Bandeira, Pais = estadoResponse.Pais };
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.Id }, estado);
        }

        // DELETE: api/Estados/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Estado>> DeleteEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            var amigos = await _context.Amigos.ToListAsync();

            if (estado == null) {
                return NotFound();
            }

            using (var transection = _context.Database.BeginTransaction()) {
                try {
                    //foreach (var item in amigos) {
                    //    if ((item.Estado.Id) == estado.Id)
                    //        _context.Amigos.Remove(item);
                    //}
                    _context.Estados.Remove(estado);
                    await _context.SaveChangesAsync();
                    transection.Commit();
                } catch {
                    transection.Rollback();
                }
            }
            return estado;
        }

        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.Id == id);
        }
    }
}
