using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IRIS_API.Data;
using IRIS_API.Models;

namespace IRIS_API.Controllers
{
    [ApiController]
    [Route("api/Denuncias")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public ReportsController(AppDbContext context)
        {
            _context = context;
            // Cria a pasta de uploads caso não exista
            if (!Directory.Exists(_uploadPath)) Directory.CreateDirectory(_uploadPath);
        }

        [HttpPost]
        public async Task<IActionResult> PostReport([FromForm] ReportRequest request)
        {
            try
            {
                if (request.Photo == null || request.Photo.Length == 0)
                    return BadRequest("A foto é obrigatória.");

                // 1. Processar e salvar a imagem
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Photo.FileName);
                var filePath = Path.Combine(_uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(stream);
                }

                // 2. Criar o objeto Report para o banco de dados
                var report = new Report
                {
                    Category = request.Category,
                    LocalAddress = request.LocalAddress,
                    // Converte o int (0, 1, 2) vindo do App para o seu Enum UrgencyLevel
                    Urgency = (UrgencyLevel)request.Urgency,
                    AdditionalInfo = request.AdditionalInfo,
                    ImageUrl = $"/uploads/{fileName}",
                    UserId = request.UserId,
                    CreatedAt = DateTime.Now
                };

                _context.Reports.Add(report);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Denúncia enviada com sucesso!", id = report.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar denúncia: {ex.Message}");
            }
        }

        [HttpGet("agente")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            // Retorna todas as denúncias para o painel do agente
            return await _context.Reports.OrderByDescending(r => r.CreatedAt).ToListAsync();
        }
    }

    // DTO para receber os dados do formulário mobile
    public class ReportRequest
    {
        public IFormFile Photo { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string LocalAddress { get; set; } = null!;
        public int Urgency { get; set; } // Recebe 0, 1 ou 2
        public string AdditionalInfo { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}