using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Registro de respuesta de los cobros sobre facturas emitidas
    /// </summary>
    public class RegistroRespuestaConsultaCobros
    {
        /// <summary>
        /// Datos de la factura recibida por la petición de la consulta.
        /// </summary>
        [XmlElement("DatosCobro", Namespace = Settings.NamespaceSiiRQ)]
        public Cobro Cobro { get; set; }

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("DatosPresentacion", Namespace = Settings.NamespaceSiiRQ)]
        public DatosPresentacion DatosPresentacion { get; set; }

        /// <summary>
        /// Constructor de la clase RegistroRespuestaConsultaCobros.
        /// </summary>
        public RegistroRespuestaConsultaCobros()
        {
            Cobro = new Cobro();
        }

        /// <summary>
        /// Representación textual de esta instancia de RespuestaLinea.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ((Cobro==null) ? "" : Cobro.Fecha.ToString()) + ": " + 
                (Cobro.Importe??"");
        }
    }
}
