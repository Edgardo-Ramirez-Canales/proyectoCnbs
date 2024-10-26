using System;
using System.Xml.Serialization;

[XmlRoot("CNBS-INFO-R")]
public class Declaracion
{
    [XmlAttribute("nroTransaccion")]
    public required string NroTransaccion { get; set; }

    [XmlAttribute("fechaHoraTrn")]
    public required string FechaHoraTrn { get; set; }

    [XmlElement("fechaAConsultar")]
    public required string FechaAConsultar { get; set; }

    [XmlElement("cuentaDeclaraciones")]
    public required int CuentaDeclaraciones { get; set; }

    [XmlElement("datosComprimidos")]
    public required string DatosComprimidos { get; set; }
}
