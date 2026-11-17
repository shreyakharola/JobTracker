using JobTracker.Core.Entities;
using JobTracker.Core.Interfaces;
using JobTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _db;

    public CompanyRepository(AppDbContext db) => _db = db;

    public Task<List<Company>> GetAllAsync(int userId) =>
        _db.Companies.Where(c => c.UserId == userId).OrderBy(c => c.Name).ToListAsync();

    public Task<Company?> GetByIdAsync(int userId, int id) =>
        _db.Companies.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);

    public Task<bool> NameExistsAsync(int userId, string name, int? excludeId = null) =>
        _db.Companies.AnyAsync(c => c.UserId == userId && c.Name == name && c.Id != excludeId);

    public Task<bool> HasApplicationsAsync(int companyId) =>
        _db.JobApplications.AnyAsync(a => a.CompanyId == companyId);

    public async Task AddAsync(Company company) => await _db.Companies.AddAsync(company);

    public void Remove(Company company) => _db.Companies.Remove(company);

    public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();
}