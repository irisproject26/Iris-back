using IRIS_API.Models;

public class Visita
{
    public int Id { get; set; }
    public string AddressType { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public string Reason { get; set; } = string.Empty;

    public VisitaStatus Status { get; set; } = VisitaStatus.Pendente;

    public int UserId { get; set; }
    public User? User { get; set; }
}