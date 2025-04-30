namespace WebApplication1.Entities
{
    public class PetAttraction: IEntity
    {
        public int PetId { get; set; }
        public int PetCareTakerId { get; set; }
        public DateTime AttractionDate { get; set; }
        public int Id { get; set;}
    }
}
