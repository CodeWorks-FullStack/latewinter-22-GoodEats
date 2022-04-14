using System.Collections.Generic;
using goodEats.Models;
using goodEats.Services;
using Microsoft.AspNetCore.Mvc;

namespace goodEats.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _pService;
    private readonly ReviewsService _rvService;

    public ProfilesController(ProfilesService pService, ReviewsService rvService)
    {
      _pService = pService;
      _rvService = rvService;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetById(string id)
    {
      try
      {
        Profile profile = _pService.GetById(id);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/reviews")]
    public ActionResult<List<Review>> GetProfileReviews(string id)
    {
      try
      {
        List<Review> reviews = _rvService.GetProfileReviews(id);
        return Ok(reviews);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
