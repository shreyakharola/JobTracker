using JobTracker.Core.Enums;

namespace JobTracker.Core.Entities;

public class  ApplicatioStatusHistory
{
    public int Id { get; set; }
    public int JobApplicationId { get; set; }
    public ApplicationStatus OldStatus { get; set; }
    public ApplicationStatus NewStatus { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public JobApplication JobApplication { get; set; } = null!;
}
