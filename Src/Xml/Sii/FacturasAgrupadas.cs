using EasySII.Xml.Silr;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Facturas Agrupadas.
    /// </summary>
    public class FacturasAgrupadas
    {

        /// <summary>
        /// ID Factura Agrupada.
        /// </summary>
        [XmlElement("IDFacturaAgrupada")]
        public IDFactura IDFacturaAgrupada { get; set; }
  

        /// <summary>
        /// Constructor FacturasAgrupadas.
        /// </summary>
        public FacturasAgrupadas()
        {
        }
    }
}
