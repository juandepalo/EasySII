using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EasySII.Business
{
    /// <summary>
    /// Cobro/Pago de factura.
    /// </summary>
    public class InvoiceRectified
    {


        /// <summary>
        /// Fecha de emisión de la factura a la que se refiere la rectificativa.
        /// </summary>
        public DateTime? RectifiedIssueDate { get; set; }

        /// <summary>
        /// Número de factura a la que se refiere la rectificativa.
        /// </summary>
        public string RectifiedInvoiceNumber { get; set; }


        /// <summary>
        /// Constructor de InvoiceRectified.
        /// </summary>
        public InvoiceRectified()
        {           
        }

    }
}
