using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Línea de respuesta.
    /// </summary>
    public class RespuestaLinea
    {
        /// <summary>
        /// Datos de cabecera. Identificador de la factura especificada 
        /// en la petición de suministro baja.
        /// </summary>
        [XmlElement("IDFactura", Namespace = Settings.NamespaceSiiR)]
        public IDFactura IDFactura { get; set; }

        /// <summary>
        /// Estado Registro. Campo que especifica si la factura ha sido registrada correctamente, o
        /// ha sido rechazada, o se trata de un caso en el que la factura ha sido
        /// registrada pero con errores (lista L15).
        /// </summary>
        [XmlElement("EstadoRegistro", Namespace = Settings.NamespaceSiiR)]
        public string EstadoRegistro { get; set; }

        /// <summary>
        /// Código que identifica el tipo de error producido para una
        /// factura/registro específico (lista L16).
        /// </summary>
        [XmlElement("CodigoErrorRegistro", Namespace = Settings.NamespaceSiiR)]
        public string CodigoErrorRegistro { get; set; }

        /// <summary>
        /// Descripción del error asociado al código de error producido en una
        /// factura/registro. Alfanumérico(500).
        /// </summary>
        [XmlElement("DescripcionErrorRegistro", Namespace = Settings.NamespaceSiiR)]
        public string DescripcionErrorRegistro { get; set; }

		/// <summary>
		/// Código seguro de verificación del registro ya existente en el sistema.
		/// Solo se suministra este campo en dos casos:
		/// En la respuesta de la operación de alta: Si el registro enviado es
		/// rechazado por estar duplicado.
		/// En la respuesta de la operación de baja: Si el registro enviado es
		/// rechazado porque ya está dado de baja. Alfanumérico(16).
		/// </summary>
		[XmlElement("CSV", Namespace = Settings.NamespaceSiiR)]
		public string CSV { get; set; }

		/// <summary>
		/// Representación textual de esta instancia de RespuestaLinea.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
        {
            return ((IDFactura==null) ? "" : IDFactura.NumSerieFacturaEmisor) + ": " + 
                (EstadoRegistro??"");
        }
    }
}
