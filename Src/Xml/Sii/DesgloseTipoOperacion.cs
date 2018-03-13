using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Desglose de tipo de operación. Hay que informarlo para los 
    /// nif no nacionales o comenzados por N.
    /// </summary>
    public class DesgloseTipoOperacion
    {

        /// <summary>
        /// Prestacion Servicios.
        /// </summary>
        [XmlElement("PrestacionServicios", Namespace = Settings.NamespaceSii)]
        public PrestacionServicios PrestacionServicios { get; set; }

        /// <summary>
        /// Entrega.
        /// </summary>
        [XmlElement("Entrega", Namespace = Settings.NamespaceSii)]
        public Entrega Entrega { get; set; }

        /// <summary>
        /// Desglose de tipo de operación.
        /// </summary>
        public DesgloseTipoOperacion()
        {
        }
    }
}
