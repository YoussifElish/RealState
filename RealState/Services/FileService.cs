using RealState.Contracts.Launch;
using RealState.Entities;
using RealState.Persistence;

namespace RealState.Services;

public class FileService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context) : IFileService
{
    private readonly string _filesPath = $"{webHostEnvironment.WebRootPath}/uploads";
    private readonly string _imagesPath = $"{webHostEnvironment.WebRootPath}/images";
    private readonly ApplicationDbContext _context = context;

   

    public async Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection images, int propertyId,string propertyType,string Description ,CancellationToken cancellationToken = default)
    {
        List<UploadedFile> uploadedFiles = [];

        foreach (var image in images)
        {
            var uploadedFile = await UploadImageAsync(image, cancellationToken);
            uploadedFile.PropertyType = propertyType;
            uploadedFile.PropertyId = propertyId;
            uploadedFile.Description = Description;
            uploadedFiles.Add(uploadedFile);
        }

        await _context.AddRangeAsync(uploadedFiles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadedFiles.Select(x => x.Id).ToList();
    }

    public async Task<List<UploadedFile>> UploadLaunchImagesAsync(LaunchUploadImageRequest uploadImageRequest, int launchId, CancellationToken cancellationToken = default)
    {
        List<UploadedFile> uploadedFiles = [];

        if (uploadImageRequest.BannerImage != null)
        {
            var banner = await UploadSingleImageAsync(uploadImageRequest.BannerImage, "Launch" ,"Banner", launchId, cancellationToken);
            uploadedFiles.Add(banner);
        }

        if (uploadImageRequest.MasterPlanImage != null)
        {
            var masterPlan = await UploadSingleImageAsync(uploadImageRequest.MasterPlanImage, "Launch","MasterPlan", launchId, cancellationToken);
            uploadedFiles.Add(masterPlan);
        }

        if (uploadImageRequest.LocationImage != null)
        {
            var location = await UploadSingleImageAsync(uploadImageRequest.LocationImage, "Launch", "Location", launchId, cancellationToken);
            uploadedFiles.Add(location);
        }

        if (uploadImageRequest.PaymentPlanImage != null)
        {
            var paymentPlan = await UploadSingleImageAsync(uploadImageRequest.PaymentPlanImage, "Launch","PaymentPlan", launchId, cancellationToken);
            uploadedFiles.Add(paymentPlan);
        }

        await _context.uploadedFiles.AddRangeAsync(uploadedFiles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadedFiles;
    }

    private async Task<UploadedFile> UploadSingleImageAsync(IFormFile image, string launchImageType, string description, int launchId, CancellationToken cancellationToken = default)
    {
        var uploadedFile = new UploadedFile
        {
            FileName = image.FileName,
            ContentType = image.ContentType,
            StoredFileName = image.FileName,
            FileExtension = Path.GetExtension(image.FileName),
            PropertyType = launchImageType, 
            PropertyId = launchId,
            Description = description
        };

        var path = Path.Combine(_filesPath, image.FileName);

        using var stream = File.Create(path);
        await image.CopyToAsync(stream, cancellationToken);

        return uploadedFile;
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