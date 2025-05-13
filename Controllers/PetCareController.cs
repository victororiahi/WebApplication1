using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;
using WebApplication1.DTO;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetCareController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PetCareController(AppDbContext context)
        {
            _context = context;

        }


        [HttpPost("RegisterPet")]
        public IActionResult RegisterPet([FromBody] Pet pet)
        {
            if (pet == null)
            {
                return BadRequest("Pet cannot be null.");
            }

            pet.Id = Guid.NewGuid();
            _context.Pets.Add(pet);
            _context.SaveChanges();

            return Ok(pet);
        }



        [HttpGet("GetAllPets")]
        public IActionResult GetAllPets()
        {
            var allPets = _context.Pets.ToList();
            return Ok(allPets);
        }


        [HttpPost("RegisterPetOwner")]
        public IActionResult RegisterPetOwner([FromBody] PetOwner petOwner)
        {
            if (petOwner == null)
            {
                return BadRequest("Pet Owner cannot be null.");
            }

            petOwner.Id = Guid.NewGuid();
            _context.Owners.Add(petOwner);
            _context.SaveChanges();
            return Ok(petOwner);
        }



        [HttpGet("GetAllPetOwners")]
        public IActionResult GetAllPetOwners()
        {
            var allPetOwners = _context.Owners.ToList();
            return Ok(allPetOwners);
        }


        [HttpPost("RegisterPetCareTaker")]
        public IActionResult RegisterPetCareTaker([FromBody] PetCareTaker petCareTaker)
        {
            if (petCareTaker == null)
            {
                return BadRequest("Pet Care Taker cannot be null.");
            }

            petCareTaker.Id = Guid.NewGuid();
            _context.CareTakers.Add(petCareTaker);
            _context.SaveChanges();
            return Ok(petCareTaker);
        }


        [HttpGet("GetAllPetCareTakers")]
        public IActionResult GetAllPetCareTakers()
        {
            var allPetCareTakers = _context.CareTakers.ToList();
            return Ok(allPetCareTakers);
        }



        [HttpPost("PetAttraction")]
        public IActionResult PetAttraction([FromBody] PetAttractionDTO petAttraction)
        {

            if (petAttraction != null)
            {
                var pets = _context.Pets;
                var petCareTakers = _context.CareTakers;
                var petAttractions = _context.Attractions;


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
                    Id = Guid.NewGuid(),
                    PetId = petAttraction.PetId,
                    PetCareTakerId = petAttraction.PetCareTakerId,
                    AttractionDate = DateTime.Now
                });
                _context.SaveChanges();
                return Ok("Your attraction has been successfully established!");
            }

            return BadRequest("Pet Attraction cannot be null.");

        }


        [HttpGet("GetAllPetAttractions")]
        public IActionResult GetAllPetAttractions()
        {
            var allAttractions = _context.Attractions.ToList();
            return Ok(allAttractions);
        }


        [HttpGet("GetPetAttractionsByPetId/{petId}")]
        public IActionResult GetPetAttractionsByPetId(Guid petId)
        {
            var petAttraction = _context.Attractions.Where(x => x.PetId == petId).ToList();

            if (petAttraction.Count == 0)
            {
                return NotFound("No attractions found for this Pet.");
            }
            return Ok(petAttraction);
        }


        [HttpGet("GetCareTakerInformationByPetId/{petId}")]
        public IActionResult GetCareTakerInformationByPetId(Guid petId)
        {
            var petAttraction = _context.Attractions.Where(x => x.PetId == petId);

            if (petAttraction.Count() == 0)
            {
                return NotFound("No attractions found for this Pet.");
            }

            var careTakerIds = petAttraction.Select(x => x.PetCareTakerId).Distinct().ToList();
            var careTakers = _context.CareTakers.Where(x => careTakerIds.Contains(x.Id)).ToList();

            return Ok(careTakers);
        }


        [HttpGet("GetPetInformationByPetId/{petId}")]
        public IActionResult GetPetInformationByPetId(Guid petId)
        {
            var pet = _context.Pets.FirstOrDefault(x => x.Id == petId);
            if (pet == null)
                return NotFound("No Pet found with this Id.");

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


        [HttpGet("GetAllBasicPetInfo")]
        public IActionResult GetAllBasicPetInfo()
        {
            List<PetResponseDTO> petDTO = new List<PetResponseDTO>();
            var pets = _context.Pets.ToList();
            foreach (var pet in pets)
            {
                PetResponseDTO petResponseDTO = new PetResponseDTO();
                petResponseDTO.Name = pet.Name;
                petResponseDTO.Description = pet.Description;
                petResponseDTO.ResponseDate = DateTime.Now;
                petDTO.Add(petResponseDTO);
            }

            return Ok(petDTO);
        }


        [HttpGet("GetAllBasicPetInfo2")]
        public IActionResult GetAllBasicPetInfo2()
        {
            List<PetResponseDTO> petDTO = new List<PetResponseDTO>();
            var pets = _context.Pets.ToList();

            pets.ForEach(pet =>
        {
            petDTO.Add(new PetResponseDTO
            {
                Name = pet.Name,
                Description = pet.Description,
                ResponseDate = DateTime.Now
            });
        });

            return Ok(petDTO);
        }


        [HttpGet("GetAllBasicPetInfo3")]
        public IActionResult GetAllBasicPetInfo3()
        {
            var pets = _context.Pets;
            List<PetResponseDTO> petDTO =
        pets.Select(pet =>
         new PetResponseDTO
         {
             Name = pet.Name,
             Description = pet.Description,
             ResponseDate = DateTime.Now
         }
        ).ToList();

            return Ok(petDTO);
        }


        [HttpGet("GetAllBasicPetInfo4")]
        public IActionResult GetAllBasicPetInfo4()
        {
            var pets = _context.Pets;
            var j = pets.Select(pet => new { pet.Name, pet.Description, DateTime.Now }).ToList();

            return Ok(j);
        }
    }
}