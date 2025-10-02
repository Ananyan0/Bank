using AutoMapper;
using Bank.Application.DTOs;
using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BranchController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// Get all branches
    [Authorize(Roles = "Admin")]
    [HttpGet("Get all branches")]
    public async Task<IActionResult> GetAll()
    {
        var branches = await _unitOfWork.Branches.GetAllAsync();

        var response = _mapper.Map<List<BranchResponseDto>>(branches);

        return Ok(response);
    }

    /// Get branch by id
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(id);

        await _unitOfWork.CompleteAsync();

        var response = _mapper.Map<BranchResponseDto>(branch);

        return Ok(response);
    }

    /// Create a new branch
    /// 
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateBranchRequest request)
    {
        var branch = _mapper.Map<Branch>(request);

        await _unitOfWork.Branches.AddAsync(branch);
        await _unitOfWork.CompleteAsync();

        var response = _mapper.Map<BranchResponseDto>(branch);

        return Ok(response);
    }

    /// Update a branch
    /// 
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] BranchUpdateDto update)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(id);

        _mapper.Map(update, branch);

        await _unitOfWork.Branches.UpdateAsync(branch);
        await _unitOfWork.CompleteAsync();

        var respoonse = _mapper.Map<BranchResponseDto>(branch);
        
        return Ok(respoonse);
    }

    /// Delete a branch
    /// 
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(id);

        await _unitOfWork.Branches.DeleteAsync(branch);
        await _unitOfWork.CompleteAsync();

        return Ok($"Success Branch with Id -> {branch.Id} has been deleted.");
    }
}
