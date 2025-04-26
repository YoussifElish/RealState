using RealState.Abstactions;

namespace RealState.Errors;

public static class LaunchErrors
{
    public static readonly Error LaunchNotFound = new(
           "Launch.NotFound",
           "The Launch you are looking for was not found.",
           StatusCodes.Status404NotFound
       );  
    
    public static readonly Error FailedToAddLaunch = new(
           "Launch.FailedToAdd",
           "Failed to add launch.",
           StatusCodes.Status400BadRequest
       );

   

    public static readonly Error InvalidLaunchId = new(
        "Launch.InvalidId",
        "The provided Launch ID is invalid.",
        StatusCodes.Status400BadRequest
    );

}
