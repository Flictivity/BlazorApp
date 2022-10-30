namespace AuthorizationWebApp.Data.FileSystemService;

public interface IFileSystemService
{
    public void SaveImage(string fileName, string path);
    public Task SaveImage(Stream fs, string fileName);
    public void LoadImage(string fileName);
    public void GetImagesToFolder();

    public List<string> GetAllFiles();
}