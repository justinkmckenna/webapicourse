namespace CoursesApi.Domain;

public class CourseCatalog
{
    private readonly CoursesDataContext _context;

    public CourseCatalog(CoursesDataContext context)
    {
        _context = context;
    }

    public async Task<CoursesResponseModel> GetFullCatalogAsync(CancellationToken token)
    {
        var courses = await _context.Courses.Where(c => c.Retired == false).Select(c => new CourseItemResponse {
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
        var response = await _context.Courses.Where(c => c.Id == id && c.Retired == false)
            .Select(c => new CourseItemDetailsResponse
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Category = c.Category,
                Description = c.Description
            }).SingleOrDefaultAsync(token);

        return response;
    }
}
