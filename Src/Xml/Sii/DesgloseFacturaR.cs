using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Desglose de factura recibida.
    /// </summary>
    public class DesgloseFacturaR
    {

        /// <summary>
        /// Inversion Sujet oPasivo
        /// </summary>
        [XmlElement("InversionSujetoPasivo", Namespace = Settings.NamespaceSii)]
        public DesgloseIVA InversionSujetoPasivo { get; set; }

        /// <summary>
        /// Desglose IVA
        /// </summary>
        [XmlElement("DesgloseIVA", Namespace = Settings.NamespaceSii)]
        public DesgloseIVA DesgloseIVA { get; set; }

        /// <summary>
        /// Constructor clase TipoDesglose
        /// </summary>
        public DesgloseFacturaR()
        {
        }

    }
}
