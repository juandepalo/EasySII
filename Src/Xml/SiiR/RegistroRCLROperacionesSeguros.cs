using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Registro de respuesta de los cobros en metálico
    /// </summary>
    public class RegistroRCLROperacionesSeguros
    {
        /// <summary>
        /// Datos de la factura recibida por la petición de la consulta.
        /// </summary>
        [XmlElement("DatosOperacionesSeguros", Namespace = Settings.NamespaceSiiRQ)]
        public RegistroLROpTrascendTribu OperacionesSeguros { get; set; }

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("DatosPresentacion", Namespace = Settings.NamespaceSiiRQ)]
        public DatosPresentacion DatosPresentacion { get; set; }

        /// <summary>
        /// Estado de la factura en el registro.
        /// </summary>
        [XmlElement("EstadoOperacionesSeguros", Namespace = Settings.NamespaceSiiRQ)]
        public EstadoFactura EstadoOperacionesSeguros { get; set; }

        /// <summary>
        /// Constructor de la clase RegistroRCLROperacionesSeguros.
        /// </summary>
        public RegistroRCLROperacionesSeguros()
        {
            OperacionesSeguros = new RegistroLROpTrascendTribu();
        }

        /// <summary>
        /// Representación textual de esta instancia de RespuestaLinea.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ((OperacionesSeguros == null) ? "" : OperacionesSeguros.Contraparte.NombreRazon) + ": " + 
                (EstadoOperacionesSeguros.EstadoRegistro??"");
        }
    }
}
