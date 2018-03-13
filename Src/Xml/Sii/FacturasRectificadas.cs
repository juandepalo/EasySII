using EasySII.Xml.Silr;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Facturas Agrupadas.
    /// </summary>
    public class FacturasRectificadas
    {

        /// <summary>
        /// ID Factura Rectificada.
        /// </summary>
        [XmlElement("IDFacturaRectificada")]
        public IDFactura IDFacturaRectificada { get; set; }

        /// <summary>
        /// Constructor FacturasRectificadas.
        /// </summary>
        public FacturasRectificadas()
        {
        }
    }
}
