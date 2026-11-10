namespace TestApi.Api.Dtos;


public record CarDto
{
    public int Id { get; init; }
    public string Make { get; init; }
    public string Model { get; init; }
    public int Year { get; init; }
    public string Color { get; init; }
    public decimal Price { get; init; }

}