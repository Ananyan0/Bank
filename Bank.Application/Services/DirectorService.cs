using AutoMapper;
using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Exceptions;
using Bank.Application.Interfaces;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Bank.Application.Services;

public class DirectorService : IDirectorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DirectorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //public async Task<(int directorId, string branchName)> CreateDirectorAsync(string directorName, string email, string phone, int branchId)
    //{
    //    var branch = await _unitOfWork.Branches.GetByIdAsync(branchId);
    //    if(branch == null)
    //    {
    //        throw new ArgumentException($"Branch with ID {branchId} does not exist.");
    //    }

    //    var existingDirector = await _unitOfWork.Directors
    //        .FindAsync(d => d.BranchId == branchId);

    //        if (existingDirector != null)
    //          throw new InvalidOperationException(
    //             $"Branch '{branch.Name}' already has a director."
    //        );



    //    var director = new Director
    //    {
    //        Name = directorName,
    //        Email = email,
    //        PhoneNumber = phone,
    //        BranchId = branchId
    //    };

    //    await _unitOfWork.Directors.AddAsync(director);
    //    await _unitOfWork.SaveChangesAsync();


    //    return (director.Id, branch.Name);
    //}

    public async Task<DirectorResponseDto> CreateDirectorAsync(CreateDirectorRequest request)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(request.BranchId);
        if (branch == null)
            throw new BranchException($"Branch with id -> {request.BranchId} does not exist.");

        var existingDirector = await _unitOfWork.Directors
            .FindAsync(d => d.BranchId == request.BranchId);

        if (existingDirector != null)
            throw new BranchException($"Branch '{branch.Name}' already has a director.", StatusCodes.Status409Conflict);


        var director = _mapper.Map<Director>(request);

        await _unitOfWork.Directors.AddAsync(director);
        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<DirectorResponseDto>(director);

        return response;
    }




    public async Task DeleteAllDirectorsAsync()
    {


        var directors = await _unitOfWork.Directors.GetAllAsync();
        foreach (var director in directors)
        {
            await _unitOfWork.Directors.DeleteAsync(director);
        }
    }

    public Task DeleteDirectorAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Director>> GetDirectorsWithBracnhesAsync()
    {
        var directors = await _unitOfWork.Directors
            .GetAllWithIncludeAsync(d => d.Branch);

        return directors;
    }

    public Task UpdateAsync(Director director)
    {
        throw new NotImplementedException();
    }
}
