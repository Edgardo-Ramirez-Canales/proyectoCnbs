using System.Text;
using System.Xml.Serialization;
using System.IO.Compression;


namespace proyectoCnbs.Services
{
    public class XmlProcessor
    {
        private readonly ILogger<XmlProcessor> _logger;

        public XmlProcessor(ILogger<XmlProcessor> logger)
        {
            _logger = logger;
        }
        
        public Declaracion? DeserializeXml(string xmlData)
        {
           // _logger.LogInformation($"XML Data Received: {xmlData}");

            XmlSerializer serializer = new XmlSerializer(typeof(Declaracion), new XmlRootAttribute("CNBS-INFO-R"));
            using (StringReader reader = new StringReader(xmlData))
            {
                var result = serializer.Deserialize(reader) as Declaracion;
                if (result == null)
                {
                    _logger.LogError("Error deserializing XML: Result is null");
                    return null;
                }
                return result;
            }
        }

        // Decomprime una cadena de texto (comprimida con Compress)       
        public static async System.Threading.Tasks.Task<string> DecompressAsync(string
        compressedString)
        {
            try
            {
                using (MemoryStream msi = new
                MemoryStream(Convert.FromBase64String(compressedString)))
                using (MemoryStream mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    {
                        await gs.CopyToAsync(mso);
                    }
                    return Encoding.UTF8.GetString(mso.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // Comprime una cadena de texto   
        private static string Compress(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            return Convert.ToBase64String(compressed);
        }
    }
}

