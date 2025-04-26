using RealState.Abstactions;

namespace RealState.Errors;

public static class LeadErrors
{
    public static readonly Error LeadNotFound = new(
           "Property.NotFound",
           "The property you are looking for was not found.",
           StatusCodes.Status404NotFound
       );

   

    public static readonly Error InvalidUserId = new(
        "Property.InvalidId",
        "The provided UserId ID is invalid.",
        StatusCodes.Status400BadRequest
    );

}
