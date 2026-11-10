using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestApi.Api.Cars;
using TestApi.Api.Dtos;
using TestApi.Api.Models;



namespace TestApi.Api.Cars
{

    public interface ICarService
    {
        
        public List<CarDto> GetAll();
        public CarDto GetCarByID(int id);
        public bool UpdateById(int id, UpdateCarDto car);
        public bool DeleteById(int id);
        public bool AddCarTo(AddCarsDto car);
        public List<CarModel> GetAllADMIN();

    }

public class CarService : ICarService
    {
        
        private string _filePath; 
        private ICarRepo _carrepo;

        private double Taxcalc(int price)
        {
            return price*1.23 + 0.12*price;
        }

        public CarService(IOptions<FilePath> options, ICarRepo carRepo)
        {
            _filePath = options.Value.CarPath;
            _carrepo = carRepo;

        }


        
        int IDGenerator(List<CarModel> cars)
        {
           return cars.Count == 0 ? 0 : cars.Max(c => c.Id) + 1;;
        }

        public List<CarModel> GetAllADMIN()
        {
            List<CarModel> AdminCars = _carrepo.LoadCars(_filePath);
            
            return AdminCars;
        }

        public List<CarDto> GetAll()
        {
            List<CarModel> cars = _carrepo.LoadCars(_filePath);
            List<CarDto> carsToShow = new List<CarDto>{};
            foreach(CarModel i in cars)
            {
                carsToShow.Add(new CarDto
                {
                    Id = i.Id,
                    Make = i.Make,
                    Model = i.Model,
                    Year = i.Year,
                    Color = i.Color,
                    Price = i.Price
                });
            }

            return carsToShow;
        }

        public CarDto GetCarByID(int id)
        {
            List<CarModel> carList = _carrepo.LoadCars(_filePath);
            CarModel car = carList.Find(cars => cars.Id == id);
            if(car == null){return null;}
            CarDto foundCar = new CarDto
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price

        };


            return foundCar;
        }
    


        public bool AddCarTo(AddCarsDto car)
        {
            List<CarModel> CarList = _carrepo.LoadCars(_filePath);
            Random rn = new Random();
            int PriceNum = rn.Next(1000, 2000);
            CarModel addedCar = new CarModel
            {
                Id = IDGenerator(CarList),
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Price = PriceNum,
                PriceAfterTaxes = Taxcalc((int)PriceNum)
            };
            CarList.Add(addedCar);
            _carrepo.SaveCars(_filePath, CarList);

            return true;
        }


        public bool DeleteById(int id)
        {
            List<CarModel> CarList = _carrepo.LoadCars(_filePath);
            int index = CarList.FindIndex(car => car.Id == id);
            CarList.RemoveAt(index);
            _carrepo.SaveCars(_filePath,CarList);
            return true;
        }

        public bool UpdateById(int id, UpdateCarDto car)
        {
            List<CarModel> CarList = _carrepo.LoadCars(_filePath);
            int index = CarList.FindIndex(car => car.Id == id);
            CarList[index] = new CarModel
            {
                Id = CarList[index].Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                PriceAfterTaxes = Taxcalc((int)car.Price)
            };
            _carrepo.SaveCars(_filePath, CarList);
            return true;
        }

        


    }
}

 








//         public bool AddCarTo(AddCarsDto car)
//         {

//         var cars = LoadCarsAdmin(filePath);

//         int PriceNumber = new Random().Next(1000, 2000);
//         CarModel newCar = new CarModel
//         {
//             Id = IDGenerator(cars),
//             Make = car.Make,
//             Model = car.Model,
//             Year = car.Year,
//             Color = car.Color,
//             Price = PriceNumber,
//             PriceAfterTaxes = PriceNumber * 1.23
//         };

//         cars.Add(newCar);
//         SaveCars(_filePath, cars);
        
//         return true;

        

//         }








//     }

// }



















































































// UpdateCarDto AddCar(String filePath, AddCarsDto car)
//         {

//         cars = LoadCars(filePath);

//         CarDto newCar = new CarDto
//         {
//             Id = IDGenerator(cars),
//             Make = car.Make,
//             Model = car.Model,
//             Year = car.Year,
//             Color = car.Color,
//             Price = new Random().Next(1000, 2000)
//         };
//         cars.Add(newCar);
//         SaveCars(filePath, cars);

//         var returnCar = new UpdateCarDto
//         {
//             Make = newCar.Make,
//             Model = newCar.Model,
//             Year = newCar.Year,
//             Color = newCar.Color,
//             Price = newCar.Price
//         };
        
//         return returnCar;

//         }






//         int IDGenerator(List<CarDto> cars)
//         {
//            return cars.Count == 0 ? 0 : cars.Max(c => c.Id) + 1;;
//         }


















//         public List<CarModel> GetAll()
//         {
//             List<CarModel> temp = LoadCars(filePath);

//             return temp;
//         }

















//         public CarModel GetByID(int id)
//         {
//             cars = LoadCars(filePath)       int index = cars.FindIndex(car => car.Id == id);
//             if(index == -1){return NotFound("TESTING STRING");} 
//             return Ok(cars[index]);
//         }

//         public bool AddCarTo(AddCarsDto car)
//         {
//             UpdateCarDto returnResult = AddCar(car);
            
//             return true;
//         }


//         public UpdateCarDto UpdateById(int id, UpdateCarDto car)
//         {
            
//             cars = LoadCars(filePath)      var index = cars.FindIndex(car => car.Id == id);
//             if(index == -1){return NotFound();}
//             cars[index] = cars[index] with 
//             {
//                 Make = car.Make,
//                 Model = car.Model,
//                 Year = car.Year,
//                 Color = car.Color,
//                 Price = car.Price

//             };
//             SaveCars(cars);

//             return true;
//         }


//         public bool DeleteById(int id)
//         {
//             cars = LoadCars(filePath) var index = cars.FindIndex(car => car.Id == id);
//             if(index == -1){return NotFound();}
//             cars.RemoveAt(index);
//             SaveCars(cars);

//             return true;
//         }




//         public List<CarModel> GetAllwtithAdmin(CarModel)
//     {
//         Cars     


        
//     }






//     }


// }