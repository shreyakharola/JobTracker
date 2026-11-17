using System;
using System.Collections.Generic;
using System.Text;
using JobTracker.Core.Entities;

namespace JobTracker.Core.Interfaces;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllAsync(int userId);
    Task<Company?> GetByIdAsync(int userId, int id);
    Task<bool>NameExistsAsync(int userId, string name, int? excludeId = null);
    Task<bool>HasApplicationsAsync(int companyId);
    Task AddAsync(Company company);
    void Remove(Company company);
    Task<int> SaveChangesAsync();
}

//internal interface ICompanyRepository
//{
//}
