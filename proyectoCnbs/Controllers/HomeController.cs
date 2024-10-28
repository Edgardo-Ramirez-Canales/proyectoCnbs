using Microsoft.AspNetCore.Mvc;
using proyectoCnbs.Data;
using proyectoCnbs.Services;
using proyectoCnbs.Models;
using System.Text.Json;


public class HomeController : Controller
{
    private readonly ApiService _apiService;
    private readonly ILogger<HomeController> _logger;
    private readonly XmlProcessor _xmlProcessor;
    private readonly ApplicationDbContext _contexto;

    public HomeController(ApiService apiService, ILogger<HomeController> logger, XmlProcessor xmlProcessor, ApplicationDbContext contexto)
    {
        _apiService = apiService;
        _logger = logger;
        _xmlProcessor = xmlProcessor;
        _contexto = contexto;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Message = "Por favor, especifica una fecha y presiona 'Buscar Declaracion' para consultar.";
        return View();
    }

   



    [HttpPost]
    public async Task<IActionResult> BuscarRegistro(string fechaConsultar, string Nddtimmioe)
    {
        if (string.IsNullOrWhiteSpace(fechaConsultar))
        {
            ViewBag.Message = "Debe ingresar una fecha válida.";
            return View("Index", Enumerable.Empty<ApiData>());
        }

        // Llama GetApiData 
        var resultado = await GetApiData(fechaConsultar) as OkObjectResult;
        var apiData = resultado?.Value as ApiData;

        if (apiData == null)
        {
            ViewBag.Message = "No se encontraron registros para la fecha especificada.";
            return View("Index", Enumerable.Empty<ApiData>());
        }

       
        var datosProcesadosJson = ProcesarJsonDatos(apiData.JsonDatos,Nddtimmioe) as string;
   
        if (string.IsNullOrEmpty(datosProcesadosJson))
        {
            
            ViewBag.ResultadoProcesado = null;
        }
        else
        {
            
            var datosProcesados = JsonSerializer.Deserialize<List<DeclaracionModels>>(datosProcesadosJson);
            ViewBag.ResultadoProcesado = datosProcesados;
        }

        // Devuelve lista 
        return View("Index", new List<ApiData> { apiData });
    }

    private object ProcesarJsonDatos(string jsonDatos, string Nddtimmioe)
    {
        try
        {
           
            // Configurar Ignorar mayúsculas y minúsculas
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // but
            };
            
            var declaraciones = JsonSerializer.Deserialize<List<DeclaracionModels>>(jsonDatos, opciones);

            var totalDDTConNddtimmioe = declaraciones.Count(d => d.DDT != null && d.DDT.Nddtimmioe != null);
            var totalCoincidencias = declaraciones.Count(d => d.DDT != null && d.DDT.Nddtimmioe == Nddtimmioe);

            //_logger.LogInformation($"Total de declaraciones con DDT y Nddtimmioe no nulo: {totalDDTConNddtimmioe}");
            //_logger.LogInformation($"Total de coincidencias exactas con Nddtimmioe '{Nddtimmioe}': {totalCoincidencias}");

            var valoresNddtimmioe = declaraciones
                         .Where(d => d.DDT != null && d.DDT.Nddtimmioe != null)
                         .Select(d => d.DDT.Nddtimmioe)
                         .Distinct();

            _logger.LogInformation($"Valores únicos de Nddtimmioe en declaraciones: {string.Join(", ", valoresNddtimmioe)}");

            if (declaraciones == null || declaraciones.Count == 0)
            {
                _logger.LogWarning("Deserialización exitosa, pero no se encontraron declaraciones en el JSON.");
                return null;
            }

            var declaracionesFiltradas = declaraciones
                .Where(d => d.DDT != null && d.DDT.Nddtimmioe == Nddtimmioe)
                .ToList();

            if (declaracionesFiltradas.Count == 0)
            {
                _logger.LogWarning($"No se encontraron declaraciones con Nddtimmioe = {Nddtimmioe}");
                return null;
            }

            var resultadoJson = JsonSerializer.Serialize(declaracionesFiltradas, new JsonSerializerOptions { WriteIndented = true });
            //_logger.LogInformation($"JSON final procesado: {resultadoJson}");
            return resultadoJson;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al procesar JsonDatos: {ex.Message}");
            return null;
        }
    }

    public async Task<IActionResult> GetApiData(string fechaConsultar)
    {
        try
        {
            var data = await _apiService.GetDataByDateAsync(fechaConsultar);
            var declaracionObj = _xmlProcessor.DeserializeXml(data);
          
            if (declaracionObj == null)
            {
                return BadRequest("Error al deserializar el XML.");
            }

  
            string datosDescomprimidos = string.Empty;
            if (!string.IsNullOrEmpty(declaracionObj.DatosComprimidos))
            {
                datosDescomprimidos = await XmlProcessor.DecompressAsync(declaracionObj.DatosComprimidos);
            }
            else
            {
                _logger.LogWarning("DatosComprimidos está vacío o es nulo.");
            }

            var apiData = new ApiData
            {
                NroTransaccion = declaracionObj.NroTransaccion,
                FechaHoraTrn = declaracionObj.FechaHoraTrn,
                FechaAConsultar = DateTime.Parse(declaracionObj.FechaAConsultar),
                CuentaDeclaraciones = declaracionObj.CuentaDeclaraciones,
                JsonDatos = datosDescomprimidos  
            };

            _contexto.ApiData.Add(apiData);
            await _contexto.SaveChangesAsync();
            _logger.LogInformation("Datos guardados exitosamente en la base de datos.");


            return Ok(apiData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return BadRequest("Error al obtener datos de la API");
        }
    }
}