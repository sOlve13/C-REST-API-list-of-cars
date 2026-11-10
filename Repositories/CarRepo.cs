using TestApi.Api.Cars;
using TestApi.Api.Dtos;
using System.Text.Json;
using Microsoft.Extensions.FileSystemGlobbing;
using TestApi.Api.Models;
using System.IO;


namespace TestApi.Api.Models
{
        
    public interface ICarRepo
    {
        public List<CarModel> LoadCars(string filePath);
        public void SaveCars(string filePath, List<CarModel> cars);


    }


public class CarRepo : ICarRepo

{

    public List<CarModel> LoadCars(string filePath)

        {
            if (!File.Exists(filePath))
                return new List<CarModel>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<CarModel>>(json) ?? new List<CarModel>();
        }


    public void SaveCars(string filePath, List<CarModel> cars)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(cars));
        }


}
}
