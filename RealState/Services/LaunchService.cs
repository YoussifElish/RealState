using Mapster;
using Microsoft.EntityFrameworkCore;
using RealState.Abstactions;
using RealState.Api.Contracts;
using RealState.Contracts.Launch;
using RealState.Contracts.Property;
using RealState.Entities;
using RealState.Errors;
using RealState.Persistence;

namespace RealState.Services;

public class LaunchService(ApplicationDbContext context,IFileService fileService) : ILaunchService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IFileService _fileService = fileService;

    public async Task<Result<LaunchResponse>> AddLaunchAsync(LaunchRequest launchRequest, LaunchUploadImageRequest uploadImageRequest)
    {
        var launch = launchRequest.Adapt<Launch>();

        await _context.launches.AddAsync(launch);
        await _context.SaveChangesAsync(); 

        await _fileService.UploadLaunchImagesAsync(uploadImageRequest,launch.Id);

        var launchResponse = launch.Adapt<LaunchResponse>();

        var allImages = await _context.uploadedFiles
            .Where(f => f.PropertyId == launch.Id && f.PropertyType.StartsWith("Launch"))
            .ToListAsync();

        launchResponse.BannerImages = allImages
            .Where(f => f.PropertyType == "Launch-Banner")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToList();

        launchResponse.MasterPlanImages = allImages
            .Where(f => f.PropertyType == "Launch-MasterPlan")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToList();

        launchResponse.LocationImages = allImages
            .Where(f => f.PropertyType == "Launch-Location")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToList();

        launchResponse.PaymentPlanImages = allImages
            .Where(f => f.PropertyType == "Launch-PaymentPlan")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToList();

        return Result.Success(launchResponse);
    }

    public async Task<Result<LaunchResponse>> EditLaunchAsync(int id, LaunchRequest launchRequest)
    {
        var launch = await _context.launches.FindAsync(id);

        if (launch == null)
        {
            return Result.Failure<LaunchResponse>(LaunchErrors.LaunchNotFound);
        }

        launchRequest.Adapt(launch);
        var result = await _context.SaveChangesAsync();

        if (result > 0)
        {
            var launchResponse = launch.Adapt<LaunchResponse>();

            var allImages = await _context.uploadedFiles
                .Where(f => f.PropertyId == launch.Id && f.PropertyType.StartsWith("Launch"))
                .ToListAsync();

            launchResponse.BannerImages = allImages
                .Where(f => f.Description == "Banner" && f.PropertyType == "Launch")
                .Select(f => Path.Combine("/uploads", f.StoredFileName))
                .ToList();

            launchResponse.MasterPlanImages = allImages
                .Where(f => f.Description == "MasterPlan" && f.PropertyType == "Launch")
                .Select(f => Path.Combine("/uploads", f.StoredFileName))
                .ToList();

            launchResponse.LocationImages = allImages
                .Where(f => f.Description == "Location" && f.PropertyType == "Launch")
                .Select(f => Path.Combine("/uploads", f.StoredFileName))
                .ToList();

            launchResponse.PaymentPlanImages = allImages
                .Where(f => f.Description == "PaymentPlan" && f.PropertyType == "Launch")
                .Select(f => Path.Combine("/uploads", f.StoredFileName))
                .ToList();

            return Result.Success(launchResponse);
        }

        return Result.Failure<LaunchResponse>(LaunchErrors.FailedToAddLaunch);
    }



    public async Task<Result<List<LaunchResponse>>> GetAllLaunchesAsync()
    {
        var launches = await _context.launches.ToListAsync();
        var launchResponses = launches.Adapt<List<LaunchResponse>>();

        foreach (var launch in launchResponses)
        {
            var allImages = await _context.uploadedFiles
                .Where(f => f.PropertyId == launch.Id && f.PropertyType.StartsWith("Launch"))
                .ToListAsync();

            launch.BannerImages = allImages
                .Where(f => f.Description == "Banner" && f.PropertyType == "Launch")
                .Select(f => Path.Combine("/uploads", f.StoredFileName))
                .ToList();

            launch.MasterPlanImages = allImages
                .Where(f => f.Description == "MasterPlan" && f.PropertyType == "Launch")
                .Select(f => Path.Combine("/uploads", f.StoredFileName))
                .ToList();

            launch.LocationImages = allImages
                .Where(f => f.Description == "Location" && f.PropertyType == "Launch")
                .Select(f => Path.Combine("/uploads", f.StoredFileName))
                .ToList();

            launch.PaymentPlanImages = allImages
                .Where(f => f.Description == "PaymentPlan" && f.PropertyType == "Launch")
                .Select(f => Path.Combine("/uploads", f.StoredFileName))
                .ToList();

            var properties = await _context.propertForSells
                .Where(p => p.LaunchId == launch.Id)
                .ToListAsync();

            var propertyResponses = new List<PropertyForSellResponse>();

            foreach (var property in properties)
            {
                var propImages = await _context.uploadedFiles
                    .Where(f => f.PropertyId == property.Id && f.PropertyType == "Sell")
                    .Select(f => Path.Combine("/uploads", f.StoredFileName))
                    .ToListAsync();

                var propertyResponse = property.Adapt<PropertyForSellResponse>();
                propertyResponse.Images = propImages;

                propertyResponses.Add(propertyResponse);
            }

            launch.Properties = propertyResponses;
        }

        return Result.Success(launchResponses);
    }



    public async Task<Result<LaunchResponse>> GetLaunchByIdAsync(int id)
    {
        var launch = await _context.launches.FindAsync(id);

        if (launch == null)
        {
            return Result.Failure<LaunchResponse>(LaunchErrors.LaunchNotFound);
        }

        var launchResponse = launch.Adapt<LaunchResponse>();

        var allImages = await _context.uploadedFiles
            .Where(f => f.PropertyId == launch.Id && f.PropertyType.StartsWith("Launch"))
            .ToListAsync();

        launchResponse.BannerImages = allImages
            .Where(f => f.Description == "Banner" && f.PropertyType == "Launch")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToList();

        launchResponse.MasterPlanImages = allImages
            .Where(f => f.Description == "MasterPlan" && f.PropertyType == "Launch")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToList();

        launchResponse.LocationImages = allImages
            .Where(f => f.Description == "Location" && f.PropertyType == "Launch")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToList();

        launchResponse.PaymentPlanImages = allImages
            .Where(f => f.Description == "PaymentPlan" && f.PropertyType == "Launch")
            .Select(f => Path.Combine("/uploads", f.StoredFileName))
            .ToList();

        var properties = await _context.propertForSells
            .Where(p => p.LaunchId == launch.Id)
            .ToListAsync();

        var propertyResponses = new List<PropertyForSellResponse>();

        foreach (var property in properties)
        {
            var propImages = await _context.uploadedFiles
                .Where(f => f.PropertyId == property.Id && f.PropertyType == "Sell")
                .Select(f => Path.Combine("/uploads", f.StoredFileName))
                .ToListAsync();

            var propertyResponse = property.Adapt<PropertyForSellResponse>();
            propertyResponse.Images = propImages;

            propertyResponses.Add(propertyResponse);
        }

        launchResponse.Properties = propertyResponses;

        return Result.Success(launchResponse);
    }



    public async Task<Result> RemoveLaunchAsync(int id)
    {
        var launch = await _context.launches.FindAsync(id);

        if (launch == null)
        {
            return Result.Failure(LaunchErrors.LaunchNotFound);
        }

        var files = await _context.uploadedFiles
            .Where(f => f.PropertyId == launch.Id && f.PropertyType.StartsWith("Launch"))
            .ToListAsync();

        foreach (var file in files)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.StoredFileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        _context.uploadedFiles.RemoveRange(files);

        _context.launches.Remove(launch);

        var result = await _context.SaveChangesAsync();

        return result > 0
            ? Result.Success()
            : Result.Failure(LaunchErrors.FailedToAddLaunch);
    }

}
