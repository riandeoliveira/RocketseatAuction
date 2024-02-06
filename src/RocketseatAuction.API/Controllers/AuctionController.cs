using Microsoft.AspNetCore.Mvc;

using RocketseatAuction.API.Entities;
using RocketseatAuction.API.UseCases.Auctions.GetCurrent;

namespace RocketseatAuction.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuctionController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Auction), StatusCodes.Status200OK)]
    public IActionResult GetCurrentAuction()
    {
        var useCase = new GetCurrentAuctionUseCase();
        var result = useCase.Execute();

        return result is null ? NoContent() : Ok(result);
    }
}
