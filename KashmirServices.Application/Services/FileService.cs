using KashmirServices.Application.Abstractions.IServices;
using Microsoft.AspNetCore.Http;

namespace KashmirServices.Application.Services;

public class FileService : IFileService
{
    private readonly string webRootPath;

    public FileService(string webRootPath)
    {
        this.webRootPath = webRootPath;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        string path = GetPhysicalDirPath();


        if (!(Directory.Exists(path)))
            Directory.CreateDirectory(path);



        string fileName = string.Concat(Guid.NewGuid(), file.FileName);
        string fullPath = Path.Combine(path, fileName);


        using FileStream fs = new(fullPath, FileMode.Create);
        await file.CopyToAsync(fs);


        return Path.Combine(GetRelativeDirPath(), fileName);

    }


    public Task<IEnumerable<string>> UploadFilesAsync(IFormFileCollection files)
    {
        throw new NotImplementedException();
    }


    public async Task<bool> DeleteFileAsync(string filePath)
    {

        string fullPath = Path.Combine(webRootPath, filePath);

        if (!(File.Exists(fullPath))) return false;

        await Task.Run(() => File.Delete(fullPath));

        return true;

    }


    public Task<bool> DeleteFilesAsync(IEnumerable<string> filePaths)
    {
        throw new NotImplementedException();
    }


    public async Task<string> ReadEmailTemplate(string templateName)
    {
        string templatePath = Path.Combine(@"EmailTemplates", templateName);
        var res = File.Exists(templatePath);
        if (!File.Exists(templatePath))
            return string.Empty;

        return await Task.Run(() => File.ReadAllText(templatePath));

    }

    #region helper func
    private string GetPhysicalDirPath() => Path.Combine(webRootPath, "Files", "Images");


    private string GetRelativeDirPath() => Path.Combine("Files", "Images");


    #endregion
}
