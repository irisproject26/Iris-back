using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IRIS_API.Data;
using IRIS_API.Models;

namespace IRIS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VisitasController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/visitas (Para o User Normal marcar a visita)
        [HttpPost]
        public async Task<IActionResult> PostVisita(Visita visita)
        {
            _context.Visitas.Add(visita);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Agendamento realizado com sucesso!" });
        }

        // GET: api/visitas/agente (Para o Agente ver todos os pedidos)
        [HttpGet("agente")]
        public async Task<ActionResult<IEnumerable<Visita>>> GetVisitas()
        {
            return await _context.Visitas.Include(v => v.User).ToListAsync();
        }
        // GET: api/visitas/pendentes
        [HttpGet("pendentes")]
        public async Task<ActionResult<IEnumerable<Visita>>> GetPendentes()
        {
            return await _context.Visitas
                .Where(v => v.Status == VisitaStatus.Pendente)
                .Include(v => v.User)
                .ToListAsync();
        }

        // GET: api/visitas/aceitas
        [HttpGet("aceitas")]
        public async Task<ActionResult<IEnumerable<Visita>>> GetAceitas()
        {
            return await _context.Visitas
                .Where(v => v.Status == VisitaStatus.Aceito)
                .Include(v => v.User)
                .ToListAsync();
        }

        // PATCH: api/visitas/status/5
        [HttpPatch("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] VisitaStatus novoStatus)
        {
            var visita = await _context.Visitas.FindAsync(id);
            if (visita == null) return NotFound();

            visita.Status = novoStatus; // Aqui os dois são VisitaStatus, então funciona!
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}