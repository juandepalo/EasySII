using EasySII.Xml.Sii;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Clave de paginación para las Operaciones Trascendencia Tributaria.
    /// </summary>
    public class ClavePaginacionOTT
    {

        /// <summary>
        /// Contraparte de la operacion.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSii)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Clave de la Operacion (para las operaciones de seguros).
        /// </summary>
        [XmlElement("ClaveOperacion", Namespace = Settings.NamespaceSii)]
        public string ClaveOperacion { get; set; }

        /// <summary>
        /// Constructor clase IDFactura.
        /// </summary>
        public ClavePaginacionOTT()
        {
            Contraparte = new Contraparte();
        }
    }
}
