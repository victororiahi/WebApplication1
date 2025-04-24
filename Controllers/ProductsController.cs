using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {  
       static List<Person> persons = new List<Person>();

        [HttpGet("GetMyName")]
        public string GetMyName()
        {
            return "Felix";
        }

        [HttpGet("GetProductName")]
        public string GetProductName(string product)
        {
            return $"The product name is {product}";
        }
        //Route Parameters technique
        [HttpGet("MultipyNumbers/{num1}/{num2}")]
        public int MultiplyNumbers([FromRoute] int num1,[FromRoute] int num2)
        {
            return num1 * num2;
        }
        //Query Parameters technique
        [HttpGet("MultipyNumbers")]
        public int MultiplyNumbers2( int num1, int num2)
        {
            return num1 * num2;
        }
        //Body Parameters technique
        //HTTP request contains a header and a body 
        [HttpPost("SavePerson")]
        public string SavePerson([FromBody]Person identity)
        {
            ProductsController.persons.Add(identity);
            return "The Person has been successfully added.";
        }
        [HttpGet("ListOfPersons")]
        public List<Person> GetAllPersons()
        {
            return ProductsController.persons;
        }
    } 
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }

        
    }
} 
// REST APIs are a HTTP standard for creating APIs using the following verbs :
//GET - Used to retrieve data 
//POST - Used for creating data
//PUT - Used for updating data 
//DELETE -Used for removing data 

