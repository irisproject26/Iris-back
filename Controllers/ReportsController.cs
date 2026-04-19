using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IRIS_API.Data;
using IRIS_API.Models;

namespace IRIS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public ReportsController(AppDbContext context)
        {
            _context = context;
            // Cria a pasta de fotos se ela não existir
            if (!Directory.Exists(_uploadPath)) Directory.CreateDirectory(_uploadPath);
        }

        [HttpPost]
        public async Task<IActionResult> PostReport([FromForm] ReportRequest request)
        {
            // Lógica para salvar a imagem no servidor
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Photo.FileName);
            var filePath = Path.Combine(_uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Photo.CopyToAsync(stream);
            }

            var report = new Report
            {
                Category = request.Category,
                LocalAddress = request.LocalAddress,
                Urgency = request.Urgency,
                AdditionalInfo = request.AdditionalInfo,
                ImageUrl = $"/uploads/{fileName}",
                UserId = request.UserId
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Report enviado com sucesso!" });
        }

        [HttpGet("agente")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            return await _context.Reports.ToListAsync();
        }
    }

    // Objeto para receber os dados do form (Foto + Textos)
    public class ReportRequest
    {
        public IFormFile Photo { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string LocalAddress { get; set; } = null!;
        public UrgencyLevel Urgency { get; set; }
        public string AdditionalInfo { get; set; } = null!;
        public int UserId { get; set; }
    }
}