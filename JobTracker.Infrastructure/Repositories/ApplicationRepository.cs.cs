using JobTracker.Core.Entities;
using JobTracker.Core.Interfaces;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext _db;

    public ApplicationRepository(AppDbContext db) => _db = db;

    public Task<List<JobApplication>> GetAllAsync(int userId) =>
        _db.JobApplications
           .Include(a => a.Company)
           .Where(a => a.UserId == userId)
           .OrderByDescending(a => a.CreatedAt)
           .ToListAsync();

    public Task<JobApplication?> GetByIdAsync(int userId, int id) =>
        _db.JobApplications
           .Include(a => a.Company)
           .FirstOrDefaultAsync(a => a.UserId == userId && a.Id == id);

    //public Task<List<ApplicationStatusHistory>> GetHistoryAsync(int userId, int applicationId) =>
    //    _db.ApplicationStatusHistories
    //       .Where(h => h.JobApplicationId == applicationId && h.JobApplication.UserId == userId)
    //       .OrderBy(h => h.ChangedAt)
    //       .ToListAsync();

    public async Task AddAsync(JobApplication application) =>
        await _db.JobApplications.AddAsync(application);

    //public async Task AddHistoryAsync(ApplicationStatusHistory history) =>
    //    await _db.ApplicationStatusHistories.AddAsync(history);

    public void Remove(JobApplication application) => _db.JobApplications.Remove(application);

    public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();
}