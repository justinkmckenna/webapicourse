namespace CoursesApi.Adapters;

public class OfferingsApiAdapter
{
    private readonly HttpClient _client;

    public OfferingsApiAdapter(HttpClient client)
    {
        _client = client;
    }

    public async Task<Offerings?> GetOfferingsForCourseAsync(int courseId)
    {
        var response = await _client.GetAsync($"{courseId}");

        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<Offerings>();

        return data;
    }
}