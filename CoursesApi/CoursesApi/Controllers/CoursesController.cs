namespace CoursesApi.Controllers;

[ApiController]
public class CoursesController : ControllerBase
{

    private readonly CourseCatalog _courseCatalog;
    private readonly IProvideOfferings _offeringsCatalog;


    public CoursesController(CourseCatalog courseCatalog, IProvideOfferings offeringsCatalog)
    {
        _courseCatalog = courseCatalog;
        _offeringsCatalog = offeringsCatalog;
    }

    [HttpGet("/courses")]
    public async Task<ActionResult> GetCoursesAsync(CancellationToken token)
    {
        var response = await _courseCatalog.GetFullCatalogAsync(token);
        return Ok(response);
    }

    [HttpGet("/courses/{id:int}", Name ="course-details")]
    public async Task<ActionResult<CourseItemDetailsResponse>> GetCourseById(int id, CancellationToken token)
    {
        CourseItemDetailsResponse response = await _courseCatalog.GetCourseByIdAsync(id, token);
        return response is CourseItemDetailsResponse data ? Ok(data) : NotFound();
    }

    [HttpGet("/courses/{id:int}/offerings")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
    public async Task<ActionResult> GetOfferingsForCourse(int id)
    {
        // TODO talk about a 404 here.
        // check to see if that course exists, if it doesn, return a 404.
        var data = await _offeringsCatalog.GetOfferingsForCourseAsync(id);
        return Ok(new { Offerings = data });
    }

    [HttpPost("/frontend-courses")]
    public async Task<ActionResult<CourseItemDetailsResponse>> AddFECourse([FromBody] CourseCreateRequest request)
    {
        var response = await _courseCatalog.AddCourseAsync(request, CategoryType.Frontend);
        return CreatedAtRoute("course-details", new { id = response.Id }, response);
    }

    [HttpPost("/backend-courses")]
    public async Task<ActionResult<CourseItemDetailsResponse>> AddBECourse([FromBody] CourseCreateRequest request)
    {
        var response = await _courseCatalog.AddCourseAsync(request, CategoryType.Backend);
        return CreatedAtRoute("course-details", new { id = response.Id }, response);
    }

    [HttpPut("/courses/{id:int}/description")]
    public async Task<ActionResult<CourseItemDetailsResponse>> UpdateDescription(int id, [FromBody] string description)
    {
        var response = await _courseCatalog.UpdateDescriptionAsync(id, description);
        return response ? NoContent() : NotFound();
    }
}