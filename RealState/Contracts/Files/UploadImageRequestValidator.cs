using FluentValidation;
using RealState.Contracts.Files.Common;
using RealState.Settings;

namespace RealState.Api.Contracts;

public class UploadImageRequestValidator : AbstractValidator<UploadImageRequest>
{
    public UploadImageRequestValidator()
    {
        
        RuleFor(x => x.Image)
            .Must(File =>
            {
                var extension = Path.GetExtension(File.FileName.ToLower());
                return FileSettings.AllowedImagesExtensions.Contains(extension);
            })
            .WithMessage("File extension is not allowed")
            .When(x => x.Image is not null);
    }
}