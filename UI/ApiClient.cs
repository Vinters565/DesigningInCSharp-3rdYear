using System.Globalization;
using UI.Dto;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace UI;

public class ApiClient
{
    private const string apiUrl = "http://127.0.0.1:5201";

    private readonly JsonSerializerOptions jsonSerializerOptions;
    private readonly HttpClient httpClient;

    public ApiClient()
    {
        jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(apiUrl)
        };
        
        PutToken();
    }

    public async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? queryParams = null)
    {
        PutToken();
        var url = BuildUrlWithQuery(endpoint, queryParams);
        var response = await httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseJson, jsonSerializerOptions) 
               ?? throw new JsonException($"Cannot deserialize API response from url: GET {url}");
    }

    private async Task<T> PostAsync<T>(string endpoint, object data)
    {
        PutToken();
        
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        
        // TODO: Костыль из-за неправильного ответа API для типа string
        if (typeof(T) == typeof(string))
        {
            if (!responseJson.StartsWith("\""))
            {
                responseJson = "\"" + responseJson;
            }

            if (!responseJson.EndsWith("\""))
            {
                responseJson += "\"";
            }
        }
        
        return JsonSerializer.Deserialize<T>(responseJson, jsonSerializerOptions)
               ?? throw new JsonException($"Cannot deserialize API response from url: POST {endpoint}");
    }
    
    public async Task<T> PutAsync<T>(string endpoint, object body)
    {
        PutToken();
        
        var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(endpoint, content);

        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseJson, jsonSerializerOptions)
               ?? throw new JsonException($"Cannot deserialize API response from url: PUT {endpoint}");
    }
    
    public async Task DeleteAsync(string endpoint)
    {
        PutToken();
        
        var response = await httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
    }

    public async Task<string> RegisterAsync(RegisterUserRequest request)
    {
        return await PostAsync<string>("/register", request);
    }

    public async Task<string> LoginAsync(LoginUserRequest request)
    {
        return await PostAsync<string>("/login", request);
    }

    public async Task<List<CalendarEventDto>> GetEventsAsync(DateTime fromDate, int? calendarView = null)
    {
        var queryParams = new Dictionary<string, string>();
        
        queryParams.Add("start", fromDate.ToString(CultureInfo.InvariantCulture));
        if (calendarView != null) 
            queryParams.Add("view", calendarView.Value.ToString());
        
        return await GetAsync<List<CalendarEventDto>>("/calendars/private/events", queryParams);
    }

    public async Task<string> CreateEventsAsync(CreateCalendarEventRequest request)
    {
        throw new NotImplementedException();
        return await PostAsync<string>("/events/", request);
    }
    
    private static string BuildUrlWithQuery(string endpoint, Dictionary<string, string>? queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
            return endpoint;

        var queryString = new StringBuilder("?");
        foreach (var param in queryParams)
        {
            queryString.Append($"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(param.Value)}&");
        }

        queryString.Length--;
        return endpoint + queryString;
    }

    private void PutToken()
    {
        if (httpClient.DefaultRequestHeaders.Authorization?.Parameter != null) return;
        
        var token = TokenFileStorage.GetToken();
        if (token != null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
