using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using goodEats.Models;

namespace goodEats.Repositories
{
  public class RestaurantsRepository
  {
    private readonly IDbConnection _db;

    public RestaurantsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Restaurant> GetAll()
    {
      string sql = @"
      SELECT
        rs.*,
      AVG(rv.rating) AS AverageRating,
      COUNT(rv.id) AS TotalReviews
      FROM restaurants rs
      LEFT JOIN reviews rv on rv.restaurantId = rs.id AND rv.published = 1
      GROUP BY rs.id;";
      return _db.Query<Restaurant>(sql).ToList();
      //   NOTE remember to invoke to List       ^
    }

    internal Restaurant GetById(int id)
    {
      string sql = @"
      SELECT
        rs.*,
      AVG(rv.rating) AS AverageRating,
      COUNT(rv.id) AS TotalReviews
      FROM restaurants rs
      JOIN reviews rv on rv.restaurantId = rs.id
      WHERE rs.id = @id;";
      return _db.Query<Restaurant>(sql, new { id }).FirstOrDefault();
      //   NOTE remember to invoke to FirstOrDefault              ^
    }

    internal Restaurant Create(Restaurant data)
    {
      string sql = @"
      INSERT INTO restaurants
      (name, address, category, picture, creatorId)
      VALUES
      (@Name, @Address, @Category, @Picture, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      //NOTE invoke last insert  ^
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
      //NOTE Don't forget to attach the id, if your id's are comming back 0, you probably forgot one of these two notes.
    }

    internal Restaurant Update(Restaurant original)
    {
      string sql = @"
      UPDATE restaurants
      SET
      name = @Name,
      address = @Address,
      category = @Category,
      picture = @Picture
      WHERE id = @Id;
      ";
      int rows = _db.Execute(sql, original);
      //   NOTE this below is just a redundancy check. not necessary could just return original at this point.
      if (rows > 0)
      {
        return original;
      }
      throw new Exception("Sql error on update restaurants, no rows affected");
    }

    internal string Remove(int id)
    {
      string sql = @"
      DELETE FROM restaurants WHERE id = @id LIMIT 1;
      ";
      int rows = _db.Execute(sql, new { id });
      if (rows > 0)
      {
        return "Restaurant Delorted";
      }
      throw new Exception("Sql error on remove restaurants, no rows affected");
    }
  }
}