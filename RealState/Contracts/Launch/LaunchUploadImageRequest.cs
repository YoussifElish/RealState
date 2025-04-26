namespace RealState.Contracts.Launch;

public record LaunchUploadImageRequest(
    IFormFile BannerImage,
    IFormFile MasterPlanImage,
    IFormFile LocationImage,
    IFormFile PaymentPlanImage

);