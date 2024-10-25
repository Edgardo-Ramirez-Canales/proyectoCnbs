using System.Net.Http;
using System.Threading.Tasks;
using System;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "MEYwMEJGQ0E3QUNDN0MxNTg2M0UyOEE1QTU0MTcwM0FBQjUwNjE4MkFGNjQ0RjMyQUNCMDI1OTdDMjUwMDREOA=="; // Aquí pones directamente el ApiKey

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataByDateAsync(string fechaConsultar)
    {
        string url = $"https://iis-des.cnbs.gob.hn/ws.TestData/api/data?Fecha={fechaConsultar}";

     
        _httpClient.DefaultRequestHeaders.Add("ApiKey", _apiKey);

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return data;
        }
        else
        {
            throw new Exception("Error al obtener datos de API CNBS ");
        }
    }
}
