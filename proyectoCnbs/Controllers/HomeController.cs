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
        // Cargar la vista sin datos específicos inicialmente
        ViewBag.Message = "Por favor, especifica una fecha y presiona 'Buscar' para consultar.";
        return View();
    }

    [HttpPost]
    public IActionResult BuscarRegistroManual()
    {
        // JSON de prueba que deseas procesar
        string jsonDatos = @"[
   {
      ""ART"":[
         {
            ""Iddt"":""200023INTE027863Z"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""000"",
            ""Cartetamrc"":""NU"",
            ""Iespnce"":""4821.10.00.00.00"",
            ""Cartdesc"":""ETIQUETAS PARA EMPAQUE"",
            ""Cartpayori"":""SV"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":0.0882,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":57110,
            ""Cartuntest"":""01"",
            ""Qartuntest"":32.8,
            ""Qartkgrbrt"":37,
            ""Qartkgrnet"":32.8,
            ""Martfob"":5035.91,
            ""Martfobdol"":5035.91,
            ""Martfle"":null,
            ""Martass"":null,
            ""Martemma"":null,
            ""Martfrai"":0,
            ""Martajuinc"":null,
            ""Martajuded"":null,
            ""Martbasimp"":123957.41
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""200023023604A"",
         ""Cddtver"":2,
         ""Iddtext"":""200023023604A"",
         ""Iddt"":""200023INTE027863Z"",
         ""Iext"":""5940440"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2020-08-04T09:40:40"",
         ""Dddtrectifa"":""2020-08-04T09:48:07"",
         ""Cddtcirvis"":""A"",
         ""Qddttaxchg"":24.6147,
         ""Ista"":""5100"",
         ""Cddtbur"":""0023"",
         ""Cddtburdst"":""0023"",
         ""Cddtdep"":null,
         ""Cddtentrep"":null,
         ""Cddtage"":""14091"",
         ""Nddtimmioe"":""06826421093070"",
         ""Lddtnomioe"":""HA***O CI***S, SO*****D AN****A"",
         ""Cddtagr"":""01721159991080"",
         ""Lddtagr"":""AG****A AD*****A ES****L S. DE R.L."",
         ""Cddtpayori"":""SV"",
         ""Cddtpaidst"":""SV"",
         ""Lddtnomfod"":""AL****S IM*******S S.A. DE C.V."",
         ""Cddtincote"":""CIP"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":null,
         ""Cddtdevass"":null,
         ""Cddttransp"":""14079"",
         ""Cddtmdetrn"":""T"",
         ""Cddtpaytrn"":""SV"",
         ""Nddtart"":1,
         ""Nddtdelai"":54,
         ""Dddtbae"":""2020-08-04T11:26:25"",
         ""Dddtsalida"":""2020-08-04T12:34:12"",
         ""Dddtcancel"":""2021-06-20T00:29:35"",
         ""Dddtechean"":""2025-02-04T00:00:00"",
         ""Cddtobs"":null
      },
      ""LIQ"":{
         ""Iliq"":""200023INTE027863Z"",
         ""Cliqdop"":""20260104835121"",
         ""Cliqeta"":""COB"",
         ""Mliq"":0,
         ""Mliqgar"":0,
         ""Dlippay"":null,
         ""Clipnomope"":null
      },
      ""LQA"":[
         {
            ""Iliq"":""200023INTE027863Z"",
            ""Nart"":1,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":123957.4139,
            ""Qlqacoefic"":15,
            ""Mlqa"":18593.6121
         },
         {
            ""Iliq"":""200023INTE027863Z"",
            ""Nart"":1,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":142551.026,
            ""Qlqacoefic"":15,
            ""Mlqa"":21382.6539
         }
      ]
   },
   {
      ""ART"":[
         {
            ""Iddt"":""200014INTE035729Z"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""000"",
            ""Cartetamrc"":""NU"",
            ""Iespnce"":""4822.90.00.00.00"",
            ""Cartdesc"":""ANILLOS PARA PUROS"",
            ""Cartpayori"":""DO"",
            ""Cartpayacq"":""DO"",
            ""Cartpayprc"":""DO"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":8,
            ""Martunitar"":0.0328,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":616800,
            ""Cartuntest"":""01"",
            ""Qartuntest"":77,
            ""Qartkgrbrt"":77,
            ""Qartkgrnet"":77,
            ""Martfob"":20215.61,
            ""Martfobdol"":20215.61,
            ""Martfle"":772.57,
            ""Martass"":303.23,
            ""Martemma"":null,
            ""Martfrai"":0,
            ""Martajuinc"":null,
            ""Martajuded"":null,
            ""Martbasimp"":521562.9
         },
         {
            ""Iddt"":""200014INTE035729Z"",
            ""Nart"":2,
            ""Carttyp"":""N"",
            ""Codbenef"":""000"",
            ""Cartetamrc"":""NU"",
            ""Iespnce"":""4821.10.00.00.00"",
            ""Cartdesc"":""ETIQUETAS DE PAPEL"",
            ""Cartpayori"":""DO"",
            ""Cartpayacq"":""DO"",
            ""Cartpayprc"":""DO"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":22,
            ""Martunitar"":0.6632,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":8800,
            ""Cartuntest"":""01"",
            ""Qartuntest"":200,
            ""Qartkgrbrt"":200,
            ""Qartkgrnet"":200,
            ""Martfob"":5836,
            ""Martfobdol"":5836,
            ""Martfle"":223.03,
            ""Martass"":87.54,
            ""Martemma"":null,
            ""Martfrai"":0,
            ""Martajuinc"":null,
            ""Martajuded"":null,
            ""Martbasimp"":150568.84
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""200014032584H"",
         ""Cddtver"":1,
         ""Iddtext"":""200014032584H"",
         ""Iddt"":""200014INTE035729Z"",
         ""Iext"":""5972071"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2020-09-09T11:52:24"",
         ""Dddtrectifa"":null,
         ""Cddtcirvis"":""V"",
         ""Qddttaxchg"":24.4964,
         ""Ista"":""5100"",
         ""Cddtbur"":""0014"",
         ""Cddtburdst"":""0034"",
         ""Cddtdep"":""DT01"",
         ""Cddtentrep"":null,
         ""Cddtage"":""11606"",
         ""Nddtimmioe"":""06826421093070"",
         ""Lddtnomioe"":""HA***O CI***S, SO*****D AN****A"",
         ""Cddtagr"":""01721159991080"",
         ""Lddtagr"":""AG****A AD*****A ES****L S. DE R.L."",
         ""Cddtpayori"":""DO"",
         ""Cddtpaidst"":""DO"",
         ""Lddtnomfod"":""CI**R RI**S"",
         ""Cddtincote"":""FOB"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":""USD"",
         ""Cddtdevass"":""USD"",
         ""Cddttransp"":""15504"",
         ""Cddtmdetrn"":""T"",
         ""Cddtpaytrn"":""US"",
         ""Nddtart"":2,
         ""Nddtdelai"":54,
         ""Dddtbae"":""2020-09-09T14:17:12"",
         ""Dddtsalida"":""2020-09-11T09:02:41"",
         ""Dddtcancel"":""2021-06-20T00:29:35"",
         ""Dddtechean"":""2025-03-09T00:00:00"",
         ""Cddtobs"":null
      },
      ""LIQ"":{
         ""Iliq"":""200014INTE035729Z"",
         ""Cliqdop"":""20260105741445"",
         ""Cliqeta"":""COB"",
         ""Mliq"":0,
         ""Mliqgar"":0,
         ""Dlippay"":null,
         ""Clipnomope"":null
      },
      ""LQA"":[
         {
            ""Iliq"":""200014INTE035729Z"",
            ""Nart"":1,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":521562.8959,
            ""Qlqacoefic"":15,
            ""Mlqa"":78234.4344
         },
         {
            ""Iliq"":""200014INTE035729Z"",
            ""Nart"":1,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":521562.8959,
            ""Qlqacoefic"":0,
            ""Mlqa"":0
         },
         {
            ""Iliq"":""200014INTE035729Z"",
            ""Nart"":2,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":150568.8373,
            ""Qlqacoefic"":15,
            ""Mlqa"":22585.3256
         },
         {
            ""Iliq"":""200014INTE035729Z"",
            ""Nart"":2,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":173154.163,
            ""Qlqacoefic"":15,
            ""Mlqa"":25973.1244
         }
      ]
   },
   {
      ""ART"":[
         {
            ""Iddt"":""200023INTE032941J"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""000"",
            ""Cartetamrc"":""NU"",
            ""Iespnce"":""3215.11.90.00.00"",
            ""Cartdesc"":""TINTA PARA IMPRENTA"",
            ""Cartpayori"":""MX"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":7,
            ""Martunitar"":52.0543,
            ""Cartuntdcl"":""01"",
            ""Qartuntdcl"":7,
            ""Cartuntest"":""01"",
            ""Qartuntest"":7,
            ""Qartkgrbrt"":7.7,
            ""Qartkgrnet"":7,
            ""Martfob"":364.38,
            ""Martfobdol"":364.38,
            ""Martfle"":null,
            ""Martass"":null,
            ""Martemma"":null,
            ""Martfrai"":0,
            ""Martajuinc"":null,
            ""Martajuded"":null,
            ""Martbasimp"":8924.72
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""200023028118F"",
         ""Cddtver"":1,
         ""Iddtext"":""200023028118F"",
         ""Iddt"":""200023INTE032941J"",
         ""Iext"":""5958617"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2020-09-10T14:27:33"",
         ""Dddtrectifa"":null,
         ""Cddtcirvis"":""R"",
         ""Qddttaxchg"":24.4929,
         ""Ista"":""5100"",
         ""Cddtbur"":""0023"",
         ""Cddtburdst"":""0023"",
         ""Cddtdep"":null,
         ""Cddtentrep"":null,
         ""Cddtage"":""14091"",
         ""Nddtimmioe"":""06826421093070"",
         ""Lddtnomioe"":""HA***O CI***S, SO*****D AN****A"",
         ""Cddtagr"":""01721159991080"",
         ""Lddtagr"":""AG****A AD*****A ES****L S. DE R.L."",
         ""Cddtpayori"":""MX"",
         ""Cddtpaidst"":""SV"",
         ""Lddtnomfod"":""AL****S IM*******S SA DE CV"",
         ""Cddtincote"":""CIP"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":null,
         ""Cddtdevass"":null,
         ""Cddttransp"":""14079"",
         ""Cddtmdetrn"":""T"",
         ""Cddtpaytrn"":""SV"",
         ""Nddtart"":1,
         ""Nddtdelai"":54,
         ""Dddtbae"":""2020-09-10T17:05:49"",
         ""Dddtsalida"":""2020-09-11T00:23:57"",
         ""Dddtcancel"":""2021-06-20T00:29:35"",
         ""Dddtechean"":""2025-03-10T00:00:00"",
         ""Cddtobs"":null
      },
      ""LIQ"":{
         ""Iliq"":""200023INTE032941J"",
         ""Cliqdop"":""20260105780715"",
         ""Cliqeta"":""COB"",
         ""Mliq"":0,
         ""Mliqgar"":0,
         ""Dlippay"":null,
         ""Clipnomope"":null
      },
      ""LQA"":[
         {
            ""Iliq"":""200023INTE032941J"",
            ""Nart"":1,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":8924.7229,
            ""Qlqacoefic"":10,
            ""Mlqa"":892.4723
         },
         {
            ""Iliq"":""200023INTE032941J"",
            ""Nart"":1,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":9817.1952,
            ""Qlqacoefic"":15,
            ""Mlqa"":1472.5793
         }
      ]
   },
   {
      ""ART"":[
         {
            ""Iddt"":""200023INTE032945N"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""000"",
            ""Cartetamrc"":""NU"",
            ""Iespnce"":""4821.10.00.00.00"",
            ""Cartdesc"":""ETIQUETAS PARA EMPAQUE"",
            ""Cartpayori"":""SV"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":0.0219,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":55000,
            ""Cartuntest"":""01"",
            ""Qartuntest"":17.6,
            ""Qartkgrbrt"":18.1,
            ""Qartkgrnet"":17.6,
            ""Martfob"":1203.75,
            ""Martfobdol"":1203.75,
            ""Martfle"":null,
            ""Martass"":null,
            ""Martemma"":null,
            ""Martfrai"":0,
            ""Martajuinc"":null,
            ""Martajuded"":null,
            ""Martbasimp"":29483.33
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""200023028121W"",
         ""Cddtver"":2,
         ""Iddtext"":""200023028121W"",
         ""Iddt"":""200023INTE032945N"",
         ""Iext"":""5958616"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2020-09-10T14:33:01"",
         ""Dddtrectifa"":""2020-09-10T14:42:11"",
         ""Cddtcirvis"":""R"",
         ""Qddttaxchg"":24.4929,
         ""Ista"":""5100"",
         ""Cddtbur"":""0023"",
         ""Cddtburdst"":""0023"",
         ""Cddtdep"":null,
         ""Cddtentrep"":null,
         ""Cddtage"":""14091"",
         ""Nddtimmioe"":""06826421093070"",
         ""Lddtnomioe"":""HA***O CI***S, SO*****D AN****A"",
         ""Cddtagr"":""01721159991080"",
         ""Lddtagr"":""AG****A AD*****A ES****L S. DE R.L."",
         ""Cddtpayori"":""SV"",
         ""Cddtpaidst"":""SV"",
         ""Lddtnomfod"":""AL****S IM*******S S.A. DE C.V."",
         ""Cddtincote"":""CIP"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":null,
         ""Cddtdevass"":null,
         ""Cddttransp"":""14079"",
         ""Cddtmdetrn"":""T"",
         ""Cddtpaytrn"":""SV"",
         ""Nddtart"":1,
         ""Nddtdelai"":54,
         ""Dddtbae"":""2020-09-10T17:28:36"",
         ""Dddtsalida"":""2020-09-11T00:24:01"",
         ""Dddtcancel"":""2021-06-20T00:29:35"",
         ""Dddtechean"":""2025-03-10T00:00:00"",
         ""Cddtobs"":null
      },
      ""LIQ"":{
         ""Iliq"":""200023INTE032945N"",
         ""Cliqdop"":""20260105781643"",
         ""Cliqeta"":""COB"",
         ""Mliq"":0,
         ""Mliqgar"":0,
         ""Dlippay"":null,
         ""Clipnomope"":null
      },
      ""LQA"":[
         {
            ""Iliq"":""200023INTE032945N"",
            ""Nart"":1,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":29483.3284,
            ""Qlqacoefic"":15,
            ""Mlqa"":4422.4993
         },
         {
            ""Iliq"":""200023INTE032945N"",
            ""Nart"":1,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":33905.8276,
            ""Qlqacoefic"":15,
            ""Mlqa"":5085.8741
         }
      ]
   },
   {
      ""ART"":[
         {
            ""Iddt"":""200016INTE009353M"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""000"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8609.00.00.00.00"",
            ""Cartdesc"":""CONTENEDOR  CMCU9011660 53 PIES  MARCA: JINDO  A?O: 2002  MATERIAL: ACERO  COLOR: MARR?N"",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""HN"",
            ""Cartpayprc"":""US"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":3000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":4500,
            ""Qartkgrnet"":4500,
            ""Martfob"":3000,
            ""Martfobdol"":3000,
            ""Martfle"":300,
            ""Martass"":45,
            ""Martemma"":null,
            ""Martfrai"":0,
            ""Martajuinc"":null,
            ""Martajuded"":null,
            ""Martbasimp"":81764.51
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""200016009026E"",
         ""Cddtver"":1,
         ""Iddtext"":""200016009026E"",
         ""Iddt"":""200016INTE009353M"",
         ""Iext"":""5981099"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2020-10-01T13:32:28"",
         ""Dddtrectifa"":null,
         ""Cddtcirvis"":""V"",
         ""Qddttaxchg"":24.4438,
         ""Ista"":""2000"",
         ""Cddtbur"":""0016"",
         ""Cddtburdst"":""0016"",
         ""Cddtdep"":null,
         ""Cddtentrep"":null,
         ""Cddtage"":""15848"",
         ""Nddtimmioe"":""06723521091050"",
         ""Lddtnomioe"":""CR****Y EQ******T DE HO*****S S"",
         ""Cddtagr"":""83001290091080"",
         ""Lddtagr"":""VE**A CU****S SA DE CV"",
         ""Cddtpayori"":""CN"",
         ""Cddtpaidst"":""US"",
         ""Lddtnomfod"":""CR****Y LI**R SE*****S IN*"",
         ""Cddtincote"":""FOB"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":""USD"",
         ""Cddtdevass"":""USD"",
         ""Cddttransp"":null,
         ""Cddtmdetrn"":null,
         ""Cddtpaytrn"":null,
         ""Nddtart"":1,
         ""Nddtdelai"":6,
         ""Dddtbae"":""2020-10-01T13:34:16"",
         ""Dddtsalida"":""2020-10-11T08:32:19"",
         ""Dddtcancel"":""2021-06-20T00:29:35"",
         ""Dddtechean"":""2021-04-01T00:00:00"",
         ""Cddtobs"":null
      },
      ""LIQ"":{
         ""Iliq"":""200016INTE009353M"",
         ""Cliqdop"":""20260106337583"",
         ""Cliqeta"":""COB"",
         ""Mliq"":0,
         ""Mliqgar"":0,
         ""Dlippay"":null,
         ""Clipnomope"":null
      },
      ""LQA"":[
         
      ]
   },
   {
      ""ART"":[
         {
            ""Iddt"":""210034INTE000263E"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""000"",
            ""Cartetamrc"":""NU"",
            ""Iespnce"":""4412.39.00.00.00"",
            ""Cartdesc"":""LAMINAS DE PLYWOODS 4MM,3MM,5.5MM"",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""NI"",
            ""Cartpayprc"":""NI"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1002,
            ""Martunitar"":10.978,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1002,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1002,
            ""Qartkgrbrt"":6813,
            ""Qartkgrnet"":6813,
            ""Martfob"":11000,
            ""Martfobdol"":11000,
            ""Martfle"":250,
            ""Martass"":135,
            ""Martemma"":null,
            ""Martfrai"":0,
            ""Martajuinc"":null,
            ""Martajuded"":null,
            ""Martbasimp"":274352.31
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""210034000250S"",
         ""Cddtver"":1,
         ""Iddtext"":""210034000250S"",
         ""Iddt"":""210034INTE000263E"",
         ""Iext"":""5958603"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2021-01-07T12:46:56"",
         ""Dddtrectifa"":null,
         ""Cddtcirvis"":""A"",
         ""Qddttaxchg"":24.0977,
         ""Ista"":""5100"",
         ""Cddtbur"":""0034"",
         ""Cddtburdst"":""0034"",
         ""Cddtdep"":null,
         ""Cddtentrep"":null,
         ""Cddtage"":""11640"",
         ""Nddtimmioe"":""06826421093070"",
         ""Lddtnomioe"":""HA***O CI***S, SO*****D AN****A"",
         ""Cddtagr"":""01721159991080"",
         ""Lddtagr"":""AG****A AD*****A ES****L S. DE R.L."",
         ""Cddtpayori"":""CN"",
         ""Cddtpaidst"":""NI"",
         ""Lddtnomfod"":""IN********S ES*********S DE NI******A,S.A."",
         ""Cddtincote"":""FOB"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":""USD"",
         ""Cddtdevass"":""USD"",
         ""Cddttransp"":""14306"",
         ""Cddtmdetrn"":""T"",
         ""Cddtpaytrn"":""NI"",
         ""Nddtart"":1,
         ""Nddtdelai"":48,
         ""Dddtbae"":""2021-01-08T13:56:39"",
         ""Dddtsalida"":""2021-01-09T00:24:09"",
         ""Dddtcancel"":""2021-06-20T00:29:36"",
         ""Dddtechean"":""2025-01-07T00:00:00"",
         ""Cddtobs"":""FACTURA AL CREDITO 30 DIAS TRANSFERENCIA BANCARIA PENDIENTE DE PAGO""
      },
      ""LIQ"":{
         ""Iliq"":""210034INTE000263E"",
         ""Cliqdop"":""21260100145602"",
         ""Cliqeta"":""COB"",
         ""Mliq"":0,
         ""Mliqgar"":0,
         ""Dlippay"":null,
         ""Clipnomope"":null
      },
      ""LQA"":[
         {
            ""Iliq"":""210034INTE000263E"",
            ""Nart"":1,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":274352.3145,
            ""Qlqacoefic"":10,
            ""Mlqa"":27435.2314
         },
         {
            ""Iliq"":""210034INTE000263E"",
            ""Nart"":1,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":301787.546,
            ""Qlqacoefic"":15,
            ""Mlqa"":45268.1319
         }
      ]
   },
   {
      ""ART"":[
         {
            ""Iddt"":""210025INTE008513K"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""025"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8716.39.00.00.00"",
            ""Cartdesc"":""CHASIS # CMCZ163591, 45 PIES, MARCA:CIMC, PLACA:26TLR30919, A?O:2006, SERIE O VIN:LJRC4626961008583."",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":3000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":3600,
            ""Qartkgrnet"":3600,
            ""Martfob"":3000,
            ""Martfobdol"":3000,
            ""Martfle"":300,
            ""Martass"":45,
            ""Martemma"":0,
            ""Martfrai"":0,
            ""Martajuinc"":0,
            ""Martajuded"":0,
            ""Martbasimp"":80321.48
         },
         {
            ""Iddt"":""210025INTE008513K"",
            ""Nart"":2,
            ""Carttyp"":""N"",
            ""Codbenef"":""025"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8716.39.00.00.00"",
            ""Cartdesc"":""CHASIS # CMCZ161464, 45 PIES, MARCA:LOADCRAFT, PLACA:T569231, A?O:2002, SERIE O VIN:CB782402."",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":3000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":3600,
            ""Qartkgrnet"":3600,
            ""Martfob"":3000,
            ""Martfobdol"":3000,
            ""Martfle"":300,
            ""Martass"":45,
            ""Martemma"":0,
            ""Martfrai"":0,
            ""Martajuinc"":0,
            ""Martajuded"":0,
            ""Martbasimp"":80321.48
         },
         {
            ""Iddt"":""210025INTE008513K"",
            ""Nart"":3,
            ""Carttyp"":""N"",
            ""Codbenef"":""025"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8716.39.00.00.00"",
            ""Cartdesc"":""CHASIS # CMCZ164921, 45 PIES, MARCA:SINGAMAS, PLACA:26TLR20359, A?O:2006, SERIE O VIN:L81CG45246T100233."",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":3000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":3600,
            ""Qartkgrnet"":3600,
            ""Martfob"":3000,
            ""Martfobdol"":3000,
            ""Martfle"":300,
            ""Martass"":45,
            ""Martemma"":0,
            ""Martfrai"":0,
            ""Martajuinc"":0,
            ""Martajuded"":0,
            ""Martbasimp"":80321.48
         },
         {
            ""Iddt"":""210025INTE008513K"",
            ""Nart"":4,
            ""Carttyp"":""N"",
            ""Codbenef"":""025"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8716.39.00.00.00"",
            ""Cartdesc"":""CHASIS # CMCZ160650, 45 PIES, MARCA:CHEETAH, PLACA:014195T, A?O:2003, SERIE O VIN:1S12GC40GEL648569."",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":3000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":3600,
            ""Qartkgrnet"":3600,
            ""Martfob"":3000,
            ""Martfobdol"":3000,
            ""Martfle"":300,
            ""Martass"":45,
            ""Martemma"":0,
            ""Martfrai"":0,
            ""Martajuinc"":0,
            ""Martajuded"":0,
            ""Martbasimp"":80321.48
         },
         {
            ""Iddt"":""210025INTE008513K"",
            ""Nart"":5,
            ""Carttyp"":""N"",
            ""Codbenef"":""025"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8716.39.00.00.00"",
            ""Cartdesc"":""CHASIS # CMCZ164480, 45 PIES, MARCA:SINGAMAS, PLACA:26TLR18205, A?O:2006, SERIE O VIN:L81CG45286T100705."",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":3000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":3600,
            ""Qartkgrnet"":3600,
            ""Martfob"":3000,
            ""Martfobdol"":3000,
            ""Martfle"":300,
            ""Martass"":45,
            ""Martemma"":0,
            ""Martfrai"":0,
            ""Martajuinc"":0,
            ""Martajuded"":0,
            ""Martbasimp"":80321.48
         },
         {
            ""Iddt"":""210025INTE008513K"",
            ""Nart"":6,
            ""Carttyp"":""N"",
            ""Codbenef"":""025"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8716.39.00.00.00"",
            ""Cartdesc"":""CHASIS # CMCZ166796, 45 PIES, MARCA:SINGAMAS, PLACA:28TLR11934, A?O:2008, SERIE O VIN:L81CG45288T107690."",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":3000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":3600,
            ""Qartkgrnet"":3600,
            ""Martfob"":3000,
            ""Martfobdol"":3000,
            ""Martfle"":300,
            ""Martass"":45,
            ""Martemma"":0,
            ""Martfrai"":0,
            ""Martajuinc"":0,
            ""Martajuded"":0,
            ""Martbasimp"":80321.48
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""210025007750H"",
         ""Cddtver"":1,
         ""Iddtext"":""210025007750H"",
         ""Iddt"":""210025INTE008513K"",
         ""Iext"":""6126467"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2021-03-10T21:44:21"",
         ""Dddtrectifa"":null,
         ""Cddtcirvis"":""V"",
         ""Qddttaxchg"":24.0124,
         ""Ista"":""5300"",
         ""Cddtbur"":""0025"",
         ""Cddtburdst"":""0025"",
         ""Cddtdep"":null,
         ""Cddtentrep"":null,
         ""Cddtage"":""15476"",
         ""Nddtimmioe"":""08450290091080"",
         ""Lddtnomioe"":""CR****Y LA**N AM****A SE*****S LL*"",
         ""Cddtagr"":""83001290091080"",
         ""Lddtagr"":""VE**A CU****S SA DE CV"",
         ""Cddtpayori"":""CN"",
         ""Cddtpaidst"":""SV"",
         ""Lddtnomfod"":""CR****Y LA**N AM****A SE*****S."",
         ""Cddtincote"":""FOB"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":""USD"",
         ""Cddtdevass"":""USD"",
         ""Cddttransp"":""14194"",
         ""Cddtmdetrn"":""T"",
         ""Cddtpaytrn"":""SV"",
         ""Nddtart"":6,
         ""Nddtdelai"":3,
         ""Dddtbae"":""2021-03-10T21:46:02"",
         ""Dddtsalida"":""2021-03-11T00:24:09"",
         ""Dddtcancel"":""2021-06-20T00:29:36"",
         ""Dddtechean"":""2021-06-21T00:00:00"",
         ""Cddtobs"":null
      },
      ""LIQ"":{
         ""Iliq"":""210025INTE008513K"",
         ""Cliqdop"":""21260102079072"",
         ""Cliqeta"":""COB"",
         ""Mliq"":0,
         ""Mliqgar"":0,
         ""Dlippay"":null,
         ""Clipnomope"":null
      },
      ""LQA"":[
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":1,
            ""Clqatax"":""GAR"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":21285.1917,
            ""Qlqacoefic"":0.03,
            ""Mlqa"":638.5558
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":1,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":80321.478,
            ""Qlqacoefic"":10,
            ""Mlqa"":8032.1478
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":1,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":88353.6258,
            ""Qlqacoefic"":15,
            ""Mlqa"":13253.0439
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":2,
            ""Clqatax"":""GAR"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":21285.1917,
            ""Qlqacoefic"":0.03,
            ""Mlqa"":638.5558
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":2,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":80321.478,
            ""Qlqacoefic"":10,
            ""Mlqa"":8032.1478
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":2,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":88353.6258,
            ""Qlqacoefic"":15,
            ""Mlqa"":13253.0439
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":3,
            ""Clqatax"":""GAR"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":21285.1917,
            ""Qlqacoefic"":0.03,
            ""Mlqa"":638.5558
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":3,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":80321.478,
            ""Qlqacoefic"":10,
            ""Mlqa"":8032.1478
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":3,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":88353.6258,
            ""Qlqacoefic"":15,
            ""Mlqa"":13253.0439
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":4,
            ""Clqatax"":""GAR"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":21285.1917,
            ""Qlqacoefic"":0.03,
            ""Mlqa"":638.5558
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":4,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":80321.478,
            ""Qlqacoefic"":10,
            ""Mlqa"":8032.1478
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":4,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":88353.6258,
            ""Qlqacoefic"":15,
            ""Mlqa"":13253.0439
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":5,
            ""Clqatax"":""GAR"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":21285.1917,
            ""Qlqacoefic"":0.03,
            ""Mlqa"":638.5558
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":5,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":80321.478,
            ""Qlqacoefic"":10,
            ""Mlqa"":8032.1478
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":5,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":88353.6258,
            ""Qlqacoefic"":15,
            ""Mlqa"":13253.0439
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":6,
            ""Clqatax"":""GAR"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":21285.1917,
            ""Qlqacoefic"":0.03,
            ""Mlqa"":638.5558
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":6,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":80321.478,
            ""Qlqacoefic"":10,
            ""Mlqa"":8032.1478
         },
         {
            ""Iliq"":""210025INTE008513K"",
            ""Nart"":6,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":88353.6258,
            ""Qlqacoefic"":15,
            ""Mlqa"":13253.0439
         }
      ]
   },
   {
      ""ART"":[
         {
            ""Iddt"":""210025INTE009365Z"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""025"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8716.39.00.00.00"",
            ""Cartdesc"":""CHASIS #  CMCZ160582   , 45 PIES, MARCA: CHEETAH , PLACA:  T825078 , A?O: 2003, SERIE O VIN: 1NNC04529TM280918"",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":3000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":3600,
            ""Qartkgrnet"":3600,
            ""Martfob"":3000,
            ""Martfobdol"":3000,
            ""Martfle"":300,
            ""Martass"":45,
            ""Martemma"":0,
            ""Martfrai"":0,
            ""Martajuinc"":0,
            ""Martajuded"":0,
            ""Martbasimp"":80325.16
         },
         {
            ""Iddt"":""210025INTE009365Z"",
            ""Nart"":2,
            ""Carttyp"":""N"",
            ""Codbenef"":""025"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8716.39.00.00.00"",
            ""Cartdesc"":""CHASIS #   CMCZ166829   , 45 PIES, MARCA: SINGAMAS , PLACA:  28TLR11967  , A?O: 2008, SERIE O VIN: L81CG45288T107723"",
            ""Cartpayori"":""CN"",
            ""Cartpayacq"":""SV"",
            ""Cartpayprc"":""SV"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":3000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":3600,
            ""Qartkgrnet"":3600,
            ""Martfob"":3000,
            ""Martfobdol"":3000,
            ""Martfle"":300,
            ""Martass"":45,
            ""Martemma"":0,
            ""Martfrai"":0,
            ""Martajuinc"":0,
            ""Martajuded"":0,
            ""Martbasimp"":80325.16
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""210025008525X"",
         ""Cddtver"":1,
         ""Iddtext"":""210025008525X"",
         ""Iddt"":""210025INTE009365Z"",
         ""Iext"":""6126566"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2021-03-17T20:14:02"",
         ""Dddtrectifa"":null,
         ""Cddtcirvis"":""V"",
         ""Qddttaxchg"":24.0135,
         ""Ista"":""5300"",
         ""Cddtbur"":""0025"",
         ""Cddtburdst"":""0025"",
         ""Cddtdep"":null,
         ""Cddtentrep"":null,
         ""Cddtage"":""15476"",
         ""Nddtimmioe"":""08450290091080"",
         ""Lddtnomioe"":""CR****Y LA**N AM****A SE*****S LL*"",
         ""Cddtagr"":""83001290091080"",
         ""Lddtagr"":""VE**A CU****S SA DE CV"",
         ""Cddtpayori"":""CN"",
         ""Cddtpaidst"":""SV"",
         ""Lddtnomfod"":""CR****Y LA**N AM****A SE*****S."",
         ""Cddtincote"":""FOB"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":""USD"",
         ""Cddtdevass"":""USD"",
         ""Cddttransp"":""14195"",
         ""Cddtmdetrn"":""T"",
         ""Cddtpaytrn"":""SV"",
         ""Nddtart"":2,
         ""Nddtdelai"":3,
         ""Dddtbae"":""2021-03-17T20:14:58"",
         ""Dddtsalida"":""2021-03-18T00:23:55"",
         ""Dddtcancel"":""2021-06-20T00:29:35"",
         ""Dddtechean"":""2021-06-28T00:00:00"",
         ""Cddtobs"":null
      },
      ""LIQ"":{
         ""Iliq"":""210025INTE009365Z"",
         ""Cliqdop"":""21260102303106"",
         ""Cliqeta"":""COB"",
         ""Mliq"":0,
         ""Mliqgar"":0,
         ""Dlippay"":null,
         ""Clipnomope"":null
      },
      ""LQA"":[
         {
            ""Iliq"":""210025INTE009365Z"",
            ""Nart"":1,
            ""Clqatax"":""GAR"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":21286.1667,
            ""Qlqacoefic"":0.03,
            ""Mlqa"":638.585
         },
         {
            ""Iliq"":""210025INTE009365Z"",
            ""Nart"":1,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":80325.1575,
            ""Qlqacoefic"":10,
            ""Mlqa"":8032.5158
         },
         {
            ""Iliq"":""210025INTE009365Z"",
            ""Nart"":1,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":88357.6733,
            ""Qlqacoefic"":15,
            ""Mlqa"":13253.651
         },
         {
            ""Iliq"":""210025INTE009365Z"",
            ""Nart"":2,
            ""Clqatax"":""GAR"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":21286.1667,
            ""Qlqacoefic"":0.03,
            ""Mlqa"":638.585
         },
         {
            ""Iliq"":""210025INTE009365Z"",
            ""Nart"":2,
            ""Clqatax"":""DAI"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":80325.1575,
            ""Qlqacoefic"":10,
            ""Mlqa"":8032.5158
         },
         {
            ""Iliq"":""210025INTE009365Z"",
            ""Nart"":2,
            ""Clqatax"":""ISV"",
            ""Clqatyp"":""E"",
            ""Mlqabas"":88357.6733,
            ""Qlqacoefic"":15,
            ""Mlqa"":13253.651
         }
      ]
   },
   {
      ""ART"":[
         {
            ""Iddt"":""210004INTE039223J"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""000"",
            ""Cartetamrc"":""EN"",
            ""Iespnce"":""2309.90.90.00.09"",
            ""Cartdesc"":""HIDROLIZADO DE PESCADO PARA CONSUMO ANIMAL"",
            ""Cartpayori"":""EC"",
            ""Cartpayacq"":""EC"",
            ""Cartpayprc"":""EC"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":108,
            ""Martunitar"":990.0248,
            ""Cartuntdcl"":""04"",
            ""Qartuntdcl"":119.74,
            ""Cartuntest"":""01"",
            ""Qartuntest"":119743,
            ""Qartkgrbrt"":126010,
            ""Qartkgrnet"":119743,
            ""Martfob"":118545.57,
            ""Martfobdol"":118545.57,
            ""Martfle"":9408,
            ""Martass"":1778.18,
            ""Martemma"":null,
            ""Martfrai"":0,
            ""Martajuinc"":null,
            ""Martajuded"":null,
            ""Martbasimp"":3117051.78
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""210004037365J"",
         ""Cddtver"":1,
         ""Iddtext"":""210004037365J"",
         ""Iddt"":""210004INTE039223J"",
         ""Iext"":""6072399"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2021-03-19T10:45:48"",
         ""Dddtrectifa"":null,
         ""Cddtcirvis"":""V"",
         ""Qddttaxchg"":24.0269,
         ""Ista"":""7000"",
         ""Cddtbur"":""0004"",
         ""Cddtburdst"":""0004"",
         ""Cddtdep"":""DT24"",
         ""Cddtentrep"":""DA02"",
         ""Cddtage"":""13295"",
         ""Nddtimmioe"":""52564421091050"",
         ""Lddtnomioe"":""AQ*****D SA DE CV"",
         ""Cddtagr"":""40894010091050"",
         ""Lddtagr"":""ME**A GA******E S DE  R L"",
         ""Cddtpayori"":""EC"",
         ""Cddtpaidst"":""EC"",
         ""Lddtnomfod"":""FO*****X S.A."",
         ""Cddtincote"":""FOB"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":""USD"",
         ""Cddtdevass"":""USD"",
         ""Cddttransp"":""15587"",
         ""Cddtmdetrn"":""M"",
         ""Cddtpaytrn"":""LR"",
         ""Nddtart"":1,
         ""Nddtdelai"":12,
         ""Dddtbae"":""2021-03-19T10:49:06"",
         ""Dddtsalida"":""2021-03-23T10:25:42"",
         ""Dddtcancel"":""2021-06-20T00:29:35"",
         ""Dddtechean"":""2022-03-19T00:00:00"",
         ""Cddtobs"":null
      },
      ""LIQ"":{
         ""Iliq"":""210004INTE039223J"",
         ""Cliqdop"":""21260102352176"",
         ""Cliqeta"":""COB"",
         ""Mliq"":0,
         ""Mliqgar"":0,
         ""Dlippay"":null,
         ""Clipnomope"":null
      },
      ""LQA"":[
         
      ]
   },
   {
      ""ART"":[
         {
            ""Iddt"":""210016INTE003718M"",
            ""Nart"":1,
            ""Carttyp"":""N"",
            ""Codbenef"":""000"",
            ""Cartetamrc"":""US"",
            ""Iespnce"":""8703.23.69.99.00"",
            ""Cartdesc"":""AUTOMOVIL MARCA: SUZUKI, A?O: 2007, TIPO: CAMIONETA, MODELO: GRAND VITARA, MOTOR: N/V, COLOR: GRIS, PLACA: 268VCC, VIN: JS3TE94V374202327."",
            ""Cartpayori"":""JP"",
            ""Cartpayacq"":null,
            ""Cartpayprc"":""GT"",
            ""Iddtapu"":null,
            ""Nartapu"":null,
            ""Qartbul"":1,
            ""Martunitar"":7000,
            ""Cartuntdcl"":""15"",
            ""Qartuntdcl"":1,
            ""Cartuntest"":""15"",
            ""Qartuntest"":1,
            ""Qartkgrbrt"":1,
            ""Qartkgrnet"":1,
            ""Martfob"":7000,
            ""Martfobdol"":7000,
            ""Martfle"":null,
            ""Martass"":null,
            ""Martemma"":null,
            ""Martfrai"":null,
            ""Martajuinc"":null,
            ""Martajuded"":null,
            ""Martbasimp"":168188.3
         }
      ],
      ""DDT"":{
         ""Iddtextr"":""210016003561D"",
         ""Cddtver"":1,
         ""Iddtext"":""210016003561D"",
         ""Iddt"":""210016INTE003718M"",
         ""Iext"":""F9A1084172"",
         ""Cddteta"":""CANC"",
         ""Dddtoficia"":""2021-03-19T11:17:43"",
         ""Dddtrectifa"":null,
         ""Cddtcirvis"":""V"",
         ""Qddttaxchg"":24.0269,
         ""Ista"":""5800"",
         ""Cddtbur"":""0016"",
         ""Cddtburdst"":null,
         ""Cddtdep"":null,
         ""Cddtentrep"":null,
         ""Cddtage"":""16141"",
         ""Nddtimmioe"":""11000000000000"",
         ""Lddtnomioe"":""IM*******R EV*****L "",
         ""Cddtagr"":null,
         ""Lddtagr"":null,
         ""Cddtpayori"":""JP"",
         ""Cddtpaidst"":""GT"",
         ""Lddtnomfod"":""MI***L AN**L FE***E MA*****Z"",
         ""Cddtincote"":""CIF"",
         ""Cddtdevfob"":""USD"",
         ""Cddtdevfle"":null,
         ""Cddtdevass"":null,
         ""Cddttransp"":null,
         ""Cddtmdetrn"":null,
         ""Cddtpaytrn"":null,
         ""Nddtart"":1,
         ""Nddtdelai"":3,
         ""Dddtbae"":""2021-03-25T12:45:32"",
         ""Dddtsalida"":""2021-03-25T12:45:32"",
         ""Dddtcancel"":""2021-06-20T00:29:35"",
         ""Dddtechean"":""2021-06-28T00:00:00"",
         ""Cddtobs"":""MIGUEL ANGEL FELIPE MARTINEZ, PASAPORTE: G39218443.""
      },
      ""LIQ"":{
         ""Iliq"":""210016INTE003718M"",
         ""Cliqdop"":""21260102354583"",
         ""Cliqeta"":""COB"",
         ""Mliq"":636.71,
         ""Mliqgar"":0,
         ""Dlippay"":""2021-03-25T12:53:12"",
         ""Clipnomope"":""100000007060009""
      },
      ""LQA"":[
         {
            ""Iliq"":""210016INTE003718M"",
            ""Nart"":0,
            ""Clqatax"":""STD"",
            ""Clqatyp"":""P"",
            ""Mlqabas"":120.1345,
            ""Qlqacoefic"":100,
            ""Mlqa"":120.1345
         },
         {
            ""Iliq"":""210016INTE003718M"",
            ""Nart"":1,
            ""Clqatax"":""9A1"",
            ""Clqatyp"":""P"",
            ""Mlqabas"":516.5784,
            ""Qlqacoefic"":100,
            ""Mlqa"":516.5784
         }
      ]
   }
]";

        string Nddtimmioe = "06826421093070";

        // Llamar a la función para procesar el JSON y el Nddtimmioe
        var datosProcesadosJson = ProcesarJsonDatos(jsonDatos, Nddtimmioe) as string;

        if (string.IsNullOrEmpty(datosProcesadosJson))
        {
            _logger.LogWarning("El procesamiento de JSON no devolvió resultados.");
            ViewBag.ResultadoProcesado = null;
        }
        else
        {
            // Deserializar el JSON procesado en una lista de DeclaracionModels
            var datosProcesados = JsonSerializer.Deserialize<List<DeclaracionModels>>(datosProcesadosJson);

            // Log para verificar el resultado procesado
            _logger.LogInformation($"Resultado procesado: {datosProcesadosJson}");

            // Mostrar el resultado en la vista
            ViewBag.ResultadoProcesado = datosProcesados;
        }

        return View("Index");
    }




    [HttpPost]
    public async Task<IActionResult> BuscarRegistro(string fechaConsultar, string Nddtimmioe)
    {
        if (string.IsNullOrWhiteSpace(fechaConsultar))
        {
            ViewBag.Message = "Debe ingresar una fecha válida.";
            return View("Index", Enumerable.Empty<ApiData>());
        }

        // Llamar a GetApiData para obtener el registro específico
        var resultado = await GetApiData(fechaConsultar) as OkObjectResult;
        var apiData = resultado?.Value as ApiData;

        if (apiData == null)
        {
            ViewBag.Message = "No se encontraron registros para la fecha especificada.";
            return View("Index", Enumerable.Empty<ApiData>());
        }

        // Llamar a la función que procesará JsonDatos y pasará el parámetro Nddtimmioe
      //  _logger.LogWarning($"antes de pasarlo a ProcesarJsonDatos: {apiData.JsonDatos}");
        var datosProcesadosJson = ProcesarJsonDatos(apiData.JsonDatos,Nddtimmioe) as string;
        _logger.LogWarning($"despues de pasarlo a ProcesarJsonDatos: {datosProcesadosJson}");
        if (string.IsNullOrEmpty(datosProcesadosJson))
        {
            _logger.LogWarning("El procesamiento de JSON no devolvió resultados.");
            ViewBag.ResultadoProcesado = null;
        }
        else
        {
            // Deserializar el JSON procesado en una lista de DeclaracionModels
            var datosProcesados = JsonSerializer.Deserialize<List<DeclaracionModels>>(datosProcesadosJson);

            // Log para verificar el resultado procesado
            _logger.LogInformation($"Resultado procesado: {datosProcesadosJson}");

            // Mostrar el resultado en la vista
            ViewBag.ResultadoProcesado = datosProcesados;
        }

        // Devuelve una lista con el único resultado para que la vista funcione sin cambios significativos
        return View("Index", new List<ApiData> { apiData });
    }

    private object ProcesarJsonDatos(string jsonDatos, string Nddtimmioe)
    {
        try
        {
           
            // Configurar opciones de deserialización para ignorar mayúsculas y minúsculas
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignora mayúsculas y minúsculas
            };


            
            var declaraciones = JsonSerializer.Deserialize<List<DeclaracionModels>>(jsonDatos, opciones);

            var totalDDTConNddtimmioe = declaraciones.Count(d => d.DDT != null && d.DDT.Nddtimmioe != null);
            var totalCoincidencias = declaraciones.Count(d => d.DDT != null && d.DDT.Nddtimmioe == Nddtimmioe);

            _logger.LogInformation($"Total de declaraciones con DDT y Nddtimmioe no nulo: {totalDDTConNddtimmioe}");
            _logger.LogInformation($"Total de coincidencias exactas con Nddtimmioe '{Nddtimmioe}': {totalCoincidencias}");

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

            // Filtrar las declaraciones que coinciden con el Nddtimmioe
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




    /*private object ProcesarJsonDatos(string jsonDatos, string Nddtimmioe)
    {
        //_logger.LogWarning($"Iniciando procesamiento en ProcesarJsonDatos. JSON: {jsonDatos ?? "null"}");

        try
        {
            _logger.LogWarning($" despues try Iniciando procesamiento en ProcesarJsonDatos. JSON: {jsonDatos ?? "null"}");
            // Deserializar el JSON en una lista de objetos Declaracion
            var declaraciones = JsonSerializer.Deserialize<List<DeclaracionModels>>(jsonDatos);
            
            if (declaraciones == null || declaraciones.Count == 0)
            {
                _logger.LogWarning("No se encontraron declaraciones en el JSON proporcionado.");
                return null;
            }

            // Filtrar las declaraciones que coinciden con el Nddtimmioe
            var declaracionesFiltradas = declaraciones
                .Where(d => d.DDT != null && d.DDT.Nddtimmioe == Nddtimmioe)
                .ToList();

            if (declaracionesFiltradas.Count == 0)
            {
                _logger.LogWarning($"No se encontraron declaraciones con Nddtimmioe = {Nddtimmioe}");
                return null;
            }

            _logger.LogInformation($"Se encontraron {declaracionesFiltradas.Count} declaraciones con Nddtimmioe = {Nddtimmioe}");

            // Opcional: puedes transformar el resultado en un JSON de salida si necesitas
            var resultadoJson = JsonSerializer.Serialize(declaracionesFiltradas, new JsonSerializerOptions { WriteIndented = true });

            // Retornar el resultado en el formato deseado (por ejemplo, JSON o como objeto)
            return resultadoJson;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al procesar JsonDatos: {ex.Message}");
            return null;
        }
    }*/


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
                FechaAConsultar = DateTime.Parse(declaracionObj.FechaAConsultar),
                CuentaDeclaraciones = declaracionObj.CuentaDeclaraciones,
                JsonDatos = datosDescomprimidos  
            };

            // Guardar el objeto en la base de datos
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