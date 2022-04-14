using System.Collections.Generic;
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
  public class RestaurantsController : ControllerBase
  {
    private readonly RestaurantsService _rtService;
    private readonly ReviewsService _rvService;

    public RestaurantsController(RestaurantsService rtService, ReviewsService rvService)
    {
      _rtService = rtService;
      _rvService = rvService;
    }

    [HttpGet]
    public ActionResult<List<Restaurant>> GetAll()
    {
      try
      {
        List<Restaurant> restaurants = _rtService.GetAll();
        return Ok(restaurants);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Restaurant> GetById(int id)
    {
      try
      {
        Restaurant restaurant = _rtService.GetById(id);
        return Ok(restaurant);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant data)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        data.CreatorId = userInfo.Id;
        Restaurant restaurant = _rtService.Create(data);
        return Ok(restaurant);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Restaurant>> Update([FromBody] Restaurant updateData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updateData.Id = id;
        updateData.CreatorId = userInfo.Id;
        Restaurant restaurant = _rtService.Update(updateData);
        return Ok(restaurant);
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
        return _rtService.Remove(id, userInfo.Id);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/reviews")]
    public ActionResult<List<Review>> GetRestaurantReviews(int id)
    {
      try
      {
        List<Review> reviews = _rvService.GetRestaurantReviews(id);
        return Ok(reviews);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}