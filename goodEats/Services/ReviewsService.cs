using System;
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
      throw new NotImplementedException();
    }

    internal ActionResult<string> Remove(int id1, string id2)
    {
      throw new NotImplementedException();
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