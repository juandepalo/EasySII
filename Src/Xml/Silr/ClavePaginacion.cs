using EasySII.Xml.Sii;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Clave de paginación.
    /// </summary>
    public class ClavePaginacion
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
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaExpedicionFacturaEmisor", Namespace = Settings.NamespaceSii)]
        public string FechaExpedicionFacturaEmisor { get; set; }

        /// <summary>
        /// Constructor clase IDFactura.
        /// </summary>
        public ClavePaginacion()
        {
            IDEmisorFactura = new IDEmisorFactura();
        }
    }
}
