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
        public static List<Pet> pets { get; set; } = new List<Pet>();
        public static List<PetOwner> petOwners { get; set; } = new List<PetOwner>();
        public static List<PetCareTaker> petCareTakers { get; set; } = new List<PetCareTaker>();
        public static List<PetAttraction> petAttractions { get; set; } = new List<PetAttraction>();


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

            if (petAttractions.Any(x => x.PetId == petAttraction.PetId && x.PetCareTakerId == petAttraction.PetCareTakerId))
            {
                return BadRequest("Pet already has an attraction with this Pet Care Taker.");
            }

            if (petAttractions.Count(x => x.PetId == petAttraction.PetId) >= 3)
            {
                return BadRequest("Pet can only have 3 attractions.");
            }

            if (petAttractions.Count(x => x.PetCareTakerId == petAttraction.PetCareTakerId) >= 3)
            {
                return BadRequest("Pet Care Taker can only have 3 attractions.");
            }

            petAttractions.Add(new PetAttraction
            {
                Id = petAttractions.Count + 1,
                PetId = petAttraction.PetId,
                PetCareTakerId = petAttraction.PetCareTakerId,
                AttractionDate = DateTime.Now
            });
            return Ok("Your attraction has been successfully established!");

        }


        [HttpGet("GetAllPetAttractions")]
        public IActionResult GetAllPetAttractions()
        {
            return Ok(petAttractions);
        }


        [HttpGet("GetPetAttractionsByPetId/{petId}")]
        public IActionResult GetPetAttractionsByPetId(int petId)
        {
            var petAttraction = petAttractions.Where(x => x.PetId == petId).ToList();
            if (petAttraction.Count == 0)
            {
                return NotFound("No attractions found for this Pet.");
            }
            return Ok(petAttraction);
        }


        [HttpGet("GetCareTakerInformationByPetId/{petId}")]
        public IActionResult GetCareTakerInformationByPetId(int petId)
        {
            var petAttraction = petAttractions.Where(x => x.PetId == petId).ToList();
            if (petAttraction.Count == 0)
            {
                return NotFound("No attractions found for this Pet.");
            }

            var careTakerIds = petAttraction.Select(x => x.PetCareTakerId).Distinct().ToList();
            var careTakers = petCareTakers.Where(x => careTakerIds.Contains(x.Id)).ToList();

            return Ok(careTakers);
        }


        [HttpGet("GetPetInformationByPetId/{petId}")]
        public IActionResult GetPetInformationByPetId(int petId)
        {
            var pet = pets.FirstOrDefault(x => x.Id == petId);
            if (pet == null)
            {
                return NotFound("No Pet found with this Id.");
            }
            return Ok(pet);
        }


        [HttpGet("GetPetTypeByPetTypeId/{petTypeId}")]
        public IActionResult GetPetTypeByPetTypeId(int petTypeId)
        {
            if (Enum.IsDefined(typeof(PetType), petTypeId))
            {
                var petType = (PetType)petTypeId;
                return Ok(petType.ToString());
            }
            else
            {
                return NotFound("No Pet Type found with this Id.");
            }
        }

    }
}
