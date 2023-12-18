using Microsoft.AspNetCore.Http;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IFileService
{

    Task<string> UploadFileAsync(IFormFile file);


    Task<IEnumerable<string>> UploadFilesAsync(IFormFileCollection files);


    Task<bool> DeleteFileAsync(string filePath);


    Task<bool> DeleteFilesAsync(IEnumerable<string> filePaths);



    Task<string> ReadEmailTemplate(string templateName);

}
