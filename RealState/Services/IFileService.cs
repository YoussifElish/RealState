using RealState.Contracts.Launch;
using RealState.Entities;

namespace RealState.Services;

public interface IFileService
{

    Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection images, int propertyId, string propertyType, string Description, CancellationToken cancellationToken = default);
    Task<List<UploadedFile>> UploadLaunchImagesAsync(LaunchUploadImageRequest uploadImageRequest, int launchId, CancellationToken cancellationToken = default);


}