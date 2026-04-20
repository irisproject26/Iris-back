using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRIS_API.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string LocalAddress { get; set; } = string.Empty;

        // Usa o Enum que você já tem definido
        public UrgencyLevel Urgency { get; set; }

        public string AdditionalInfo { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relacionamento com Usuário
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}