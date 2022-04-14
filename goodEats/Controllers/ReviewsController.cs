using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using goodEats.Models;
using goodEats.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace goodEats.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ReviewsController : ControllerBase
  {

    private readonly ReviewsService _rvService;

    public ReviewsController(ReviewsService rvService)
    {
      _rvService = rvService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Review>> Create([FromBody] Review data)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        data.CreatorId = userInfo.Id;
        Review review = _rvService.Create(data);
        return review;

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return _rvService.Remove(id, userInfo.Id);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}