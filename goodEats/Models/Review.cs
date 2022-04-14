using System;

namespace goodEats.Models
{
  public class Review
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Body { get; set; }
    public int Rating { get; set; }
    public int RestaurantId { get; set; }
    public bool Published { get; set; }
    public string? RestaurantName { get; set; }

    public Profile? Creator { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }

}