using JobTracker.Core.DTOs;

namespace JobTracker.Core.Interfaces;

public interface ICompanyService
{
    Task<List<CompanyResponse>> GetAllAsync(int userId);
    Task<CompanyResponse?> GetByIdAsync(int userId, int id);
    Task<CompanyResponse> CreateAsync(int userId, CreateCompanyRequest request);
    Task<bool> UpdateAsync(int userId, int id, UpdateCompanyRequest request);
    Task<bool> DeleteAsync(int userId, int id);
}