namespace MessageApi.Domain;

/* Datacontract attributes only work with classes. Using these attributes
* wcf can serialise the class, and send the data over the internet.
* 
* The Datacontract attribute to used to map the class that is going to be
* serialised, were as the DataMember attribute is used to map the property
* that are also going to be serialised.
* */

public class User
{
   /* Please note that the name assignment to each DataMember attribute must be unique,
       * so something like:
       *      [DataMember(Name = "Id")] // same name assignment
       *      public long Id { get; set; }
       *      
       *      [DataMember(Name = "Id")] // same name assignment
       *      public string UserName { get; set; }
       *      
       * Beacause both share the same name, but are different properties, wcf get confused, and
       * throws an exception saying that one of the property is not accessible .i.e. not serialiseable.
       * */

   public required long Id { get; set; }
   public required string UserName { get; set; }
   public required string Password { get; set; }
   public required string EmailAddress { get; set; }
   public required string FirstName { get; set; }
   public required string Surname { get; set; }
   public required DateTime DOB { get; set; }
   public required string Gender { get; set; }
   public List<Message> Messages { get; set; }

   public User()
   {
      Messages = new();
   }
}