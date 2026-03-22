namespace MessageApi.Application;

public record NewUserData
{
   public required string UserName { get; set; }
   public required string Password { get; set; }
   public required string EmailAddress { get; set; }
   public required string FirstName { get; set; }
   public required string Surname { get; set; }
   public required DateTime Dob { get; set; }
   public required string Gender { get; set; }
}