using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Datos de la factura.
    /// </summary>
    [Serializable]
    public class FacturaRecibida
    {

        /// <summary>
        /// Especificación del tipo de factura a dar de alta: factura  normal, 
        /// factura rectificativa, tickets, factura emitida en sustitución de facturas.
        /// Tipo factura según lista L2:
        /// <para>F1: Factura (art. 6, 7.2 y 7.3 del RD 1619/2012)</para>
        /// <para>F2: Factura Simplificada (ticket).Factura Simplificada y y Facturas sin identificación del
        /// destinatario art. 6.1.d) RD 1619/2012</para>
        /// <para>R1: Factura Rectificativa (Art 80.1 y 80.2 y error fundado en derecho)</para>
        /// <para>R2: Factura Rectificativa (Art. 80.3)</para>
        /// <para>R3: Factura Rectificativa (Art. 80.4)</para>
        /// <para>R4: Factura Rectificativa (Resto)</para>
        /// <para>R5: Factura Rectificativa en facturas simplificadas</para>
        /// <para>F3: Factura emitida en sustitución de facturas simplificadas facturadas y declaradas</para>
        /// <para>F4: Asiento resumen de facturas</para>
        /// <para>LC: Aduanas - Liquidación complementaria</para> 
        /// </summary>
        [XmlElement("TipoFactura", Namespace = Settings.NamespaceSii)]
        public string TipoFactura {get; set;}

        /// <summary>
        /// Campo que identifica si el tipo de factura rectificativa es por sustitución o por diferencia.
        /// Tipo rectificativa según lista L5:
        /// <para>S: Por sustitución.</para>
        /// <para>I: Por diferencias.</para>
        /// </summary>
        [XmlElement("TipoRectificativa", Namespace = Settings.NamespaceSii)]
        public string TipoRectificativa { get; set; }

        /// <summary>
        /// Facturas Agrupadas.
        /// </summary>
        [XmlArray("FacturasAgrupadas", Namespace = Settings.NamespaceSii)]
        [XmlArrayItem("IDFacturaAgrupada", Namespace = Settings.NamespaceSii)]
        public List<IDFactura> FacturasAgrupadas { get; set; }

        /// <summary>
        /// Facturas Rectificadas.
        /// </summary>
        [XmlArray("FacturasRectificadas", Namespace = Settings.NamespaceSii)]
        [XmlArrayItem("IDFacturaRectificada", Namespace = Settings.NamespaceSii)]
        public List<IDFactura> FacturasRectificadas { get; set; }

        /// <summary>
        /// Importe Rectificacion.
        /// </summary>
        [XmlElement("ImporteRectificacion", Namespace = Settings.NamespaceSii)]
        public ImporteRectificacion ImporteRectificacion { get; set; }

        /// <summary>
        /// Fecha en la que se ha realizado la operación siempre que sea  
        /// diferente a la fecha de expedición.
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaOperacion", Namespace = Settings.NamespaceSii)]
        public string FechaOperacion { get; set; }

        /// <summary>
        /// Clave que identificará el tipo de operación o el régimen 
        /// especial con transcendencia tributaria. Alfanumérico(2). Lista L3.1.
        /// </summary>
        [XmlElement("ClaveRegimenEspecialOTrascendencia", Namespace = Settings.NamespaceSii)]
        public string ClaveRegimenEspecialOTrascendencia { get; set; }

		/// <summary>
		/// Clave que identificará el tipo de operación o el régimen 
		/// especial con transcendencia tributaria. Alfanumérico(2). Lista L3.1.
		/// </summary>
		[XmlElement("ClaveRegimenEspecialOTrascendencia1", Namespace = Settings.NamespaceSii)]
		public string ClaveRegimenEspecialOTrascendencia1 { get; set; }

		/// <summary>
		/// Clave que identificará el tipo de operación o el régimen 
		/// especial con transcendencia tributaria. Alfanumérico(2). Lista L3.1.
		/// </summary>
		[XmlElement("ClaveRegimenEspecialOTrascendencia2", Namespace = Settings.NamespaceSii)]
		public string ClaveRegimenEspecialOTrascendencia2 { get; set; }

		/// <summary>
		/// Número de registro obtenido al enviar el 
		/// acuerdo de facturación correspondiente.
		/// </summary>
		[XmlElement("NumRegistroAcuerdoFacturacion", Namespace = Settings.NamespaceSii)]
		public string NumRegistroAcuerdoFacturacion { get; set; }

		/// <summary>
		/// Importe total de la factura.
		/// </summary>
		[XmlElement("ImporteTotal", Namespace = Settings.NamespaceSii)]
        public string ImporteTotal { get; set; }

        /// <summary>
        /// Base imponible a coste de la factura.
        /// </summary>
        [XmlElement("BaseImponibleACoste", Namespace = Settings.NamespaceSii)]
        public string BaseImponibleACoste { get; set; }

        /// <summary>
        /// Texto breve de la operación.
        /// </summary>
        [XmlElement("DescripcionOperacion", Namespace = Settings.NamespaceSii)]
        public string DescripcionOperacion { get; set; }

        /// <summary>
        /// Datos del DUA.
        /// </summary>
        [XmlElement("Aduanas", Namespace = Settings.NamespaceSii)]
        [Obsolete("Bloque Eliminado en la versión 0.7 del SII.", false)]
        public Aduanas Aduanas { get; set; }

        /// <summary>
        /// Desglose Factura.
        /// </summary>
        [XmlElement("DesgloseFactura", Namespace = Settings.NamespaceSii)]
        public DesgloseFacturaR DesgloseFactura { get; set; }

 
        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSii)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaRegContable", Namespace = Settings.NamespaceSii)]
        public string FechaRegContable { get; set; }

        /// <summary>
        /// Cuota Deducible
        /// </summary>
        [XmlElement("CuotaDeducible", Namespace = Settings.NamespaceSii)]
        public string CuotaDeducible { get; set; }

        /// <summary>
        /// Constructor clase FacturaExpedida.
        /// </summary>
        public FacturaRecibida()
        {
            Contraparte = new Contraparte();
            DesgloseFactura = new DesgloseFacturaR();
        }
    }
}
