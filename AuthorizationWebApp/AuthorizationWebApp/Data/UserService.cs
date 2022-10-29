using AuthorizationWebApp.Data.LocalStorageService;
using MongoDB.Driver;

namespace AuthorizationWebApp.Data;

public class UserService
{
    public User CurrentUser = new User();

    private readonly ILocalStorageService _localStorageService;
    private static readonly MongoClient Client = new MongoClient("mongodb://localhost");
    private static readonly IMongoDatabase Db = Client.GetDatabase("UsersDb");
    private readonly IMongoCollection<User> _usersCollection = Db.GetCollection<User>("Users");

    public UserService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public bool SaveUser(User user)
    {
        try
        {
            _usersCollection.InsertOne(user);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public User UserLogin(string login, string password)
    {
        try
        {
            CurrentUser = _usersCollection.Find(x => x.Login == login &&
                                                     x.Password == password).FirstOrDefault();
            _localStorageService.SetAsync<User>("Authorization", CurrentUser);
        }
        catch
        {
            // ignored
        }

        return CurrentUser;
    }
}