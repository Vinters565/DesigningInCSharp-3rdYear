using UI.Dto;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;

namespace UI;
public class ApiClient
{
    private readonly HttpClient httpClient;

    public ApiClient()
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5201")
        };
        var token = TokenFileStorage.GetToken();
        if (token != null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    private async Task<string> PostAsync(string endpoint, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        return responseString;
    }

    private async Task<T> PostAsyncJson<T>(string endpoint, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseJson);
    }

    public async Task<string> RegisterAsync(RegisterUserRequest request)
    {
        return await PostAsync("/register", request);
    }

    public async Task<string> LoginAsync(LoginUserRequest request)
    {
        return await PostAsync("/login", request);
    }

    public async Task<List<CalendarEventDto>> GetEventsAsync(Guid request)
    {
        return await PostAsyncJson<List<CalendarEventDto>>("/calendars/private/events", request);
    }

    public async Task<string> CreateEventsAsync(CreateCalendarEventRequest request)
    {
        return await PostAsyncJson<string>("/events/", request);
    }
}
