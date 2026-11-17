using JobTracker.Core.Enums;
namespace JobTracker.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public ICollection<Company> Companies { get; set; } = new List<Company>();
    public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}