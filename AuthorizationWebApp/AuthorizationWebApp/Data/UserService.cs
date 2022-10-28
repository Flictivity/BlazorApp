using MongoDB.Driver;

namespace AuthorizationWebApp.Data;

public class UserService
{
    public User CurrentUser = new User();
    
    private static readonly MongoClient Client = new MongoClient("mongodb://localhost");
    private static readonly IMongoDatabase Db = Client.GetDatabase("UsersDb");
    private readonly IMongoCollection<User> _usersCollection = Db.GetCollection<User>("Users");
    
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

    public User UserLogin(string login)
    {
        try
        {
            CurrentUser = _usersCollection.Find(x => x.Login == login).FirstOrDefault();
        }
        catch
        {
            // ignored
        }

        return CurrentUser;
    }
}