using System.Globalization;
using UI.Dto;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Windows;

namespace UI;

public class ApiClient
{
    //://localhost:5201
    //://127.0.0.1:5201
    private const string apiUrl = "http://localhost:5201";

    private readonly JsonSerializerOptions jsonSerializerOptions;
    private readonly HttpClient httpClient;

    public ApiClient()
    {
        jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(apiUrl)
        };
    }

    public async Task<string> RegisterAsync(RegisterUserRequest request)
    {
        return await PostAsync<string>("/register", request);
    }

    public async Task<string> LoginAsync(LoginUserRequest request)
    {
        return await PostAsync<string>("/login", request);
    }

    public async Task<List<AttributeMetadata>> GetEventAttributesAsync()
    {
        return await GetAsync<List<AttributeMetadata>>("/events/attributes");
    }

    public async Task<List<CalendarEventDto>> GetPrivateEventsAsync(DateTime fromDate, int? calendarView = null)
    {
        var queryParams = new Dictionary<string, string>();
        
        queryParams.Add("start", fromDate.ToString(CultureInfo.InvariantCulture));
        if (calendarView != null) 
            queryParams.Add("view", calendarView.Value.ToString());
        
        return await GetAsync<List<CalendarEventDto>>("/calendars/private/events", queryParams);
    }

    public async Task<List<CalendarEventDto>> GetPublicEventsAsync(string username, 
        DateTime fromDate, int? calendarView = null)
    {
        var queryParams = new Dictionary<string, string>();

        queryParams.Add("start", fromDate.ToString(CultureInfo.InvariantCulture));
        if (calendarView != null)
            queryParams.Add("view", calendarView.Value.ToString());

        return await GetAsync<List<CalendarEventDto>>($"/calendars/public/{username}/events", queryParams);
    }

    public async Task<CalendarEventDto> CreateEventsAsync(CreateCalendarEventRequest request)
    {
        return await PostAsync<CalendarEventDto>("/events", request);
    }

    public async Task<CalendarEventDto> GetEventAsync(Guid eventId)
    {
        return await GetAsync<CalendarEventDto>($"events/{eventId}");
    }

    public async Task<CalendarEventDto> UpdateEventAsync(Guid eventId, UpdateCalendarEventRequest request)
    {
        return await PutAsync<CalendarEventDto>($"/events/{eventId}", request);
    }

    public async Task DeleteEventAsync(Guid eventId)
    {
        await DeleteAsync($"/events/{eventId}");
    }
    
    private async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? queryParams = null)
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
        
        var json = JsonSerializer.Serialize(data, jsonSerializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(endpoint, content);
        try
        {
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
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
            return default(T);
        }
    }
    
    private async Task<T> PutAsync<T>(string endpoint, object body)
    {
        PutToken();

        var json = JsonSerializer.Serialize(body, jsonSerializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(endpoint, content);

        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseJson, jsonSerializerOptions)
               ?? throw new JsonException($"Cannot deserialize API response from url: PUT {endpoint}");
    }
    
    private async Task DeleteAsync(string endpoint)
    {
        PutToken();
        
        var response = await httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
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
