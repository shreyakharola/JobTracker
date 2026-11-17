using JobTracker.Core.DTOs;
using JobTracker.Core.Entities;
using JobTracker.Core.Exceptions;
using JobTracker.Core.Interfaces;

namespace JobTracker.Core.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repo;

    public CompanyService(ICompanyRepository repo) => _repo = repo;

    public async Task<List<CompanyResponse>> GetAllAsync(int userId)
    {
        var companies = await _repo.GetAllAsync(userId);
        return companies.Select(ToResponse).ToList();
    }

    public async Task<CompanyResponse?> GetByIdAsync(int userId, int id)
    {
        var company = await _repo.GetByIdAsync(userId, id);
        return company is null ? null : ToResponse(company);
    }

    public async Task<CompanyResponse> CreateAsync(int userId, CreateCompanyRequest request)
    {
        var name = request.Name.Trim();
        if (await _repo.NameExistsAsync(userId, name))
            throw new ConflictException($"A company named '{name}' already exists.");

        var company = new Company
        {
            UserId = userId,
            Name = name,
            Website = request.Website,
            Location = request.Location
        };

        await _repo.AddAsync(company);
        await _repo.SaveChangesAsync();
        return ToResponse(company);
    }

    public async Task<bool> UpdateAsync(int userId, int id, UpdateCompanyRequest request)
    {
        var company = await _repo.GetByIdAsync(userId, id);
        if (company is null) return false;

        var name = request.Name.Trim();
        if (await _repo.NameExistsAsync(userId, name, excludeId: id))
            throw new ConflictException($"A company named '{name}' already exists.");

        company.Name = name;
        company.Website = request.Website;
        company.Location = request.Location;
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int userId, int id)
    {
        var company = await _repo.GetByIdAsync(userId, id);
        if (company is null) return false;

        if (await _repo.HasApplicationsAsync(id))
            throw new ConflictException("This company still has applications. Delete them first.");

        _repo.Remove(company);
        await _repo.SaveChangesAsync();
        return true;
    }

    private static CompanyResponse ToResponse(Company c) =>
        new(c.Id, c.Name, c.Website, c.Location);
}