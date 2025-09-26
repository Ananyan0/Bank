using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Domain.Entities;
using System.IO;

namespace Bank.Application.Interfaces;

public interface IDirectorService
{
    Task<DirectorResponseDto> CreateDirectorAsync(CreateDirectorRequest request);
    Task<List<Director>> GetDirectorsWithBracnhesAsync();
    Task DeleteDirectorAsync(int id);
    Task UpdateAsync(Director director);
    Task DeleteAllDirectorsAsync();
}
