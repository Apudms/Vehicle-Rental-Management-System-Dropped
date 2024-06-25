using Microsoft.AspNetCore.Mvc;
using VehicleRentalApi.Contracts;
using VehicleRentalApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRental _rental;

        public RentalsController(IRental rental)
        {
            _rental = rental;
        }

        // GET: api/<RentalsController>
        [HttpGet]
        public IEnumerable<Rental> Get()
        {
            var rentals = _rental.GetAll();
            return rentals;
        }

        // GET api/<RentalsController>/5
        [HttpGet("{id}")]
        public Rental Get(int id)
        {
            var rental = _rental.GetById(id); // ?ID Rental string
            return rental;
        }

        //// POST api/<RentalsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RentalsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RentalsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
