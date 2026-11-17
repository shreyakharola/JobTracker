using JobTracker.Core.Enums;

namespace JobTracker.Core.Entities;

public class JobApplication
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string? JobUrl { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Saved;
    public decimal? ExpectedSalary { get; set; }
    public DateOnly? AppliedOn { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public User User { get; set; } = null!;
    public Company Company { get; set; } = null!;
    public ICollection<ApplicatioStatusHistory> History { get; set; } = new List<ApplicatioStatusHistory>();
}