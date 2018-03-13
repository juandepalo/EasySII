using EasySII.Xml.Silr;
using System.Xml.Serialization;

namespace EasySII.Xml.Sii
{
    /// <summary>
    /// Datos Descuadre Contraparte.
    /// </summary>
    public class DatosDescuadreContraparte
    {

        /// <summary>
        /// Sumatorio Base Imponible declarada por la contraparte en la operación 
        /// con inversión de sujeto pasivo.Sólo se suministra si la factura es 
        /// parcialmente contrastada y hay discrepancia con la 
        /// información declarada por la contraparte. Decimal(14,2).
        /// </summary>
        public string SumBaseImponibleISP{ get; set; }

        /// <summary>
        /// Sumatorio Base Imponible declarada por la contraparte en la operación sin 
        /// inversión de sujeto pasivo.Sólo se suministra si la factura es parcialmente 
        /// contrastada y hay discrepancia con la información declarada por 
        /// la contraparte. Decimal(14,2).
        /// </summary>
        public string SumBaseImponible { get; set; }

        /// <summary>
        ///Sumatorio de la cuota del impuesto declarada por la contraparte en la operacion
        ///sin inversión de sujeto pasivo.Sólo se suministra si la factura es parcialmente 
        ///contrastada y hay discrepancia con la información declarada por la contraparte. Decimal(14,2).
        /// </summary>
        public string SumCuota{ get; set; }

        /// <summary>
        /// Sumatorio de la cuota del recargo de equivalencia declarada por la contraparte en la operacion 
        /// sin inversión de sujeto pasivo.Sólo se suministra si la factura es parcialmente contrastada y hay
        /// discrepancia con la información declarada por la contraparte. Decimal(14,2).
        /// </summary>
        public string SumCuotaRecargoEquivalencia{ get; set; }

        /// <summary>
        /// Importe de la factura declarada por la contraparte. Sólo se suministra si la factura es parcialmente 
        /// contrastada y hay discrepancia con la información declarada por la contraparte. Decimal(14,2).
        /// </summary>
        public string ImporteTotal{ get; set; }

        /// <summary>
        /// Constructor DatosDescuadreContraparte.
        /// </summary>
        public DatosDescuadreContraparte()
        {
        }
    }
}
