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
    public class InvoicePayment
    {


        /// <summary>
        /// Fecha de emisión.
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Importe cobro.
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// Medio de pago.
        /// </summary>
        public PaymentTerms PaymentTerm { get; set; }

        /// <summary>
        /// Texto de cuenta o medio de cobro de factura.
        /// </summary>
        public string AccountOrTermsText { get; set; }


        /// <summary>
        /// Constructor de InvoicePayment.
        /// </summary>
        public InvoicePayment()
        {           
        }

    }
}
