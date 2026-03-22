using MessageApi.Domain;

namespace MessageApi.Application;

internal class UserMapper : MapperBase<User, UserDto>
{
   public override UserDto? Map(User? entity)
   {
      if (entity == null)
      {
         return null;
      }
      return new UserDto()
      {
         Id = entity.Id,
         UserName = entity.UserName,
      };
   }
}