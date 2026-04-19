namespace IRIS_API.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string LocalAddress { get; set; } = string.Empty;
        public UrgencyLevel Urgency { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
