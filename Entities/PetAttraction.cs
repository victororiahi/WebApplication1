namespace WebApplication1.Entities
{
    public class PetAttraction
    {
        public Guid PetId { get; set; }
        public Guid PetCareTakerId { get; set; }
        public DateTime AttractionDate { get; set; }
        public Guid Id { get; set;}
    }
}
