using RealState.Abstactions;
using RealState.Contracts.Launch;
using RealState.Entities;

namespace RealState.Services;

public interface ILaunchService
{
    Task<Result<LaunchResponse>> AddLaunchAsync(LaunchRequest launchRequest, LaunchUploadImageRequest uploadImageRequest);
    Task<Result<LaunchResponse>> EditLaunchAsync(int id, LaunchRequest launchRequest);
    Task<Result> RemoveLaunchAsync(int id);
    Task<Result<LaunchResponse>> GetLaunchByIdAsync(int id);
    Task<Result<List<LaunchResponse>>> GetAllLaunchesAsync();

}
