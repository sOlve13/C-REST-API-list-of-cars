using System.Security.Cryptography.X509Certificates;

namespace TestApi.Api.Dtos{
public record AddCarsDto{

    public string Make { get; init; }
    public string Model { get; init; }
    public int Year { get; init; }
    public string Color { get; init; }

}

}