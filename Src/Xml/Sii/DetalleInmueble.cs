using System;
using System.Xml.Serialization;


namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Bloque detalle inmueble.
    /// </summary>
    [Serializable]
    [XmlRoot("DetalleInmueble", Namespace = Settings.NamespaceSii)]
    public class DetalleInmueble
    {
        /// <summary>
        /// Identificador que especifica la situación del inmueble. Númerico según lista L6:
        /// <para></para>
        /// </summary>
        [XmlElement("SituacionInmueble")]
        public string SituacionInmueble { get; set; }

        /// <summary>
        /// Referencia catastral del inmueble. Alfanumérico(25).
        /// </summary>
        [XmlElement("ReferenciaCatastral")]
        public string ReferenciaCatastral { get; set; }
    }
}
