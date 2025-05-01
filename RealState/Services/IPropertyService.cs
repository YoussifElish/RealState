using RealState.Abstactions;
using RealState.Api.Contracts;
using RealState.Contracts.Property;

namespace RealState.Services;

public interface IPropertyService
{
    Task<Result<PropertyForSellResponse>> AddPropertyForSell(PropertyForSellRequest propertyDto, UploadImageRequest uploadImageRequest);
    Task<Result<PropertyForRentResponse>> AddPropertyForRent(PropertyForRentRequest propertyDto, UploadImageRequest uploadImageRequest);
    Task<Result<PropertyForSellResponse>> GetPropertyForSell(int id);
    Task<Result<PropertyForRentResponse>> GetPropertyForRent(int id);
    Task<Result> DeletePropertyForSell(int id);
    Task<Result> DeletePropertyForRent(int id);
    Task<Result<List<PropertyForSellHomePageResponse>>> GetAllPropertiesForSell();
    Task<Result<List<PropertyForSellHomePageResponse>>> GetAllPropertiesForRent();
    Task<Result<PropertyForSellResponse>> EditPropertyForSell(int id, PropertyForSellRequest propertyDto, UploadImageRequest uploadImageRequest);
    Task<Result<PropertyForRentResponse>> EditPropertyForRent(int id, PropertyForRentRequest propertyDto, UploadImageRequest uploadImageRequest);

    Task<Result<List<PropertyForSellHomePageResponse>>> SearchProperties(PropertySearchRequest searchRequest);
}
