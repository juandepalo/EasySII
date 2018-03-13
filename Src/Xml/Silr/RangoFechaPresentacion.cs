using EasySII.Xml.Sii;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Rango entre una fecha incial y una fecha final.
    /// </summary>
    public class RangoFechaPresentacion
    {
  
        /// <summary>
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("Desde", Namespace = Settings.NamespaceSii)]
        public string Desde { get; set; }

        /// <summary>
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("Hasta", Namespace = Settings.NamespaceSii)]
        public string Hasta { get; set; }

        /// <summary>
        /// Constructor clase RangoFechaPresentacion.
        /// </summary>
        public RangoFechaPresentacion()
        {
        }
    }
}
