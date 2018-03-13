using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Registro de respuesta de los cobros en metálico
    /// </summary>
    public class RegistroRCLRCobrosMetalico
    {
        /// <summary>
        /// Datos de la factura recibida por la petición de la consulta.
        /// </summary>
        [XmlElement("DatosCobroMetalico", Namespace = Settings.NamespaceSiiRQ)]
        public RegistroLROpTrascendTribu CobrosMetalico { get; set; }

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("DatosPresentacion", Namespace = Settings.NamespaceSiiRQ)]
        public DatosPresentacion DatosPresentacion { get; set; }

        /// <summary>
        /// Estado de la factura en el registro.
        /// </summary>
        [XmlElement("EstadoCobroMetalico", Namespace = Settings.NamespaceSiiRQ)]
        public EstadoFactura EstadoCobroMetalico { get; set; }

        /// <summary>
        /// Constructor de la clase RegistroRCLRCobrosMetalico.
        /// </summary>
        public RegistroRCLRCobrosMetalico()
        {
            CobrosMetalico = new RegistroLROpTrascendTribu();
        }

        /// <summary>
        /// Representación textual de esta instancia de RespuestaLinea.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ((CobrosMetalico==null) ? "" : CobrosMetalico.Contraparte.NombreRazon) + ": " + 
                (EstadoCobroMetalico.EstadoRegistro??"");
        }
    }
}
