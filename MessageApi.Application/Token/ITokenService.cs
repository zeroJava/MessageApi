namespace MessageApi.Application;

public interface ITokenService
{
   string GenerateToken(string userId, string role);
}