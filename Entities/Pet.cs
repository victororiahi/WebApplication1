﻿namespace WebApplication1.Entities
{
    public class Pet : IEntity
    {
        public PetType PetType { get; set; } 
        public int Id { get; set; }
         public string Name { get; set; }
        public string Description { get; set; }

        public string Breed { get; set; }

        public int Age { get; set; }
        public string HealthProfile { get; set; }
    }


    public enum PetType
    {
        Dog,
        Cat,
        Rabbit, 
        Hamster,
        Snake
    }
}
