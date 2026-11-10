using Microsoft.AspNetCore.Mvc;
using TestApi.Api.Dtos;
using System.Text.Json;
using System.Reflection.Metadata.Ecma335;

namespace TestApi.Api.Cars
{
    [ApiController]
    [Route("car")]
    public class CarsController : ControllerBase
    {
       
       private ICarService _carservice;

       public CarsController(ICarService carService)
        {
            _carservice = carService;

        }

        [HttpGet("")]
        public ActionResult<List<CarDto>> GetCars()
        {   
            var carList = _carservice.GetAll();

            return Ok(carList);
        }

        [HttpGet("admin")]
        public ActionResult<List<CarDto>> GetAdminCars()
        {   
            var carListADmin = _carservice.GetAllADMIN();
            
            return Ok(carListADmin);
        }



        [HttpPost("addcar")]
        public ActionResult AddCar([FromBody]AddCarsDto car)
        {
            bool AddCarController = _carservice.AddCarTo(car);
                        
            return AddCarController == true ? Ok("Added") : Ok("Couldn't add the car to the list");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTheCar(int id)
        {   
            _carservice.DeleteById(id);

            return Ok();
        }


        [HttpPut("{id}")]
        public ActionResult UpdateCar(int id, [FromBody] UpdateCarDto car)
        {
            
            _carservice.UpdateById(id, car);

            return Ok();
        }


        [HttpGet("{id}")]
        public ActionResult GetByID(int id)
        {  
            CarDto car = _carservice.GetCarByID(id);
            return  car == null ? NotFound("Car was not found") : Ok(car);
        }

    }
}
