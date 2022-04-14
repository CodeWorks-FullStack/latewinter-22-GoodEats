using System.Collections.Generic;
using goodEats.Models;
using goodEats.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace goodEats.Services
{
  public class ReviewsService
  {
    private readonly ReviewsRepository _repo;

    public ReviewsService(ReviewsRepository repo)
    {
      _repo = repo;
    }

    internal Review Create(Review data)
    {
      return _repo.Create(data);
    }

    internal ActionResult<string> Remove(int id, string userId)
    {

      return _repo.Remove(id);
    }

    internal List<Review> GetProfileReviews(string profileId)
    {
      return _repo.GetProfileReviews(profileId);
    }

    internal List<Review> GetRestaurantReviews(int restaurantId)
    {
      return _repo.GetRestaurantReviews(restaurantId);
    }
  }
}