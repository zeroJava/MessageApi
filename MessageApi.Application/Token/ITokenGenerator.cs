namespace MessageApi.Application;

public interface ITokenGenerator
{
   string GenerateToken(string userId, string role);
}