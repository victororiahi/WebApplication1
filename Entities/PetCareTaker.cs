namespace WebApplication1.Entities
{
    public class PetCareTaker
    {
        public Guid Id { get; set; }

        public Experience Experience { get; set; }
        public string Description { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string FirstName { get ; set ; }
        public string LastName { get ; set ; }
    }


    public enum Experience
    {
        Beginner,
        Amateur,
        Experienced
    }
}
