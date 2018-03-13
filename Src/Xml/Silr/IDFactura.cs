using EasySII.Xml.Sii;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Identificador de factura.
    /// </summary>
    public class IDFactura
    {

        /// <summary>
        /// Identificador del emisor.
        /// </summary>
        [XmlElement("IDEmisorFactura", Namespace = Settings.NamespaceSii)]
        public IDEmisorFactura IDEmisorFactura { get; set; }

        /// <summary>
        /// Número de factura.
        /// </summary>
        [XmlElement("NumSerieFacturaEmisor", Namespace = Settings.NamespaceSii)]
        public string NumSerieFacturaEmisor { get; set; }

        /// <summary>
        /// Número de factura final para asientos resumen.
        /// </summary>
        [XmlElement("NumSerieFacturaEmisorResumenFin", Namespace = Settings.NamespaceSii)]
        public string NumSerieFacturaEmisorResumenFin { get; set; }

        /// <summary>
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaExpedicionFacturaEmisor", Namespace = Settings.NamespaceSii)]
        public string FechaExpedicionFacturaEmisor { get; set; }

        /// <summary>
        /// Constructor clase IDFactura.
        /// </summary>
        public IDFactura()
        {
            IDEmisorFactura = new IDEmisorFactura();
        }
    }
}
