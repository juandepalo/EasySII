using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Elemento de detalle de facturas emitidas.
    /// </summary>
    [Serializable]
    public class FiltroConsultaCobrosPagos
    {


        /// <summary>
        /// Identicador unívoco de la factura. Número+serie que identifica a la ultima factura 
        /// cuando el Tipo de Factura es un asiento resumen de facturas.
        /// </summary>
        public IDFactura IDFactura { get; set; }

        /// <summary>
        /// Especifica la clave del pago a partir de la cual se recuperarán los siguientes cobros ordenados por
        /// fecha de presentación.En la primera petición de consulta no hay que incluir este campo y se obtendrán
        /// un máximo de 10.000 cobros ordenados por fecha depresentación. 
        /// </summary>
        [XmlElement("ClavePaginacion")]
        public string ClavePaginacionCobro { get; set; }
        
        /// <summary>
        /// Constructor clase RegistroLRFacturasEmitidas.
        /// </summary>
        public FiltroConsultaCobrosPagos()
        {
            IDFactura = new IDFactura();
        }
    }
}
