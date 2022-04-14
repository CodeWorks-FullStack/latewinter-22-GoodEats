namespace goodEats.Models
{
  public class Restaurant
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public string Picture { get; set; }
    public string CreatorId { get; set; }
  }
}