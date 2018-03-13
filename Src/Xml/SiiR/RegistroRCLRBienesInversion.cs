using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Registro de respuesta de los bienes de inversión
    /// </summary>
    public class RegistroRCLRBienesInversion
    {
        /// <summary>
        /// Datos de cabecera. Identificador de la factura especificada 
        /// en la petición de la consulta..
        /// </summary>
        [XmlElement("IDFactura", Namespace = Settings.NamespaceSiiRQ)]
        public IDFactura IDFactura { get; set; }

        /// <summary>
        /// Datos de la factura recibida por la petición de la consulta.
        /// </summary>
        [XmlElement("DatosBienInversion", Namespace = Settings.NamespaceSiiRQ)]
        public BienesInversion BienesInversion { get; set; }

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("DatosPresentacion", Namespace = Settings.NamespaceSiiRQ)]
        public DatosPresentacion DatosPresentacion { get; set; }

        /// <summary>
        /// Estado de la factura en el registro.
        /// </summary>
        [XmlElement("EstadoFactura", Namespace = Settings.NamespaceSiiRQ)]
        public EstadoFactura EstadoFactura { get; set; }

        /// <summary>
        /// Constructor de la clase RegistroRCLRBienesInversion.
        /// </summary>
        public RegistroRCLRBienesInversion()
        {
            BienesInversion = new BienesInversion();
        }

        /// <summary>
        /// Representación textual de esta instancia de RespuestaLinea.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ((IDFactura==null) ? "" : IDFactura.NumSerieFacturaEmisor) + ": " + 
                (EstadoFactura.EstadoRegistro??"");
        }
    }
}
