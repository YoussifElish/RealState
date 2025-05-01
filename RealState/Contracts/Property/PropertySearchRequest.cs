namespace RealState.Contracts.Property;

public record PropertySearchRequest(
    string PropertyType,
   int? NumberOfRooms,
   string? Location);

