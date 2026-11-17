using System.ComponentModel.DataAnnotations;

namespace JobTracker.Core.DTOs;

public record CreateCompanyRequest(
    [Required, MaxLength(150)] string Name,
    [MaxLength(300)] string? Website,
    [MaxLength(100)] string? Location);

public record UpdateCompanyRequest(
    [Required, MaxLength(150)] string Name,
    [MaxLength(300)] string? Website,
    [MaxLength(100)] string? Location);

public record CompanyResponse(int Id, string Name, string? Website, string? Location);