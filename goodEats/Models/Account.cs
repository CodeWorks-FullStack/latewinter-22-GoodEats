namespace goodEats.Models
{
  // NOTE REMEMBER account inherits the profile cause the account has extra information
  public class Account : Profile
  {
    public string Email { get; set; }
  }
}