using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Sujeta
    /// </summary>
    public class Sujeta
    {

		/// <summary>
		/// Exenta
		/// </summary>
		[XmlElement("Exenta", Namespace = Settings.NamespaceSii)]
		public Exenta Exenta { get; set; }

        /// <summary>
        /// Exenta a partir de la versión 1.1
        /// </summary>
        [XmlElement("Exenta", Namespace = Settings.NamespaceSii)]
        public List<Exenta> DetalleExenta { get; set; }

        /// <summary>
        /// No exenta
        /// </summary>
        [XmlElement("NoExenta", Namespace = Settings.NamespaceSii)]
        public NoExenta NoExenta { get; set; }
 

        /// <summary>
        /// Constructor clase Sujeta.
        /// </summary>
        public Sujeta()
        {
            NoExenta = new NoExenta();
        }
    }
}
