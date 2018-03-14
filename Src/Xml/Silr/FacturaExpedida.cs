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
    public class FacturaExpedida
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
        [XmlElement("TipoFactura", Namespace = Settings.NamespaceSii)]
        public string TipoFactura {get; set;}

        /// <summary>
        /// Campo que identifica si el tipo de factura rectificativa es 
        /// por sustitución o por diferencia según lista L5:
        /// <para>S: Por sustitución</para>
        /// <para>I: Por diferencias</para>
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
        /// Fecha en la que se ha realizado la operación siempre que 
        /// sea diferente a la fecha de expedición. Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
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
        /// Clave adicional que
        /// identificará el tipo de régimen del IVA o 
        /// una operación con trascendencia tributaria. 
        /// Alfanumérico(2). Lista L3.1.
        /// </summary>
        [XmlElement("ClaveRegimenEspecialOTrascendenciaAdicional1", Namespace = Settings.NamespaceSii)]
		public string ClaveRegimenEspecialOTrascendenciaAdicional1 { get; set; }


        /// <summary>
        /// Clave adicional que
        /// identificará el tipo de régimen del IVA o 
        /// una operación con trascendencia tributaria. 
        /// Alfanumérico(2). Lista L3.1.
        /// </summary>
        [XmlElement("ClaveRegimenEspecialOTrascendenciaAdicional2", Namespace = Settings.NamespaceSii)]
		public string ClaveRegimenEspecialOTrascendenciaAdicional2 { get; set; }

        /// <summary>
        /// Número de registro obtenido al enviar la 
        /// autorización en materia de facturación o de 
        /// libros registro. Alfanumérico(15)
        /// </summary>
        [XmlElement("NumRegistroAcuerdoFacturacion", Namespace = Settings.NamespaceSii)]
		public string NumRegistroAcuerdoFacturacion { get; set; }

        /// <summary>
        /// Importe total de la factura. Decimal(12,2).
        /// </summary>
        [XmlElement("ImporteTotal", Namespace = Settings.NamespaceSii)]
        public string ImporteTotal { get; set; }

        /// <summary>
        /// Base Imponible a Coste de la factura. Decimal(12,2)
        /// </summary>
        [XmlElement("BaseImponibleACoste", Namespace = Settings.NamespaceSii)]
        public string BaseImponibleACoste { get; set; }

        /// <summary>
        /// Texto breve de la operación. Alfanumérico(500).
        /// </summary>
        [XmlElement("DescripcionOperacion", Namespace = Settings.NamespaceSii)]
        public string DescripcionOperacion { get; set; }

        /// <summary>
        /// Referencia Externa. Dato adicional de contenido 
        /// libre enviado por algunas aplicaciones clientes 
        /// (asiento contable, etc). Alfanumérico(60).
        /// </summary>
        [XmlElement("RefExterna", Namespace = Settings.NamespaceSii)]
        public string RefExterna { get; set; }

        /// <summary>
        /// Factura simplificada Articulo 7,2 Y 7,3 RD 1619/2012. 
        /// Si no se informa este campo se entenderá que tiene valor “N". 
        /// L27 ('S', 'N').
        /// </summary>
        [XmlElement("FacturaSimplificadaArticulos7.2_7.3", Namespace = Settings.NamespaceSii)]
        public string FacturaSimplificadaArticulos72_73 { get; set; }

        /// <summary>
        /// NombreRazon + NIF de la entidad sucedida como 
        /// consecuencia de una operación de reestructuración.
        /// </summary>
        [XmlElement("EntidadSucedida", Namespace = Settings.NamespaceSii)]
        public Parte EntidadSucedida { get; set; }

        /// <summary>
        /// Identificador que especifica aquellos registros de 
        /// facturación con dificultades para enviarse en plazo 
        /// por no tener constancia del cambio de condición a
        /// GGEE, de la inclusión en REDEME o de un cambio 
        /// en la competencia inspectora. L29 ('S', 'N').
        /// Alfanumérico(1).
        /// </summary>
        [XmlElement("RegPrevioGGEEoREDEMEoCompetencia", Namespace = Settings.NamespaceSii)]
        public string RegPrevioGGEEoREDEMEoCompetencia{ get; set; }

        /// <summary>
        /// Identificador que especifica aquellas facturas con importe 
        /// de la factura superior a un umbral de 100.000 euros. Si no se informa este
        /// campo se entenderá que tiene valor “N”. L30 ('S', 'N').
        /// Alfanumérico(1).
        /// </summary>
        [XmlElement("Macrodato", Namespace = Settings.NamespaceSii)]
        public string Macrodato { get; set; }

        /// <summary>
        ///Datos Inmueble.
        /// </summary>
        [XmlElement("DatosInmueble", Namespace = Settings.NamespaceSii)]
        public DatInmueble DatosInmueble { get; set; }

        /// <summary>
        /// Importe. Para versiones anteriores 
        /// a la versión 1.1
        /// </summary>
        [XmlElement("ImporteTransmisionSujetoAIVA", Namespace = Settings.NamespaceSii)]
        public string ImporteTransmisionSujetoAIVA { get; set; }

        /// <summary>
        /// Importe. Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// </summary>
        [XmlElement("ImporteTransmisionInmueblesSujetoAIVA", Namespace = Settings.NamespaceSii)]
        public string ImporteTransmisionInmueblesSujetoAIVA { get; set; }


        /// <summary>
        /// Indicador de si ha sido emitida por terceros. L10: Emitidas por Terceros.
        /// Para versiones anteriores 
        /// a la versión 1.1
        /// <para>S: Si</para>
        /// <para>N: No</para>
        /// </summary>
        [XmlElement("EmitidaPorTerceros", Namespace = Settings.NamespaceSii)]
        public string EmitidaPorTerceros { get; set; }

        /// <summary>
        /// Indicador de si ha sido emitida por terceros. L10: Emitidas por Terceros.
        ///   Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// <para>S: Si</para>
        /// <para>N: No</para>
        /// </summary>
        [XmlElement("EmitidaPorTercerosODestinatario", Namespace = Settings.NamespaceSii)]
        public string EmitidaPorTercerosODestinatario { get; set; }

        /// <summary>
        /// Identificador que especifica si la factura ha sido 
        /// emitida por un tercero de acuerdo a una exigencia normativa 
        /// (disposición adicional tercera y sexta Reglamento por el 
        /// que se regulan las obligaciones de facturación y del Mercado 
        /// Organizado del Gas). Si no se informa este campo se entenderá 
        /// que tiene valor “N”. L25 ('S', 'N').
        /// Alfanumérico(1).
        /// </summary>
        [XmlElement("FacturacionDispAdicionalTerceraYsextayDelMercadoOrganizadoDelGas", Namespace = Settings.NamespaceSii)]
        public string FacturacionDispAdicionalTerceraYsextayDelMercadoOrganizadoDelGas { get; set; }

        /// <summary>
        /// Identificador que especifica si la factura tiene varios 
        /// destinatarios. Si no se informa este campo se entenderá que 
        /// tiene valor “N”. L20 ('S', 'N').
        /// Alfanumérico(1).
        /// </summary>
        [XmlElement("VariosDestinatarios", Namespace = Settings.NamespaceSii)]
        public string VariosDestinatarios { get; set; }

        /// <summary>
        /// Identificador que especifica si la factura tipo 
        /// R1, R5 o F4 tiene minoración de la base imponible por 
        /// la concesión de cupones, bonificaciones o descuentos 
        /// cuando solo se expide el original de la factura. 
        /// Si no se informa este campo se entenderá 
        /// que tiene valor “N”. L22 ('S', 'N').
        /// Alfanumérico(1).
        /// </summary>
        [XmlElement("Cupon", Namespace = Settings.NamespaceSii)]
        public string Cupon { get; set; }

        /// <summary>
        /// Factura sin identificación destinatario artículo 6.1.d) 
        /// RD 1619/2012 Si no se informa este campo se entenderá 
        /// que tiene valor “N”. L28 ('S', 'N').
        /// Alfanumérico(1).
        /// </summary>
        [XmlElement("FacturaSinIdentifDestinatarioAritculo6.1.d", Namespace = Settings.NamespaceSii)]
        public string FacturaSinIdentifDestinatarioAritculo61d { get; set; }

        /// <summary>
        /// Comprador.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSii)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Tipo Desglose
        /// </summary>
        [XmlElement("TipoDesglose", Namespace = Settings.NamespaceSii)]
        public TipoDesglose TipoDesglose { get; set; }

        /// <summary>
        /// Constructor clase FacturaExpedida.
        /// </summary>
        public FacturaExpedida()
        {
            Contraparte = new Contraparte();
            TipoDesglose = new TipoDesglose();
        }
    }
}
