using JobTracker.Core.Entities;

namespace JobTracker.Core.Interfaces;

public interface IApplicationRepository
{
    Task<List<JobApplication>> GetAllAsync(int userId);
    Task<JobApplication?> GetByIdAsync(int userId, int id);
    //Task<List<ApplicationStatusHistory>> GetHistoryAsync(int userId, int applicationId);
    Task AddAsync(JobApplication application);
    //Task AddHistoryAsync(ApplicationStatusHistory history);
    void Remove(JobApplication application);
    Task<int> SaveChangesAsync();
}