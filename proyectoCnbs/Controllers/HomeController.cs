using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly ApiService _apiService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApiService apiService, ILogger<HomeController> logger)
    {
        _apiService = apiService;
        _logger = logger;
    }

    public async Task<IActionResult> GetApiData(string fechaConsultar)
    {
        try
        {
           
            var data = await _apiService.GetDataByDateAsync(fechaConsultar);

            
            Console.WriteLine(data);
            _logger.LogInformation(message: data);
            
            return Ok(data); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return BadRequest("Error al obtener datos de la API");
        }
    }
}
