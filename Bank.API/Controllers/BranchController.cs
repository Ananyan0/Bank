using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.DTOs.ResponseDTOs;
using Bank.Application.Interfaces;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BranchController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get all branches
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var branches = await _unitOfWork.Branches.GetAllAsync();
        return Ok(branches.Select(b => new BranchResponse
        {
            Id = b.Id,
            Name = b.Name
        }));
    }

    /// <summary>
    /// Get branch by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(id);
        if (branch == null) return NotFound();

        return Ok(new BranchResponse
        {
            Id = branch.Id,
            Name = branch.Name
        });
    }

    /// <summary>
    /// Create a new branch
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateBranchRequest request)
    {
        var branch = new Branch
        {
            Name = request.Name
        };

        await _unitOfWork.Branches.AddAsync(branch);
        await _unitOfWork.CompleteAsync();

        return Ok(new BranchResponse
        {
            Id = branch.Id,
            Name = branch.Name
        });
    }

    /// <summary>
    /// Update a branch
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateBranchRequest request)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(id);
        if (branch == null) return NotFound();

        branch.Name = request.Name;

        await _unitOfWork.Branches.UpdateAsync(branch);
        await _unitOfWork.CompleteAsync();

        return Ok(new BranchResponse
        {
            Id = branch.Id,
            Name = branch.Name
        });
    }

    /// <summary>
    /// Delete a branch
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var branch = await _unitOfWork.Branches.GetByIdAsync(id);
        if (branch == null) return NotFound();

        await _unitOfWork.Branches.DeleteAsync(branch);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
