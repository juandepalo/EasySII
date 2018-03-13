using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Desglose IVA.
    /// </summary>
    public class DesgloseIVA
    {

        /// <summary>
        /// Detalle IVA.
        /// </summary>
        [XmlElement("DetalleIVA")]
        public List<DetalleIVA> DetalleIVA { get; set; }

        /// <summary>
        /// Constructor DesgloseIVA.
        /// </summary>
        public DesgloseIVA()
        {
            DetalleIVA = new List<DetalleIVA>();
        }
    }
}
