using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetCareController : ControllerBase
    {
        static List<Pet> pets = new List<Pet>();
        static List<PetOwner> petOwners = new List<PetOwner>();
        static List<PetCareTaker> petCareTakers = new List<PetCareTaker>();
        static List<PetAttraction> petAttractions = new List<PetAttraction>();

        [HttpPost("RegisterPet")]
        public IActionResult RegisterPet([FromBody] Pet pet)
        {
            PetCareController.pets.Add(pet);
            return Ok(pet);
        }

        [HttpGet("GetAllPets")]
        public IActionResult GetAllPets()
        {
            return Ok(pets);
        }

        [HttpPost("RegisterPetOwner")]
        public IActionResult RegisterPetOwner([FromBody] PetOwner petOwner)
        {
            PetCareController.petOwners.Add(petOwner);
            return Ok(petOwner);
        }

        [HttpGet("GetAllPetOwners")]
        public IActionResult GetAllPetOwners()
        {
            return Ok(petOwners);
        }
        [HttpPost("RegisterPetCareTaker")]
        public IActionResult RegisterPetCareTaker([FromBody] PetCareTaker petCareTaker)
        {
            PetCareController.petCareTakers.Add(petCareTaker);
            return Ok(petCareTaker);
        }

        [HttpGet("GetAllPetCareTakers")]
        public IActionResult GetAllPetCareTakers()
        {
            return Ok(petCareTakers);
        }

        [HttpPost("PetAttraction")]
        public IActionResult PetAttraction([FromBody] PetAttractionDTO petAttraction)
        {
            if (!pets.Any(x => x.Id == petAttraction.PetId))
            {
                return BadRequest("Pet with given Id not found.");
            }
            if (!petCareTakers.Any(x => x.Id == petAttraction.PetCareTakerId))
            {
                return BadRequest("Pet Care Taker doesn't exist.");
            }
            return Ok(petAttraction);

        }
    }
}
