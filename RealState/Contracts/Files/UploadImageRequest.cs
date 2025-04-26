namespace RealState.Api.Contracts;

public record UploadImageRequest(
    IFormFileCollection Image
);  