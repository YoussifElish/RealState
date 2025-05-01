using RealState.Abstactions;

namespace RealState.Errors;

public static class PropertyErrors
{
    public static readonly Error PropertyNotFound = new(
           "Property.NotFound",
           "The property you are looking for was not found.",
           StatusCodes.Status404NotFound
       );

    public static readonly Error FailedToAddProperty = new(
        "Property.AddFailed",
        "Failed to add the property. Please try again later.",
        StatusCodes.Status500InternalServerError
    );

    public static readonly Error FailedToDeleteProperty = new(
        "Property.DeleteFailed",
        "Failed to delete the property. It may not exist or something went wrong.",
        StatusCodes.Status500InternalServerError
    );

    public static readonly Error InvalidPropertyId = new(
        "Property.InvalidId",
        "The provided property ID is invalid.",
        StatusCodes.Status400BadRequest
    );

    public static readonly Error InvalidPropertyType =
        new("Property.InvalidType", "Property type must be either 'Sell' or 'Rent'",StatusCodes.Status400BadRequest);

}
