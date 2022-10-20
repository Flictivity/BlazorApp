using BlazorApp.Model;

namespace BlazorApp.Services;

public class UserService
{
    private static readonly string[] Cityes = new[]
    {
        "Moscow", "Kazan", "Rio", "Tokyo", "New York", "Saint Petersburg", "Washington"
    };
    
    private static readonly string[] FNames = new[]
    {
        "Ivan", "Bulat", "Nikita", "Roman", "Kamil", "Sergey", "Riyaz"
    };
    
    private static readonly string[] LNames = new[]
    {
        "Ivanov", "Saifullin", "Petrov", "Vasiliev", "Sharipov", "Turushkin", "Zaripov"
    };

    public static List<User> GetUsersList()
    {
        var random = new Random();
        return Enumerable.Range(1, random.Next(0,6)).Select(index => new User()
        {
            FName = FNames[Random.Shared.Next(FNames.Length)],
            LName = LNames[Random.Shared.Next(LNames.Length)],
            Age = Random.Shared.Next(15, 55),
            City = Cityes[Random.Shared.Next(Cityes.Length)]
        }).ToList();
    }
}