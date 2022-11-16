using System.ComponentModel.DataAnnotations;

namespace CoursesApi.Models;

public record CourseCreateRequest
{
    [Required, MaxLength(100)]
    public string Title { get; init; } = string.Empty;
    [Required, MaxLength(500)]
    public string Description { get; init; } = string.Empty;
}

public record CoursesResponseModel
{
    public int NumberOfBackendCourses { get; init; }
    public int NumberOfFrontendCourses { get; init; }
    public List<CourseItemResponse> Courses { get; init; } = new List<CourseItemResponse>();
}

public record CourseItemResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public CategoryType Category { get; set; }
}

public record CourseItemDetailsResponse
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public CategoryType Category { get; init; }
    public string Description { get; init; } = string.Empty;
}