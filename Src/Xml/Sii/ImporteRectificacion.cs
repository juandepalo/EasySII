using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Elemento de importe de rectificación.
    /// </summary>
    [Serializable]
    public class ImporteRectificacion
    {
        /// <summary>
        /// Base Rectificada factura.
        /// </summary>
        [XmlElement("BaseRectificada", Namespace = Settings.NamespaceSii)]
        public string BaseRectificada { get; set; }

        /// <summary>
        ///Cuota repercutida o soportada de la factura/facturas sustituidas
        /// </summary>
        [XmlElement("CuotaRectificada", Namespace = Settings.NamespaceSii)]
        public string CuotaRectificada { get; set; }

        /// <summary>
        /// Cuota recargo de  equivalencia de la factura/facturas sustituidas
        /// </summary>
        [XmlElement("CuotaRecargoRectificado", Namespace = Settings.NamespaceSii)]
        public string CuotaRecargoRectificado { get; set; }
    }
}
