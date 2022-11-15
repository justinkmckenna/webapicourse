namespace CoursesApi.Controllers;

public class CoursesController : ControllerBase
{

    private readonly CourseCatalog _catalog;

    public CoursesController(CourseCatalog catalog)
    {
        _catalog = catalog;
    }

    [HttpGet("/courses")]
    public async Task<ActionResult> GetCoursesAsync(CancellationToken token)
    {
        var response = await _catalog.GetFullCatalogAsync(token);
        return Ok(response);
    }

    [HttpGet("/courses/{id:int}")]
    public async Task<ActionResult<CourseItemDetailsResponse>> GetCourseById(int id, CancellationToken token)
    {
        CourseItemDetailsResponse response = await _catalog.GetCourseByIdAsync(id, token);



        if (response == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(response);
        }
    }
}
