using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Domain;

public class CourseCatalog
{
    private readonly CoursesDataContext _coursesDataContext;

    public CourseCatalog(CoursesDataContext coursesDataContext)
    {
        _coursesDataContext = coursesDataContext;
    }

    public async Task<CoursesResponseModel> GetFullCatalogAsync(CancellationToken token)
    {
        var courses = await _coursesDataContext.Courses.Where(c => c.Retired == false).Select(c => new CourseItemResponse {
            Id = c.Id.ToString(),
            Title = c.Title,
            Category = c.Category
        }).ToListAsync(token);

        var backEndCount = courses.Count(c => c.Category == CategoryType.Backend);
        var frontEndCount = courses.Count(c => c.Category == CategoryType.Frontend);

        return new CoursesResponseModel { Courses = courses , NumberOfBackendCourses = backEndCount , NumberOfFrontendCourses = frontEndCount};
    }

    public async Task<CourseItemDetailsResponse?> GetCourseByIdAsync(int id, CancellationToken token)
    {
        var response = await _coursesDataContext.Courses.Where(c => c.Id == id && c.Retired == false)
            .Select(c => new CourseItemDetailsResponse
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Category = c.Category,
                Description = c.Description
            }).SingleOrDefaultAsync(token);

        return response;
    }

    public async Task<CourseItemDetailsResponse> AddCourseAsync(CourseCreateRequest request, CategoryType category)
    {
        var courseToAdd = new CourseEntity
        {
            Title = request.Title,
            Description = request.Description,
            Retired = false,
            Category = category
        };
        _coursesDataContext.Courses.Add(courseToAdd);
        await _coursesDataContext.SaveChangesAsync();

        var response = new CourseItemDetailsResponse
        {
            Id = courseToAdd.Id.ToString(),
            Title = courseToAdd.Title,
            Description = courseToAdd.Description,
            Category = courseToAdd.Category
        };
        return response;
    }

    public async Task<bool> UpdateDescriptionAsync(int id, string description)
    {
        var course = await _coursesDataContext.Courses.SingleOrDefaultAsync(c => c.Id == id && c.Retired == false);
        if (course != null)
        {
            course.Description = description;
            await _coursesDataContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
