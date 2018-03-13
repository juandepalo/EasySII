using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Clase base que representa un desglose: El desglose puede ser de factura,
    /// de prestación de servicios o de entrega de bienes.
    /// </summary>
    public class DesgloseF
    {

        /// <summary>
        /// Operación sujeta.
        /// </summary>
        [XmlElement("Sujeta", Namespace = Settings.NamespaceSii)]
        public Sujeta Sujeta { get; set; }

        /// <summary>
        /// Operación no sujeta.
        /// </summary>
        [XmlElement("NoSujeta", Namespace = Settings.NamespaceSii)]
        public NoSujeta NoSujeta { get; set; }

        /// <summary>
        /// Desglose de factura.
        /// </summary>
        public DesgloseF()
        {
        }
        
    }
}
