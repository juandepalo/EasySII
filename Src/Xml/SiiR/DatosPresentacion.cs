using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Datos presentación lote asignado por la AEAT.
    /// </summary>
    [Serializable]
    [XmlRoot("DatosPresentacion")]
    public class DatosPresentacion
    {

        /// <summary>
        /// NIF Presentador.
        /// </summary>
        [XmlElement("NIFPresentador", Namespace = Settings.NamespaceSii)]
        public string NIFPresentador { get; set; }

        /// <summary>
        /// Timestamp Presentacion.
        /// Timestamp asociado a la petición enviada
        /// </summary>
        [XmlElement("TimestampPresentacion", Namespace = Settings.NamespaceSii)]
        public string TimestampPresentacion { get; set; }

        /// <summary>
        /// Código CSV asignado al envío. 
        /// Código seguro de verificación asociado a la petición enviada.
        /// </summary>
        [XmlElement("CSV", Namespace = Settings.NamespaceSii)]
        public string CSV { get; set; }

        /// <summary>
        /// Representación textual de esta instancia de DatosPresentacion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (NIFPresentador ?? "") + ", " + 
                TimestampPresentacion;
        }

    }
}
