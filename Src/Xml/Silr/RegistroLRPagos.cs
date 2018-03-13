using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Elemento de detalle de facturas emitidas.
    /// </summary>
    [Serializable]
    public class RegistroLRPagos
    {

        /// <summary>
        /// Identicador unívoco de la factura. Número+serie que identifica a la ultima factura 
        /// cuando el Tipo de Factura es un asiento resumen de facturas.
        /// </summary>
        public IDFactura IDFactura { get; set; }

        /// <summary>
        /// Lista de cobros de la factura.
        /// </summary>
        [XmlArrayItem("Pago", Namespace = Settings.NamespaceSii)]
        public List<Cobro> Pagos { get; set; }

        /// <summary>
        /// Constructor clase RegistroLRFacturasEmitidas.
        /// </summary>
        public RegistroLRPagos()
        {
            IDFactura = new IDFactura();
            Pagos = new List<Cobro>();
        }
    }
}
