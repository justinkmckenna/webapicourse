namespace CoursesApi.Domain;

public class OfferingsCatalog
{
    public async Task<List<DateTime>> GetOfferingsForCourse(int courseId)
    {
        var numberOfOfferings = courseId % 2 == 0 ? 5 : 8;
        var results = new List<DateTime>();
        foreach (var d in Enumerable.Range(1, numberOfOfferings))
        {
            results.Add(DateTime.Now.AddDays(d));
        }
        return results;
    }
}
