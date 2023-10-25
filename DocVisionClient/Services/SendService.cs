using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocVisionClient.Services;

public class SendService:ISendService
{
    private const string API_URL = @"https://localhost:7148/api/Messages/registrateMessage";
    private readonly HttpClient _httpClient;
    public SendService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<bool> SendMessage(Message message)
    {
        try
        {
            var json = JsonContent.Create(message);

            var response = await _httpClient.PostAsync(API_URL, json);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }
        catch (Exception ex)
        {
#if DEBUG
            Console.WriteLine(ex);
#endif
            return false;
        }
       
    }
}
