using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Tipo desglose: NIF español no empezado con 'N' => DesgloseFactura != null / DesgloseTipoOperacion == null
    /// NIF no español o comenzado por 'N' => DesgloseTipoOperacion != null /  DesgloseFactura == null
    /// </summary>
    public class TipoDesglose
    {

        /// <summary>
        /// Desglose Factura
        /// </summary>
        [XmlElement("DesgloseFactura", Namespace = Settings.NamespaceSii)]
        public DesgloseFactura DesgloseFactura { get; set; }

        /// <summary>
        /// Desglose Desglose Tipo Operacion
        /// </summary>
        [XmlElement("DesgloseTipoOperacion", Namespace = Settings.NamespaceSii)]
        public DesgloseTipoOperacion DesgloseTipoOperacion { get; set; }

        /// <summary>
        /// Constructor clase TipoDesglose
        /// </summary>
        public TipoDesglose()
        {
        }

    }
}
