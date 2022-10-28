namespace AuthorizationWebApp.Data.FileSystemService;

public interface IFileSystemService
{
    public void SaveImage(string fileName, string path);
    public void LoadImage(string fileName);
    public void GetImagesToFolder();

    public List<string> GetAllFiles();
    public void SaveTestImage();
}