namespace ApiBasics.Models;

public record EmployeeDetailsResponse
{
    public int Id { get; init; }
    public string FirstName { get; init; } = "";
    public string LastName { get; init; } = string.Empty;
    public string EmailAddress { get; init; } = string.Empty;
}


public record EmployeeCreateRequest
{
    public string? firstName { get; init; }
    public string? lastName { get; init; }
    public string? department { get; init; }
}
