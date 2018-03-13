using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Registro de respuesta de la factura recibida.
    /// </summary>
    public class RegistroRCLRFacturasRecibidas
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
        [XmlElement("DatosFacturaRecibida", Namespace = Settings.NamespaceSiiRQ)]
        public FacturaRecibidaLRRC FacturaRecibida { get; set; }

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
        /// Las facturas con estado “Parcialmente contrastada” incluirán en la respuesta de la consulta el bloque
        /// DatosDescuadreContraparte con la información declarada por el proveedor que no coincide en la 
        /// información del libro de facturas recibidas.
        /// </summary>
        [XmlElement("DatosDescuadreContraparte", Namespace = Settings.NamespaceSiiRQ)]
        public DatosDescuadreContraparte DatosDescuadreContraparte { get; set; }

        /// <summary>
        /// Constructor de la clase RegistroRCLRFacturasEmitidas.
        /// </summary>
        public RegistroRCLRFacturasRecibidas()
        {
            FacturaRecibida = new FacturaRecibidaLRRC();
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
