using EasySII.Xml;
using EasySII.Xml.Silr;
using EasySII.Xml.Soap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EasySII.Business
{
    /// <summary>
    /// Lote de cobros de facturas expedidas (Accounts recivable invoices batch).
    /// </summary>
    public class ARInvoicesPaymentsBatch
    {
 

        /// <summary>
        /// Titular del lote de facturas expedidas.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de facturas emitidas incluidas en el lote.
        /// </summary>
        public List<ARInvoice> ARInvoices { get; set; }

        /// <summary>
        /// Constructor clase ARInvoicesPaymentsBatch.
        /// </summary>
        public ARInvoicesPaymentsBatch()
        {
            ARInvoices = new List<ARInvoice>();
        }

        /// <summary>
        /// Constructor clase ARInvoicesPaymentsBatch.
        /// </summary>
        /// <param name="suministroLRCobrosEmitidas">Objeto de serialización xml para
        /// suministro de facturas emitidas.</param>
        public ARInvoicesPaymentsBatch(SuministroLRCobrosEmitidas suministroLRCobrosEmitidas)
        {
            ARInvoices = new List<ARInvoice>();

            Titular = new Party()
            {
                TaxIdentificationNumber = suministroLRCobrosEmitidas.Cabecera.Titular.NIF,
                PartyName = suministroLRCobrosEmitidas.Cabecera.Titular.NombreRazon
            };

            foreach (var invoice in suministroLRCobrosEmitidas.RegistroLRCobros)
            {
                ARInvoice facturaWrk = new ARInvoice();

                facturaWrk.InvoiceNumber = invoice.IDFactura.NumSerieFacturaEmisor;
                facturaWrk.IssueDate = Convert.ToDateTime(invoice.IDFactura.FechaExpedicionFacturaEmisor);
                facturaWrk.BuyerParty = Titular; // En este caso, al no venir dicha información y para evitar problemas, el comprador también será el titular.

                //facturaWrk.SellerParty.PartyName = invoice.IDFactura.IDEmisorFactura.NombreRazon;
                Party Emisor = new Party()
                {
                    TaxIdentificationNumber = invoice.IDFactura.IDEmisorFactura.NIF
                };
                facturaWrk.SellerParty = Emisor;

                foreach (var cobros in invoice.Cobros)
                {
                    ARInvoicePayment cobroWrk = new ARInvoicePayment();
                    cobroWrk.PaymentDate = Convert.ToDateTime(cobros.Fecha);
                    cobroWrk.PaymentAmount = Convert.ToDecimal(cobros.Importe, Settings.DefaultNumberFormatInfo);

                    PaymentTerms tipoCobro;
                    if (!Enum.TryParse<PaymentTerms>(cobros.Medio, out tipoCobro))
                        throw new InvalidOperationException($"Unknown payment term {cobros.Medio}");

                    cobroWrk.PaymentTerm = tipoCobro;

                    facturaWrk.ARInvoicePayments.Add(cobroWrk);
                }
                ARInvoices.Add(facturaWrk);
            }

        }

        /// <summary>
        /// Devuelve el sobre soap del lote de facturas emitidas.
        /// </summary>
        /// <returns>String con el xml del sobre SOAP para el envío de
        /// cobros de facturas emitidas en regimen especial de caja.</returns>
        public Envelope GetEnvelope()
        {

            Envelope envelope = new Envelope();

            envelope.Body.SuministroLRCobrosEmitidas = new SuministroLRCobrosEmitidas();

            envelope.Body.SuministroLRCobrosEmitidas.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.SuministroLRCobrosEmitidas.Cabecera.Titular.NombreRazon = Titular.PartyName;

            foreach(ARInvoice invoice in ARInvoices)
                envelope.Body.SuministroLRCobrosEmitidas.RegistroLRCobros.Add(invoice.ToPaymentsSII());

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de facturas como un archivo xml para soap según las
        /// especificaciones de la aeat.
        /// </summary>
        /// <param name="xmlPath">Ruta donde se guardará el archivo generado.</param>
        /// <returns>Xaml generado.</returns>
        public XmlDocument GetXml(string xmlPath)
        {
            return SIIParser.GetXml(GetEnvelope(), xmlPath);
        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de facturas emitidas.</returns>
        public string GetSentFileName()
        {

            return GetFileName("LCFE.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de respuesta recibido para una instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("LCFE.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        public static string GetNameSent(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "LCFE.SENT.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>        
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        public static string GetNameReceived(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "LCFE.RECEIVED.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        private string GetFileName(string template)
        {

            return GetName(template, ARInvoices[0].InvoiceNumber,
                ARInvoices[ARInvoices.Count - 1].InvoiceNumber,
                Titular.TaxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="template"> Plantilla para el nombre.</param>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>        
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        private static string GetName(string template, string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {
            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                numFirstInvoiceNumber)).Replace("-", "");

            numLast = BitConverter.ToString(Encoding.UTF8.GetBytes(
                numLastInvoiceNumber)).Replace("-", "");

            return string.Format(template, taxIdentificationNumber, numFirst, numLast);
        }
    }
}
