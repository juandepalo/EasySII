using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Elemento de detalle de facturas emitidas.
    /// </summary>
    [Serializable]
    public class FiltroConsulta
    {

        /// <summary>
        /// Datos del periodo impositivo. 
        /// Nombre para versiones anteriores 
        /// a la versión 1.1.
        /// </summary>
        [XmlElement("PeriodoImpositivo", Namespace = Settings.NamespaceSii)]
        public PeriodoImpositivo PeriodoImpositivo { get; set; }

        /// <summary>
        /// Datos del perkiodo impositivo. 
        /// Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// </summary>
        [XmlElement("PeriodoLiquidacion", Namespace = Settings.NamespaceSii)]
        public PeriodoImpositivo PeriodoLiquidacion { get; set; }

        /// <summary>
        /// ID Factura.
        /// </summary>
        [XmlElement("IDFactura", Namespace = Settings.NamespaceSiiLRQ)]
        public IDFactura IDFactura { get; set; }

        /// <summary>
        /// Contraparte.
        /// </summary>
        [XmlElement("Contraparte", Namespace = Settings.NamespaceSiiLRQ)]
        public Contraparte Contraparte { get; set; }

        /// <summary>
        /// Rango Fecha Presentacion.
        /// </summary>
        [XmlElement("FechaPresentacion")]
        public RangoFechaPresentacion FechaPresentacion { get; set; }

        /// <summary>
        /// ID Factura modificada.
        /// </summary>
        [XmlElement("FacturaModificada")]
        public IDFactura FacturaModificada { get; set; }

        /// <summary>
        /// <para> Lista L23: Estado de cuadre de la factura</para>
        /// <para> 1: No contrastable. Estas facturas no permiten contrastarse.</para>
        /// <para> 2: En proceso de contraste. Estado "temporal" entre 
        /// el alta/modificación de la factura y su intento de cuadrea</para> 
        /// <para> 3: No contrastada. El emisor o el receptor no han registrado 
        /// la factura (no hay coincidencia en el NIF del emisor, número de factura 
        /// del emisor y fecha de expedición).</para> 
        /// <para> 4: Parcialmente contrastada. El emisor y el receptor han registrado la factura (coincidencia 
        /// en el NIF del emisor, número de factura del emisor y fecha de expedición) pero tiene
        /// discrepancias en algunos datos de la factura.</para> 
        /// <para> 5: Contrastada. El emisor y el receptor han registrado la factura 
        /// (coincidencia en el NIF del emisor, número de factura del emisor y fecha de expedición) 
        /// con los mismos datos de la factura.</para>
        /// </summary>
        [XmlElement("EstadoCuadre", Namespace = Settings.NamespaceSii)]
        public string EstadoCuadre { get; set; }

        /// <summary>
        /// Identicador unívoco de la factura. Número+serie que identifica a la ultima factura 
        /// cuando el Tipo de Factura es un asiento resumen de facturas.
        /// </summary>
        public ClavePaginacion ClavePaginacion { get; set; }
   

        /// <summary>
        /// Constructor clase RegistroLRFacturasEmitidas.
        /// </summary>
        public FiltroConsulta()
        {

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                PeriodoImpositivo = new PeriodoImpositivo();
            else
                PeriodoLiquidacion = new PeriodoImpositivo();

            PeriodoImpositivo = new PeriodoImpositivo();
        }
    }
}
