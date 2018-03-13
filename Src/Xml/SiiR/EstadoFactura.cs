using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Datos presentación lote asignado por la AEAT.
    /// </summary>
    [Serializable]
    [XmlRoot("EstadoFactura")]
    public class EstadoFactura
    {

        /// <summary>
        /// Timestamp de la última modificación realizada a la factura.
        /// </summary>
        [XmlElement("TimestampUltimaModificacion", Namespace = Settings.NamespaceSiiRQ)]
        public string TimestampUltimaModificacion { get; set; }

        /// <summary>
        /// Estado de la factura en el registro.
        /// </summary>
        [XmlElement("EstadoRegistro", Namespace = Settings.NamespaceSiiRQ)]
        public string EstadoRegistro { get; set; }


        /// <summary>
        /// Representación textual de esta instancia de DatosPresentacion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (TimestampUltimaModificacion ?? "") + ", " +
                EstadoRegistro;
        }

    }
}
