using Mapster;
using Microsoft.EntityFrameworkCore;
using RealState.Abstactions;
using RealState.Api.Contracts;
using RealState.Contracts.Property;
using RealState.Entities.PropertForRent;
using RealState.Entities.PropertForSell;
using RealState.Errors;
using RealState.Persistence;

namespace RealState.Services;

public class PropertyService(ApplicationDbContext context,IFileService fileService) : IPropertyService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IFileService _fileService = fileService;

    public async Task<Result<PropertyForSellResponse>> AddPropertyForSell(PropertyForSellRequest propertyDto,UploadImageRequest uploadImageRequest)
    {
        var property = propertyDto.Adapt<PropertForSell>();
        property.DateListed = DateTime.UtcNow;


        _context.propertForSells.Add(property);

        await _context.SaveChangesAsync();
        await _fileService.UploadManyAsync(uploadImageRequest.Image, property.Id, "Sell");

        var result = property.Adapt<PropertyForSellResponse>();
        var images = await _context.uploadedFiles
           .Where(f => f.PropertyId == property.Id && f.PropertyType == "Sell")
           .Select(f => Path.Combine("/uploads", f.StoredFileName)) 
           .ToListAsync();

        result.Images = images;
        return Result.Success(result);
    }


    public async Task<Result<PropertyForRentResponse>> AddPropertyForRent(PropertyForRentRequest propertyDto, UploadImageRequest uploadImageRequest)
    {
        var property = propertyDto.Adapt<PropertForRent>();
        property.DateListed = DateTime.UtcNow;

        _context.propertForRents.Add(property);
        await _context.SaveChangesAsync();

        var result = property.Adapt<PropertyForRentResponse>();
        var images = await _context.uploadedFiles
          .Where(f => f.PropertyId == property.Id && f.PropertyType == "Rent")
          .Select(f => Path.Combine("/uploads", f.StoredFileName))
          .ToListAsync();

        result.Images = images;
        return Result.Success(result);
    }


    public async Task<Result> DeletePropertyForSell(int id)
    {
        var prop = await _context.propertForSells.FindAsync(id);
        if (prop is null)
            return Result.Failure(PropertyErrors.PropertyNotFound);
        var images = await _context.uploadedFiles
         .Where(f => f.PropertyId == prop.Id && f.PropertyType == "Sell").ToListAsync();

  
        _context.propertForSells.Remove(prop);
        _context.uploadedFiles.RemoveRange(images);
        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeletePropertyForRent(int id)
    {
        var prop = await _context.propertForRents.FindAsync(id);
        if (prop is null)
            return Result.Failure(PropertyErrors.PropertyNotFound);
        var images = await _context.uploadedFiles
 .Where(f => f.PropertyId == prop.Id && f.PropertyType == "Rent").ToListAsync();

        _context.propertForRents.Remove(prop);
        _context.uploadedFiles.RemoveRange(images);

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<PropertyForSellResponse>> GetPropertyForSell(int id)
    {
        var prop = await _context.propertForSells.FindAsync(id);
        if (prop is null)
            return Result.Failure<PropertyForSellResponse>(PropertyErrors.PropertyNotFound);

        var images = await _context.uploadedFiles
  .Where(f => f.PropertyId == prop.Id && f.PropertyType == "Sell")
  .Select(f => Path.Combine("/uploads", f.StoredFileName))
  .ToListAsync();

      
        var result = prop.Adapt<PropertyForSellResponse>();
        result.Images = images;
        return Result.Success(result);
    }

    public async Task<Result<PropertyForRentResponse>> GetPropertyForRent(int id)
    {
        var prop = await _context.propertForRents.FindAsync(id);
        if (prop is null)
            return Result.Failure<PropertyForRentResponse>(PropertyErrors.PropertyNotFound);
        var images = await _context.uploadedFiles
 .Where(f => f.PropertyId == prop.Id && f.PropertyType == "Rent")
 .Select(f => Path.Combine("/uploads", f.StoredFileName))
 .ToListAsync();
        var result = prop.Adapt<PropertyForRentResponse>();
        result.Images = images;

        return Result.Success(result);
    }

    public async Task<Result<List<PropertyForSellHomePageResponse>>> GetAllPropertiesForSell()
    {
        var props = await _context.propertForSells
            .ProjectToType<PropertyForSellHomePageResponse>()
            .ToListAsync();
        var propertyIds = props.Select(p => p.Id).ToList();

        var images = await _context.uploadedFiles
       .Where(f => propertyIds.Contains(f.PropertyId) && f.PropertyType == "Sell")
       .Select(f => new
       {
           f.PropertyId,
           ImagePath = Path.Combine("/uploads", f.StoredFileName)
       })
       .ToListAsync();
        foreach (var property in props)
        {
            property.Images = images
                .Where(img => img.PropertyId == property.Id)
                .Select(img => img.ImagePath)
                .ToList();
        }
        return Result.Success(props);
    }

    public async Task<Result<List<PropertyForSellHomePageResponse>>> GetAllPropertiesForRent()
    {
        var props = await _context.propertForRents
            .ProjectToType<PropertyForSellHomePageResponse>()
            .ToListAsync();


        var propertyIds = props.Select(p => p.Id).ToList();

        var images = await _context.uploadedFiles
     .Where(f => propertyIds.Contains(f.PropertyId) && f.PropertyType == "Rent")
     .Select(f => new
     {
         f.PropertyId,
         ImagePath = Path.Combine("/uploads", f.StoredFileName)
     })
     .ToListAsync();
        foreach (var property in props)
        {
            property.Images = images
                .Where(img => img.PropertyId == property.Id)
                .Select(img => img.ImagePath)
                .ToList();
        }
        return Result.Success(props);
    }

    public async Task<Result<PropertyForSellResponse>> EditPropertyForSell(int id, PropertyForSellRequest propertyDto, UploadImageRequest uploadImageRequest)
    {
        var property = await _context.propertForSells.FindAsync(id);
        if (property is null)
            return Result.Failure<PropertyForSellResponse>(PropertyErrors.PropertyNotFound);


        property = propertyDto.Adapt(property);
        property.DateListed = DateTime.UtcNow;


        if (uploadImageRequest.Image?.Any() == true)
        {
  
            var oldImages = await _context.uploadedFiles
                .Where(f => f.PropertyId == property.Id && f.PropertyType == "Sell")
                .ToListAsync();
            _context.uploadedFiles.RemoveRange(oldImages);


            await _fileService.UploadManyAsync(uploadImageRequest.Image, property.Id, "Sell");
        }


        _context.propertForSells.Update(property);
        await _context.SaveChangesAsync();

        var result = property.Adapt<PropertyForSellResponse>();
        var images = await _context.uploadedFiles
            .Where(f => f.PropertyId == property.Id && f.PropertyType == "Sell")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToListAsync();

        result.Images = images;
        return Result.Success(result);
    }

    public async Task<Result<PropertyForRentResponse>> EditPropertyForRent(int id, PropertyForRentRequest propertyDto, UploadImageRequest uploadImageRequest)
    {
        var property = await _context.propertForRents.FindAsync(id);
        if (property is null)
            return Result.Failure<PropertyForRentResponse>(PropertyErrors.PropertyNotFound);

        property = propertyDto.Adapt(property);
        property.DateListed = DateTime.UtcNow;


        if (uploadImageRequest.Image?.Any() == true)
        {
            var oldImages = await _context.uploadedFiles
                .Where(f => f.PropertyId == property.Id && f.PropertyType == "Rent")
                .ToListAsync();
            _context.uploadedFiles.RemoveRange(oldImages);

            await _fileService.UploadManyAsync(uploadImageRequest.Image, property.Id, "Rent");
        }

        _context.propertForRents.Update(property);
        await _context.SaveChangesAsync();

        var result = property.Adapt<PropertyForRentResponse>();
        var images = await _context.uploadedFiles
            .Where(f => f.PropertyId == property.Id && f.PropertyType == "Rent")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToListAsync();

        result.Images = images;
        return Result.Success(result);
    }

}
