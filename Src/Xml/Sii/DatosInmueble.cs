using System;
using System.Xml.Serialization;


namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Bloque datos inmueble.
    /// </summary>
    [Serializable]
    [XmlRoot("DatosInmueble", Namespace = Settings.NamespaceSii)]
    public class DatInmueble
    {
        /// <summary>
        /// Detalle Inmueble.
        /// </summary>
        [XmlElement("DetalleInmueble", Namespace = Settings.NamespaceSii)]
        public DetalleInmueble DetalleInmueble { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public DatInmueble()
        {
            DetalleInmueble = new DetalleInmueble();
        }
    }
}
