/*
    This file is part of the EasySII (R) project.
    Copyright (c) 2017-2018 Irene Solutions SL
    Authors: Irene Solutions SL.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    IRENE SOLUTIONS SL. IRENE SOLUTIONS SL DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
        http://www.irenesolutions.com/terms-of-use.pdf
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the EasySII software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving extract PDFs data on the fly in a web application, shipping EasySII
    with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

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
    [Obsolete("Utilice el método SendSiiLote(Batch batch) de la clase BatchDispatcher.")]
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
