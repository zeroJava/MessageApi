namespace MessageApi.Domain;

public enum QueryFailReason
{
   PrimaryKeyDoesNotMatch = 1,
   UniqueColumnDoesNotMatch = 2,
   CouldNotFindRow = 3,
}