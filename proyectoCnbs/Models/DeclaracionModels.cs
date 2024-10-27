using System.Collections.Generic;
namespace proyectoCnbs.Models
{
    public class DeclaracionModels
    {
        public List<Articulo> ART { get; set; }
        public DetalleDDT DDT { get; set; }
        public Liquidacion LIQ { get; set; }
        public List<LiquidacionArticulo> LQA { get; set; }
    }

    public class Articulo
    {
        public string Iddt { get; set; }
        public int Nart { get; set; }
        public string Cartdesc { get; set; }
        public double Martfob { get; set; }
        // Otros campos del artículo...
    }

    public class DetalleDDT
    {
        public string Iddtextr { get; set; }
        public string Nddtimmioe { get; set; }
        public string Dddtoficia { get; set; }
        // Otros campos de la declaración (DDT)...
    }

    public class Liquidacion
    {
        public string Iliq { get; set; }
        public string Cliqdop { get; set; }
        // Otros campos de la liquidación...
    }

    public class LiquidacionArticulo
    {
        public string Iliq { get; set; }
        public int Nart { get; set; }
        public string Clqatax { get; set; }
        public double Mlqa { get; set; }
        // Otros campos de la liquidación de artículos...
    }
}
