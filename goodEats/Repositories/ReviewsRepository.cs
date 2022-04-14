using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using goodEats.Models;

namespace goodEats.Repositories
{
  public class ReviewsRepository
  {
    private readonly IDbConnection _db;

    public ReviewsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Review> GetProfileReviews(string profileId)
    {
      // NOTE the location of the AS is important, it has to come directly after the model that has what your AS is renaming (the review has the Restaurant name, so the as comes after it)
      string sql = @"
    SELECT
       rv.*,
       rt.name AS RestaurantName
    FROM reviews rv
    JOIN restaurants rt ON rt.id = rv.restaurantId
    WHERE rv.creatorId = @profileId;
      ";
      return _db.Query<Review>(sql, new { profileId }).ToList();
    }

    // NOTE alternate way for the exact same method that always includes the Creator
    // internal List<Review> GetProfileReviews(string profileId)
    // {
    // NOTE the location of the AS is important, it has to come directly after the model that has what your AS is renaming (the review has the Restaurant name, so the as comes after it)
    //   string sql = @"
    // SELECT
    //     rv.*,
    //     rt.name AS RestaurantName,
    //     a.*
    // FROM reviews rv
    // JOIN accounts a ON rv.creatorId = a.id
    // JOIN  restaurants rt ON rv.restaurantId = rt.id
    // WHERE rv.creatorId = @profileId;
    //   ";
    //   return _db.Query<Review, Profile, Review>(sql, (rv, a) =>
    //   {
    //     rv.Creator = a;
    //     return rv;
    //   }, new { profileId }).ToList();
    // }

    internal List<Review> GetRestaurantReviews(int restaurantId)
    {
      string sql = @"
    SELECT
        rv.*,
        a.*
    FROM reviews rv
    JOIN accounts a ON rv.creatorId = a.id
    WHERE rv.restaurantId = @restaurantId;
      ";
      return _db.Query<Review, Profile, Review>(sql, (rv, a) =>
      {
        rv.Creator = a;
        return rv;
      }, new { restaurantId }).ToList();
    }

    // NOTE alternate way for the exact same method that always includes the restaurants name
    // internal List<Review> GetRestaurantReviews(int restaurantId)
    // {
    // NOTE the location of the AS is important, it has to come directly after the model that has what your AS is renaming (the review has the Restaurant name, so the as comes after it)
    //   string sql = @"
    // SELECT
    //     rv.*,
    //     rt.name AS RestaurantName,
    //     a.*
    // FROM reviews rv
    // JOIN accounts a ON rv.creatorId = a.id
    // JOIN  restaurants rt ON rv.restaurantId = rt.id
    // WHERE rv.restaurantId = @restaurantId;
    //   ";
    //   return _db.Query<Review, Profile, Review>(sql, (rv, a) =>
    //   {
    //     rv.Creator = a;
    //     return rv;
    //   }, new { restaurantId }).ToList();
    // }
  }
}