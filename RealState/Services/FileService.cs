using RealState.Entities;
using RealState.Persistence;

namespace RealState.Services;

public class FileService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context) : IFileService
{
    private readonly string _filesPath = $"{webHostEnvironment.WebRootPath}/uploads";
    private readonly string _imagesPath = $"{webHostEnvironment.WebRootPath}/images";
    private readonly ApplicationDbContext _context = context;

   

    public async Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection images, int propertyId,string propertyType ,CancellationToken cancellationToken = default)
    {
        List<UploadedFile> uploadedFiles = [];

        foreach (var image in images)
        {
            var uploadedFile = await UploadImageAsync(image, cancellationToken);
            uploadedFile.PropertyType = propertyType;
            uploadedFile.PropertyId = propertyId;
            uploadedFiles.Add(uploadedFile);
        }

        await _context.AddRangeAsync(uploadedFiles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadedFiles.Select(x => x.Id).ToList();
    }

    private async Task<UploadedFile> UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default)
    {
        

        var uploadedFile = new UploadedFile
        {
            FileName = image.FileName,
            ContentType = image.ContentType,
            StoredFileName = image.FileName,
            FileExtension = Path.GetExtension(image.FileName)
        };

        var path = Path.Combine(_filesPath, image.FileName);

        using var stream = File.Create(path);
        await image.CopyToAsync(stream, cancellationToken);

        return uploadedFile;
    }
}