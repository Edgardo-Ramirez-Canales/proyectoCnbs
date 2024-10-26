using Microsoft.AspNetCore.Mvc;
using proyectoCnbs.Data;
using proyectoCnbs.Services;
using Microsoft.EntityFrameworkCore;
using proyectoCnbs.Models;

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

    //obtener una lista de los obj de la bd falta vista
   [HttpGet]
   public async Task<IActionResult> Index()
    {
        return View(await _contexto.ApiData.ToListAsync());
    }

    public async Task<IActionResult> GetApiData(string fechaConsultar)
    {
        try
        {

            var data = await _apiService.GetDataByDateAsync(fechaConsultar);

            var declaracionObj = _xmlProcessor.DeserializeXml(data);
            //_logger.LogInformation($"Declaracion: FechaAConsultar = {declaracionObj.FechaAConsultar}, CuentaDeclaraciones = {declaracionObj.CuentaDeclaraciones}, DatosComprimidos = {declaracionObj.DatosComprimidos}");
            if (declaracionObj == null)
            {
                _logger.LogError("No se pudo deserializar el XML.");
                return BadRequest("Error al deserializar el XML.");
            }

            // Descomprime el valor de DatosComprimidos
            string datosDescomprimidos = string.Empty;

            if (!string.IsNullOrEmpty(declaracionObj.DatosComprimidos))
            {
                _logger.LogInformation("Iniciando descompresión de DatosComprimidos...");
                datosDescomprimidos = await XmlProcessor.DecompressAsync(declaracionObj.DatosComprimidos);
                //_logger.LogInformation($"Datos Descomprimidos: {datosDescomprimidos}");
            }
            else
            {
                _logger.LogWarning("DatosComprimidos está vacío o es nulo.");
            }

            // Paso 4: Crear una instancia de ApiData y asignar los valores
            var apiData = new ApiData
            {
                NroTransaccion = declaracionObj.NroTransaccion,
                FechaHoraTrn = declaracionObj.FechaHoraTrn,
                FechaAConsultar = declaracionObj.FechaAConsultar,
                CuentaDeclaraciones = declaracionObj.CuentaDeclaraciones,
                //DatosComprimidos = declaracionObj.DatosComprimidos,
                JsonDatos = datosDescomprimidos  // Almacenar el valor descomprimido
            };

            // Guardar el objeto en la base de datos
            _contexto.ApiData.Add(apiData);
            await _contexto.SaveChangesAsync();

            _logger.LogInformation("Datos guardados exitosamente en la base de datos.");


            return Ok(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return BadRequest("Error al obtener datos de la API");
        }
    }
}