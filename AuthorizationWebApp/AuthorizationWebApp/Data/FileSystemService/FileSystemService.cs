using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace AuthorizationWebApp.Data.FileSystemService;

public class FileSystemService : IFileSystemService
{
    private static readonly MongoClient Client = new MongoClient("mongodb://localhost");
    private static readonly IMongoDatabase Database = Client.GetDatabase("ImagesTest");
    private static readonly GridFSBucket? GridFs = new GridFSBucket(Database);
    
    public void SaveImage(string fileName, string path)
    {
        using (var fs = new FileStream(path, FileMode.Open))
        {
            GridFs?.UploadFromStream(fileName, fs);
        }
    }

    public void LoadImage(string fileName)
    {
        using (var fs = new FileStream(
                   $"{Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"/wwwroot/images/")}{fileName}",
                   FileMode.CreateNew))
        {
            GridFs?.DownloadToStreamByName(fileName, fs);
        }
    }

    public void GetImagesToFolder()
    {
        AddImagesToDb();
        var images = GridFs!.Find(new BsonDocument()).ToList();

        foreach (var image in images)
        {
            using (FileStream fs = new FileStream($"{Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/wwwroot/images/")}{image.Filename}", FileMode.OpenOrCreate))
            {
                GridFs.DownloadToStreamByName(image.Filename, fs);
            }
        }
    }

    public List<string> GetAllFiles()
    {
        var files = GridFs!.Find(new BsonDocument()).ToList();
        var images = new List<string>();
        
        foreach (var file in files)
        {
            images.Add(file.Filename);
        }

        return images;
    }

    public void SaveTestImage()
    {
        using (var fs = new FileStream(@"C:\Users\User\Downloads\testImage.jpg", FileMode.Open))
        {
            GridFs?.UploadFromStream("testImage.jpg", fs);
        }
    }

    private bool CheckFileExisting(string fileName)
    {
        return GridFs!.Find(FilterDefinition<GridFSFileInfo>.Empty).ToEnumerable().Any(x => x.Filename == fileName);
    }

    private void AddImagesToDb()
    {
        foreach (var file in Directory.GetFiles("wwwroot/images"))
        {
            if (!CheckFileExisting(Path.GetFileName(file)))
            {
                SaveImage(Path.GetFileName(file), file);
            }
        }
    }
}