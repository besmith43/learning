using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using OneOf;
using BasicWASMExample.Shared.Models;

namespace BasicWASMExample.Client.APIReceivers;

public class QuickLinksService : IAPIService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;
    private string _baseUrl = "http://localhost:5001/";

    // getting a runtime error when trying to inject HttpClient
    public QuickLinksService(IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
        _baseUrl = _config["BaseAPIUrl"];
    }

    public OneOf<List<QuickLink>, ErrorMessage> GetQuickLinks()
    {
        var client = _httpClientFactory.CreateClient();

        var response = client.GetAsync(_baseUrl + "api/QuickLinks").Result;

        if (response.IsSuccessStatusCode)
        {
            return response.Content.ReadFromJsonAsync<List<QuickLink>>().Result;
        }
        else
        {
            return response.Content.ReadFromJsonAsync<ErrorMessage>().Result;
        }
    }
}