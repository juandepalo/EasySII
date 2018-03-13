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
    public class FacturaExpedidaLRRC
    {

        /// <summary>
        /// Tipo factura según lista L2:
        /// <para>F1: Factura</para>
        /// <para>F2: Factura Simplificada (ticket)</para>
        /// <para>R1: Factura Rectificativa (Art 80.1 y 80.2 y error fundado en derecho)</para>
        /// <para>R2: Factura Rectificativa (Art. 80.3)</para>
        /// <para>R3: Factura Rectificativa (Art. 80.4)</para>
        /// <para>R4: Factura Rectificativa (Resto)</para>
        /// <para>R5: Factura Rectificativa en facturas simplificadas</para>
        /// <para>F3: Factura emitida en sustitución de facturas simplificadas facturadas y declaradas</para>
        /// <para>F4: Asiento resumen de facturas</para>
        /// </summary>
        [XmlElement("TipoFactura", Namespace = Settings.NamespaceSiiRQ)]
        public string TipoFactura {get; set;}

        /// <summary>
        /// Campo que identifica si el tipo de factura rectificativa es 
        /// por sustitución o por diferencia según lista L5:
        /// <para>S: Por sustitución</para>
        /// <para>I: Por diferencias</para>
        /// </summary>
        [XmlElement("TipoRectificativa", Namespace = Settings.NamespaceSiiRQ)]
        public string TipoRectificativa { get; set; }


        /// <summary>
        /// Facturas Agrupadas.
        /// </summary>
        [XmlArray("FacturasAgrupadas", Namespace = Settings.NamespaceSiiRQ)]
        [XmlArrayItem("IDFacturaAgrupada", Namespace = Settings.NamespaceSiiRQ)]
        public List<IDFactura> FacturasAgrupadas { get; set; }

        /// <summary>
        /// Facturas Rectificadas.
        /// </summary>
        [XmlArray("FacturasRectificadas", Namespace = Settings.NamespaceSiiRQ)]
        [XmlArrayItem("IDFacturaRectificada", Namespace = Settings.NamespaceSiiRQ)]
        public List<IDFactura> FacturasRectificadas { get; set; }

        /// <summary>
        /// Importe Rectificacion.
        /// </summary>
        [XmlElement("ImporteRectificacion", Namespace = Settings.NamespaceSiiRQ)]
        public ImporteRectificacion ImporteRectificacion { get; set; }

        /// <summary>
        /// Fecha en la que se ha realizado la operación siempre que 
        /// sea diferente a la fecha de expedición. Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaOperacion", Namespace = Settings.NamespaceSiiRQ)]
        public string FechaOperacion { get; set; }

        /// <summary>
        /// Clave que identificará el tipo de operación o el régimen 
        /// especial con transcendencia tributaria. Alfanumérico(2). Lista L3.1.
        /// </summary>
        [XmlElement("ClaveRegimenEspecialOTrascendencia", Namespace = Settings.NamespaceSiiRQ)]
        public string ClaveRegimenEspecialOTrascendencia { get; set; }


        /// <summary>
        /// Importe total de la factura.
        /// </summary>
        [XmlElement("ImporteTotal", Namespace = Settings.NamespaceSiiRQ)]
        public string ImporteTotal { get; set; }

        /// <summary>
        /// Base Imponible a Coste de la factura.
        /// </summary>
        [XmlElement("BaseImponibleACoste", Namespace = Settings.NamespaceSiiRQ)]
        public string BaseImponibleACoste { get; set; }

        /// <summary>
        /// Texto breve de la operación.
        /// </summary>
        [XmlElement("DescripcionOperacion", Namespace = Settings.NamespaceSiiRQ)]
        public string DescripcionOperacion { get; set; }

        /// <summary>
        ///Datos Inmueble.
        /// </summary>
        [XmlElement("DatosInmueble", Namespace = Settings.NamespaceSiiRQ)]
        public DetalleInmueble DatosInmueble { get; set; }

        /// <summary>
        /// Importe. Para versiones anteriores 
        /// a la versión 1.1
        /// </summary>
        [XmlElement("ImporteTransmisionSujetoAIVA", Namespace = Settings.NamespaceSiiRQ)]
        public string ImporteTransmisionSujetoAIVA { get; set; }

        /// <summary>
        /// Importe. Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// </summary>
        [XmlElement("ImporteTransmisionInmueblesSujetoAIVA", Namespace = Settings.NamespaceSiiRQ)]
        public string ImporteTransmisionInmueblesSujetoAIVA { get; set; }


        /// <summary>
        /// Indicador de si ha sido emitida por terceros. L10: Emitidas por Terceros.
        /// Para versiones anteriores 
        /// a la versión 1.1
        /// <para>S: Si</para>
        /// <para>N: No</para>
        /// </summary>
        [XmlElement("EmitidaPorTerceros", Namespace = Settings.NamespaceSiiRQ)]
        public string EmitidaPorTerceros { get; set; }

        /// <summary>
        /// Indicador de si ha sido emitida por terceros. L10: Emitidas por Terceros.
        /// Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// <para>S: Si</para>
        /// <para>N: No</para>
        /// </summary>
        [XmlElement("EmitidaPorTercerosODestinatario", Namespace = Settings.NamespaceSii)]
        public string EmitidaPorTercerosODestinatario { get; set; }

        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSiiRQ)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Tipo Desglose
        /// </summary>
        [XmlElement("TipoDesglose", Namespace = Settings.NamespaceSiiRQ)]
        public TipoDesgloseLRRC TipoDesglose { get; set; }


        /// <summary>
        /// Indica si la factura que estamos tratando tiene cobros o no
        /// </summary>
        [XmlElement("Cobros", Namespace = Settings.NamespaceSiiRQ)]
        public string Cobros { get; set; }

        /// <summary>
        /// Indica si la factura que estamos tratando tiene varios destinatarios
        /// </summary>
        [XmlElement("VariosDestinatarios", Namespace = Settings.NamespaceSiiRQ)]
        public string VariosDestinatarios { get; set; }

        /// <summary>
        /// Constructor clase FacturaExpedida.
        /// </summary>
        public FacturaExpedidaLRRC()
        {
            Contraparte = new Contraparte();
            TipoDesglose = new TipoDesgloseLRRC();
        }
    }
}
