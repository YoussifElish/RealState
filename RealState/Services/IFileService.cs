namespace RealState.Services;

public interface IFileService
{

    Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection images, int propertyId, string PropertyType,CancellationToken cancellationToken = default);
    
}