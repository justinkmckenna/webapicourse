namespace CoursesApi.Domain;

public interface IProvideOfferings
{
    public Task<List<DateTime>> GetOfferingsForCourseAsync(int courseId);
}