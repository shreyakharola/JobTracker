namespace JobTracker.Core.Entities;

public class Company
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? Location { get; set; }

    public User User { get; set; } = null!;
    public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
}