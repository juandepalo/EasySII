using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Respuesta envío Libro de registro de Facturas expedidas.
    /// </summary>
    [Serializable]
    [XmlRoot("RespuestaLRFacturasEmitidas", Namespace = Settings.NamespaceSiiR)]
    public class RespuestaLRF
    {
        /// <summary>
        /// Código CSV asignado al envío. 
        /// Código seguro de verificación asociado a la petición enviada.
        /// </summary>
        [XmlElement("CSV", Namespace = Settings.NamespaceSiiR)]
        public string CSV { get; set; }

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("DatosPresentacion", Namespace = Settings.NamespaceSiiR)]
        public DatosPresentacion DatosPresentacion { get; set; }


        /// <summary>
        /// Datos de cabecera.
        /// Cabecera equivalente a la enviada en la petición de suministro / baja
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSiiR)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Estado Envio.
        /// Campo que especifica si el conjunto de facturas ha sido registrada
        /// correctamente, o ha sido rechazada, o se ha aceptado de forma parcial (lista L14).
        /// </summary>
        [XmlElement("EstadoEnvio", Namespace = Settings.NamespaceSiiR)]
        public string EstadoEnvio { get; set; }

        /// <summary>
        /// Lista de facturas con un límite de 10.000.
        /// </summary>
        [XmlElement("RespuestaLinea", Namespace = Settings.NamespaceSiiR)]
        public List<RespuestaLinea> RespuestaLinea { get; set; }

        /// <summary>
        /// Representación textual de esta instancia de RespuestaLinea.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (CSV??"") +", "+ EstadoEnvio+ ", " +
                RespuestaLinea.Count.ToString();
        }
    }
}
