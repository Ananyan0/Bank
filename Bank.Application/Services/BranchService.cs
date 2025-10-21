using Bank.Application.Exceptions;
using Bank.Application.Interfaces;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;
using NHibernate.Cache;

namespace Bank.Application.Services;

public class BranchService : IBranchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    private readonly ILogger<BranchService> _logger;
    public BranchService(IUnitOfWork unitOfWork, ICacheService cacheService, ILogger<BranchService> logger)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<IEnumerable<Branch>> GetAllAsync()
    {

        const string cacheKey = "all_branches";

        // Try get from cache first
        var cached = await _cacheService.GetAsync<IEnumerable<Branch>>(cacheKey);
        if (cached != null)
        {
            _logger.LogInformation("Branches retrieved from cache.");
            return cached;
        }

        // Otherwise get from DB and cache it
        var branches = await _unitOfWork.Branches.GetAllAsync();
        if (branches == null)
            throw new BranchException("No branch found");
        await _cacheService.SetAsync(cacheKey, branches, TimeSpan.FromMinutes(10));


        return branches;




        //var branch = await _unitOfWork.Branches.GetAllAsync();
        //if (branch == null)
        //    throw new BranchException("No branch found");

        //return branch;
    }
}
