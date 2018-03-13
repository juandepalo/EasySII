using EasySII.Xml.Silr;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Datos DUA.
    /// </summary>
    public class Aduanas
    {

        /// <summary>
        /// Número de DUA. Alfanumérico (40).
        /// </summary>
        [XmlElement("IDFacturaAgrupada")]
        public string NumeroDUA { get; set; }

        /// <summary>
        /// Fecha de registro contable del DUA. 
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaRegContableDUA", Namespace = Settings.NamespaceSii)]
        public string FechaRegContableDUA { get; set; }


        /// <summary>
        /// Constructor Aduanas.
        /// </summary>
        public Aduanas()
        {
        }
    }
}
