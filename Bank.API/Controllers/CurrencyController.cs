using Bank.Application.DTOs.CreateDTOs;
using Bank.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly IExchangeRequestService _svc;
    public CurrencyController(IExchangeRequestService svc) => _svc = svc;


    [HttpPost("request")]
    [AllowAnonymous]
    public IActionResult RequestExchange([FromBody] CreateCurrencyRequest request)
    {
        _svc.RequestExchange(request);

        return Accepted(new { message = "Exchange request published" });
    }
}
