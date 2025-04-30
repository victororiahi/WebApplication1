namespace WebApplication1.Entities
{
    public class PetOwner : IEntity, IUser
    {
        public int Id { get ; set ; }
        public string FirstName { get ; set ; }
        public string LastName { get ; set ; }
    }
}
