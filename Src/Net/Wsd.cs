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

using EasySII.Business;
using EasySII.Business.Batches;
using EasySII.Xml;
using EasySII.Xml.Soap;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;


namespace EasySII.Net
{
    /// <summary>
    /// Esta clase gestiona las oparaciones con los servicios web de la AEAT para el SII.
    /// </summary>
    public class Wsd
    {

        /// <summary>
        /// Prefijo de todos los endpoints del SII.
        /// </summary>
        public static string EndPointPrefix = Settings.Current.SiiEndPointPrefix; 

        /// <summary>
        /// Url del web service 'SuministroLRFacturasEmitidas' con sus respectivas acciones para
        ///     el suministro, consulta y baja de facturas emitidas.
        /// </summary>
        private static string WsdSuministroLRFacturasEmitidasUrl = EndPointPrefix +"/fe/SiiFactFEV1SOAP";

        private static string WsdSuministroLRFacturasEmitidasAction = WsdSuministroLRFacturasEmitidasUrl + "?op=SuministroLRFacturasEmitidas";
        private static string WsdConsultaLRFacturasEmitidasAction = WsdSuministroLRFacturasEmitidasUrl + "?op=ConsultaLRFacturasEmitidas";
        private static string WsdAnulacionLRFacturasEmitidasAction = WsdSuministroLRFacturasEmitidasUrl + "?op=AnulacionLRFacturasEmitidas";

        /// <summary>
        /// Url del web service 'SuministroLRCobrosEmitidas' con sus respectivas acciones para
        ///     el suministro y consulta de Cobros de facturas emitidas.
        /// </summary>
        private static string WsdSuministroLRCobrosEmitidasUrl = EndPointPrefix +"/fe/SiiFactCOBV1SOAP";

        private static string WsdSuministroLRCobrosEmitidasAction = WsdSuministroLRCobrosEmitidasUrl + "?op=SuministroLRCobrosEmitidas";
        private static string WsdConsultaLRCobrosEmitidasAction = WsdSuministroLRCobrosEmitidasUrl + "?op=ConsultaLRCobrosEmitidas";


        /// <summary>
        /// Url del web service 'SuministroLRFacturasRecibidas' con sus respectivas acciones para
        ///     el suministro, consulta y baja de facturas recibidas.
        /// </summary>
        private static string WsdSuministroLRFacturasRecibidasUrl = EndPointPrefix +"/fr/SiiFactFRV1SOAP";

        private static string WsdSuministroLRFacturasRecibidasAction = WsdSuministroLRFacturasRecibidasUrl + "?op=SuministroLRFacturasRecibidas";
        private static string WsdConsultaLRFacturasRecibidasAction = WsdSuministroLRFacturasRecibidasUrl + "?op=ConsultaLRFacturasRecibidas";
        private static string WsdAnulacionLRFacturasRecibidasAction = WsdSuministroLRFacturasRecibidasUrl + "?op=AnulacionLRFacturasRecibidas";

        /// <summary>
        /// Url del web service 'SuministroLRPagosRecibidas' con sus respectivas acciones para
        ///     el suministro y consulta de pagos de facturas recibidas.
        /// </summary>
        private static string WsdSuministroLRPagosRecibidasUrl = EndPointPrefix +"/fr/SiiFactPAGV1SOAP";

        private static string WsdSuministroLRPagosRecibidasAction = WsdSuministroLRPagosRecibidasUrl + "?op=SuministroLRPagosRecibidas";
        private static string WsdConsultaLRPagosRecibidasAction = WsdSuministroLRPagosRecibidasUrl + "?op=ConsultaLRCobrosEmitidas";

        /// <summary>
        /// Url del web service 'SuministroLRBienesInversion' con sus respectivas acciones para
        ///     el suministro, consulta y baja de Bienes de Inversión.
        /// </summary>
        private static string WsdSuministroLRBienesInversionUrl = EndPointPrefix +"/bi/SiiFactBIV1SOAP";

        private static string WsdSuministroLRBienesInversionAction = WsdSuministroLRBienesInversionUrl + "?op=SuministroLRBienesInversion";
        private static string WsdConsultaLRBienesInversionAction = WsdSuministroLRBienesInversionUrl + "?op=ConsultaLRBienesInversion";
        private static string WsdAnulacionLRBienesInversionAction = WsdSuministroLRBienesInversionUrl + "?op=AnulacionLRBienesInversion";

        /// <summary>
        /// Url del web service 'SuministroLRDetOperacionIntracomunitaria' con sus respectivas acciones para
        ///     el suministro, consulta y baja de Operaciones Intracomunitarias.
        /// </summary>   
        private static string WsdSuministroLROperIntracomUrl = EndPointPrefix + "/oi/SiiFactOIV1SOAP";

        private static string WsdSuministroLROperIntracomAction = WsdSuministroLROperIntracomUrl + "?op=SuministroLRDetOperacionIntracomunitaria";
        private static string WsdConsultaLROperIntracomAction = WsdSuministroLROperIntracomUrl + "?op=ConsultaLRDetOperacionIntracomunitaria";
        private static string WsdAnulacionLROperIntracomAction = WsdSuministroLROperIntracomUrl + "?op=AnulacionLRDetOperacionIntracomunitaria";

        /// <summary>
        /// Url del web service 'SuministroLRCobrosMetalico' y 'SuministroLROperacionesSeguros' con sus respectivas acciones para
        ///     el suministro, consulta y baja de Operaciones Intracomunitarias.
        /// </summary>
        private static string WsdSuministroLROperTributariasUrl = EndPointPrefix + "/pm/SiiFactCMV1SOAP";

        private static string WsdSuministroLRCobrosMetalicoAction = WsdSuministroLROperTributariasUrl + "?op=SuministroLRCobrosMetalico";
        private static string WsdConsultaLRCobrosMetalicoAction = WsdSuministroLROperTributariasUrl + "?op=ConsultaLRCobrosMetalico";
        private static string WsdAnulacionLRCobrosMetalicoAction = WsdSuministroLROperTributariasUrl + "?op=AnulacionLRCobrosMetalico";

        private static string WsdSuministroLROperacionesSegurosAction = WsdSuministroLROperTributariasUrl + "?op=SuministroLROperacionesSeguros";
        private static string WsdConsultaLROperacionesSegurosAction = WsdSuministroLROperTributariasUrl + "?op=ConsultaLROperacionesSeguros";
        private static string WsdAnulacionLROperacionesSegurosAction = WsdSuministroLROperTributariasUrl + "?op=AnulacionLROperacionesSeguros";

        private static string WsdSuministroLRAgenciasViajesAction = WsdSuministroLROperTributariasUrl + "?op=SuministroLRAgenciasViajes";
        private static string WsdConsultaLRAgenciasViajesAction = WsdSuministroLROperTributariasUrl + "?op=ConsultaLRAgenciasViajes";
        private static string WsdAnulacionLRAgenciasViajesAction = WsdSuministroLROperTributariasUrl + "?op=AnulacionLRAgenciasViajes";

        /// <summary>
        /// Constructor estático de la clase Wsd.
        /// </summary>
        static Wsd()
        {
            if (Settings.Current == null)
                Settings.Get();
        }


        /// <summary>
        /// Envía un mensaje xml al SII.
        /// </summary>
        /// <param name="envelope">Sobre SOAP.</param>
        /// <returns>Respuesta de la AEAT.</returns>
        public static string Send(Envelope envelope)
        {

            int childCount = envelope.Body.GetChildNumber();

            if (envelope.Body.GetChildNumber() > childCount)
                throw new InvalidOperationException(
                    "Body with more than one child not allowed.");

            if (envelope.Body.GetChildNumber() == 0)
                throw new InvalidOperationException(
                    "Empty body not allowed.");

            string url = "";
            string action = "";
            string fileNameSent = "";
            string fileNameReceived = "";

            if (envelope.Body.SuministroLRFacturasEmitidas != null)
            {
                url = Wsd.WsdSuministroLRFacturasEmitidasUrl;
                action = Wsd.WsdSuministroLRFacturasEmitidasAction;

                string numFirstInvoiceNumber = 
                    envelope.Body.SuministroLRFacturasEmitidas.RegistroLRFacturasEmitidas[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.SuministroLRFacturasEmitidas.RegistroLRFacturasEmitidas.Count - 1;

                string numLastInvoiceNumber = 
                    envelope.Body.SuministroLRFacturasEmitidas.RegistroLRFacturasEmitidas[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.SuministroLRFacturasEmitidas.Cabecera.Titular.NIF;


                fileNameSent = ARInvoicesBatch.GetNameSent(numFirstInvoiceNumber, 
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = ARInvoicesBatch.GetNameReceived(numFirstInvoiceNumber, 
                    numLastInvoiceNumber, taxIdentificationNumber);

            }

            if (envelope.Body.SuministroLRFacturasRecibidas != null)
            {
                url = Wsd.WsdSuministroLRFacturasRecibidasUrl;
                action = Wsd.WsdSuministroLRFacturasRecibidasAction;

                string numFirstInvoiceNumber =
                    envelope.Body.SuministroLRFacturasRecibidas.RegistroLRFacturasRecibidas[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.SuministroLRFacturasRecibidas.RegistroLRFacturasRecibidas.Count - 1;

                string numLastInvoiceNumber =
                    envelope.Body.SuministroLRFacturasRecibidas.RegistroLRFacturasRecibidas[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.SuministroLRFacturasRecibidas.Cabecera.Titular.NIF;


                fileNameSent = APInvoicesBatch.GetNameSent(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = APInvoicesBatch.GetNameReceived(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

            }

            if (envelope.Body.BajaLRFacturasEmitidas != null)
            {
                url = Wsd.WsdSuministroLRFacturasEmitidasUrl;
                action = Wsd.WsdAnulacionLRFacturasEmitidasAction;

                string numFirstInvoiceNumber =
                    envelope.Body.BajaLRFacturasEmitidas.RegistroLRBajaExpedidas[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.BajaLRFacturasEmitidas.RegistroLRBajaExpedidas.Count - 1;

                string numLastInvoiceNumber =
                    envelope.Body.BajaLRFacturasEmitidas.RegistroLRBajaExpedidas[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.BajaLRFacturasEmitidas.Cabecera.Titular.NIF;


                fileNameSent = ARInvoicesDeleteBatch.GetNameSent(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = ARInvoicesDeleteBatch.GetNameReceived(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

            }

            if (envelope.Body.BajaLRFacturasRecibidas != null)
            {
                url = Wsd.WsdSuministroLRFacturasRecibidasUrl;
                action = Wsd.WsdAnulacionLRFacturasRecibidasAction;
       

                string numFirstInvoiceNumber =
                    envelope.Body.BajaLRFacturasRecibidas.RegistroLRBajaRecibidas[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.BajaLRFacturasRecibidas.RegistroLRBajaRecibidas.Count - 1;

                string numLastInvoiceNumber =
                    envelope.Body.BajaLRFacturasRecibidas.RegistroLRBajaRecibidas[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.BajaLRFacturasRecibidas.Cabecera.Titular.NIF;


                fileNameSent = APInvoicesDeleteBatch.GetNameSent(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = APInvoicesDeleteBatch.GetNameReceived(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

            }

            if (envelope.Body.SuministroLRBienesInversion != null)
            {

                url = Wsd.WsdSuministroLRBienesInversionUrl;
                action = Wsd.WsdSuministroLRBienesInversionAction;

                string numFirstInvoiceNumber =
                    envelope.Body.SuministroLRBienesInversion.RegistroLRBienesInversion[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.SuministroLRBienesInversion.RegistroLRBienesInversion.Count - 1;

                string numLastInvoiceNumber =
                    envelope.Body.SuministroLRBienesInversion.RegistroLRBienesInversion[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.SuministroLRBienesInversion.Cabecera.Titular.NIF;


                fileNameSent = AssetsBatch.GetNameSent(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = AssetsBatch.GetNameReceived(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

            }

            if (envelope.Body.SuministroLRCobrosEmitidas != null)
            {

                url = Wsd.WsdSuministroLRCobrosEmitidasUrl;
                action = Wsd.WsdSuministroLRCobrosEmitidasAction;

                string numFirstInvoiceNumber =
                    envelope.Body.SuministroLRCobrosEmitidas.RegistroLRCobros[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.SuministroLRCobrosEmitidas.RegistroLRCobros.Count - 1;

                string numLastInvoiceNumber =
                    envelope.Body.SuministroLRCobrosEmitidas.RegistroLRCobros[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.SuministroLRCobrosEmitidas.Cabecera.Titular.NIF;


                fileNameSent = ARInvoicesPaymentsBatch.GetNameSent(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = ARInvoicesPaymentsBatch.GetNameReceived(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);
            }

            if (envelope.Body.SuministroLRPagosRecibidas != null)
            {

                url = Wsd.WsdSuministroLRPagosRecibidasUrl;
                action = Wsd.WsdSuministroLRPagosRecibidasAction;

                string numFirstInvoiceNumber =
                    envelope.Body.SuministroLRPagosRecibidas.RegistroLRPagos[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.SuministroLRPagosRecibidas.RegistroLRPagos.Count - 1;

                string numLastInvoiceNumber =
                    envelope.Body.SuministroLRPagosRecibidas.RegistroLRPagos[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.SuministroLRPagosRecibidas.Cabecera.Titular.NIF;


                fileNameSent = APInvoicesPaymentsBatch.GetNameSent(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = APInvoicesPaymentsBatch.GetNameReceived(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);
            }

            if (envelope.Body.SuministroLRDetOperacionIntracomunitaria != null)
            {
                url = Wsd.WsdSuministroLROperIntracomUrl;
                action = Wsd.WsdSuministroLROperIntracomAction;

                string numFirstInvoiceNumber =
                    envelope.Body.SuministroLRDetOperacionIntracomunitaria.RegistroLRDetOperacionIntracomunitaria[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.SuministroLRDetOperacionIntracomunitaria.RegistroLRDetOperacionIntracomunitaria.Count - 1;

                string numLastInvoiceNumber =
                    envelope.Body.SuministroLRDetOperacionIntracomunitaria.RegistroLRDetOperacionIntracomunitaria[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.SuministroLRDetOperacionIntracomunitaria.Cabecera.Titular.NIF;

                fileNameSent = ITInvoicesBatch.GetNameSent(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = ITInvoicesBatch.GetNameReceived(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);
            }


            if (envelope.Body.BajaLRDetOperacionIntracomunitaria != null)
            {
                url = Wsd.WsdSuministroLROperIntracomUrl;
                action = Wsd.WsdAnulacionLROperIntracomAction;

                string numFirstInvoiceNumber =
                    envelope.Body.BajaLRDetOperacionIntracomunitaria.RegistroLRBajaDetOperacionIntracomunitaria[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.BajaLRDetOperacionIntracomunitaria.RegistroLRBajaDetOperacionIntracomunitaria.Count - 1;

                string numLastInvoiceNumber =
                    envelope.Body.BajaLRDetOperacionIntracomunitaria.RegistroLRBajaDetOperacionIntracomunitaria[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.BajaLRDetOperacionIntracomunitaria.Cabecera.Titular.NIF;

                fileNameSent = ITInvoicesBatch.GetNameSent(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = ITInvoicesBatch.GetNameReceived(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);
            }


            if (envelope.Body.BajaLRBienesInversion != null)
            {

                url = Wsd.WsdSuministroLRBienesInversionUrl;
                action = Wsd.WsdAnulacionLRBienesInversionAction;

                string numFirstInvoiceNumber =
                    envelope.Body.BajaLRBienesInversion.RegistroLRBienesInversion[0].IDFactura.NumSerieFacturaEmisor;

                int last = envelope.Body.BajaLRBienesInversion.RegistroLRBienesInversion.Count - 1;

                string numLastInvoiceNumber =
                    envelope.Body.BajaLRBienesInversion.RegistroLRBienesInversion[last].IDFactura.NumSerieFacturaEmisor;

                string taxIdentificationNumber = envelope.Body.BajaLRBienesInversion.Cabecera.Titular.NIF;


                fileNameSent = AssetsBatch.GetNameSent(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

                fileNameReceived = AssetsBatch.GetNameReceived(numFirstInvoiceNumber,
                    numLastInvoiceNumber, taxIdentificationNumber);

            }


            // Pendiente de implementar

            if (envelope.Body.ConsultaLRDetOperIntracomunitarias != null)
                throw new NotImplementedException("Send for ConsultaLRDetOperIntracomunitarias not implemented");

            if (envelope.Body.ConsultaCobros != null)
                throw new NotImplementedException("Send for ConsultaCobros not implemented");

            if (envelope.Body.ConsultaPagos != null)
                throw new NotImplementedException("Send for ConsultaPagos not implemented");

            if (envelope.Body.ConsultaLRFacturasEmitidas != null)
                throw new NotImplementedException("Send for ConsultaLRFacturasEmitidas not implemented");

            if (envelope.Body.ConsultaLRFacturasRecibidas != null)
                throw new NotImplementedException("Send for ConsultaLRFacturasRecibidas not implemented");

            if (envelope.Body.ConsultaLRBienesInversion != null)
                throw new NotImplementedException("Send for ConsultaLRBienesInversion not implemented");

            // Llamada a la rutina de envío

            string response = Wsd.Call(url,
            action,
            SIIParser.GetXml(envelope, Settings.Current.OutboxPath + fileNameSent));

            File.WriteAllText(Settings.Current.InboxPath + fileNameReceived, response);

            return response;


        }

        /// <summary>
        /// Envía un lote de facturas emitidas al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de facturas emitidas.</param>
        /// <returns>Devuelve un string con el xml de respuesta de
        /// la AEAT a la operación de envío del lote de facturas emitidas.</returns>
        [Obsolete("Utilice el método SendSiiLote(Batch invoicesBatch) de la clase BatchDispatcher.")]
        public static string SendFacturasEmitidas(ARInvoicesBatch invoicesBatch)
        {

            if (invoicesBatch.ARInvoices.Count == 0)
                throw new ArgumentException("Couldnt't send ARInvoicesBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLRFacturasEmitidasUrl,
                Wsd.WsdSuministroLRFacturasEmitidasAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Envía un lote de facturas emitidas al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de facturas emitidas.</param>
        /// <returns>Devuelve un string con el xml de respuesta de
        /// la AEAT a la operación de envío del lote de facturas emitidas.</returns>
        public static string SendFacturasEmitidasAV(ARInvoicesBatchAV invoicesBatch)
        {

            if (invoicesBatch.ARInvoicesAV.Count == 0)
                throw new ArgumentException("Couldnt't send ARInvoicesBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLRFacturasEmitidasUrl,
                Wsd.WsdSuministroLRFacturasEmitidasAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Envía un lote de cobros de facturas emitidas al SII. Las facturas envíadas con
        /// ClaveRegimenEspecialOTrascendencia igual a EspecialCriterioCaja, tienen que ser
        /// complementadas con la información relativa a los cobros. Este método de envío
        /// se utiliza para comunicar estos datos.
        /// </summary>
        /// <param name="paymentsBatch"> Lote de cobros de facturas emitidas.</param>
        /// <returns>Devuelve un string con el xml de respuesta de la AEAT
        /// a la operación de envío de cobros de facturas emitidas en regimen 
        /// especial de criterio de caja.</returns>
        [Obsolete("Utilice el método SendSiiLote(Batch invoicesBatch) de la clase BatchDispatcher.")]
        public static string SendCobrosFacturasEmitidas(ARInvoicesPaymentsBatch paymentsBatch)
        {

            if (paymentsBatch.ARInvoices.Count == 0)
                throw new ArgumentException("Couldnt't send ARInvoicesPaymentsBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLRCobrosEmitidasUrl,
                Wsd.WsdSuministroLRCobrosEmitidasAction,
                paymentsBatch.GetXml(Settings.Current.OutboxPath + paymentsBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + paymentsBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Consulta de facturas emitidas enviadas al SII. Devuelve un string
        /// con el xml de la AEAT en respuesta a una consulta de facturas emitidas
        /// según los criterios facilitados.
        /// </summary>
        /// <param name="invoicesQuery"> Consulta facturas.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de facturas emitidas enviadas.</returns>
        public static string GetFacturasEmitidas(ARInvoicesQuery invoicesQuery)
        {

            if (invoicesQuery.ARInvoice.IssueDate == null)
                throw new ArgumentException("Couldnt't get ARInvoicesQuery without invoice IssueDate.");

            string response = Wsd.Call(Wsd.WsdSuministroLRFacturasEmitidasUrl,
                Wsd.WsdConsultaLRFacturasEmitidasAction,
                invoicesQuery.GetXml(Settings.Current.OutboxPath + invoicesQuery.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesQuery.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Consulta de cobros facturas emitidas del regimen especial de caja
        /// enviadas al SII. Devuelve un string
        /// con el xml de la AEAT en respuesta a una consulta de cobros de facturas emitidas
        /// en regimen especial de caja según los criterios facilitados.
        /// </summary>
        /// <param name="paymentsQuery"> Consulta de cobros de facturas.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de facturas emitidas enviadas.</returns>
        public static string GetFacturasEmitidasCobros(ARPaymentsQuery paymentsQuery)
        {

            if (paymentsQuery.ARInvoice.IssueDate == null)
                throw new ArgumentException("Couldnt't get ARPaymentsQuery without invoice IssueDate.");

            string response = Wsd.Call(Wsd.WsdSuministroLRCobrosEmitidasUrl,
                Wsd.WsdConsultaLRCobrosEmitidasAction,
                paymentsQuery.GetXml(Settings.Current.OutboxPath + paymentsQuery.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + paymentsQuery.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Borra un lote de facturas emitidas previamente envíadas al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de facturas emitidas a borrar.</param>
        /// <returns>Devuelve un string con el xml de respuesta de
        /// la AEAT a la operación de envío del lote de facturas emitidas.</returns>
        [Obsolete("Utilice el método SendSiiLote(Batch invoicesBatch) de la clase BatchDispatcher.")]
        public static string DeleteFacturasEmitidas(ARInvoicesDeleteBatch invoicesBatch)
        {

            if (invoicesBatch.ARInvoices.Count == 0)
                throw new ArgumentException("Couldnt't send ARInvoicesDeleteBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLRFacturasEmitidasUrl,
                Wsd.WsdAnulacionLRFacturasEmitidasAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }


        /// <summary>
        /// Envía un lote de facturas recibidas al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de facturas recibidas.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de facturas emitidas recibidas.</returns>
        [Obsolete("Utilice el método SendSiiLote(Batch invoicesBatch) de la clase BatchDispatcher.")]
        public static string SendFacturasRecibidas(APInvoicesBatch invoicesBatch)
        {

            if (invoicesBatch.APInvoices.Count == 0)
                throw new ArgumentException("Couldnt't send APInvoicesBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLRFacturasRecibidasUrl,
                Wsd.WsdSuministroLRFacturasRecibidasAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Envía un lote de pagos de facturas recibidas al SII. Las facturas recibidas con
        /// ClaveRegimenEspecialOTrascendencia igual a EspecialCriterioCaja, tienen que ser
        /// complementadas con la información relativa a los cobros. Este método de envío
        /// se utiliza para comunicar estos datos.
        /// </summary>
        /// <param name="paymentsBatch"> Lote de pagos de facturas recibidas.</param>
        /// <returns>Devuelve un string con el xml de respuesta de la AEAT
        /// a la operación de envío de pagos de facturas recibidas en regimen 
        /// especial de criterio de caja.</returns>
        [Obsolete("Utilice el método SendSiiLote(Batch invoicesBatch) de la clase BatchDispatcher.")]
        public static string SendPagosFacturasRecibidas(APInvoicesPaymentsBatch paymentsBatch)
        {

            if (paymentsBatch.APInvoices.Count == 0)
                throw new ArgumentException("Couldnt't send APInvoicesPaymentsBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLRPagosRecibidasUrl,
                Wsd.WsdSuministroLRPagosRecibidasAction,
                paymentsBatch.GetXml(Settings.Current.OutboxPath + paymentsBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + paymentsBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Consulta de facturas recibidas enviadas al SII. Devuelve
        /// un string con el xml de respuesta de la AEAT a una petición
        /// de lista de facturas recibidas envíadas, atendiendo a los criterios
        /// de filtro facilidados como parámetro.
        /// </summary>
        /// <param name="invoicesQuery"> Consulta facturas. Contiene los
        /// criterios de filtro para la lista de facturas que se pretende
        /// recuperar.</param>
        /// <returns>String con el xml de respuesta de la AEAT a
        /// una petición de lista de facturas recibidas envíadas.</returns>
        public static string GetFacturasRecibidas(APInvoicesQuery invoicesQuery)
        {

            if (invoicesQuery.APInvoice.IssueDate == null)
                throw new ArgumentException("Couldnt't get ARInvoicesQuery without invoice IssueDate.");

            string response = Wsd.Call(Wsd.WsdSuministroLRFacturasRecibidasUrl,
                Wsd.WsdConsultaLRFacturasRecibidasAction,
                invoicesQuery.GetXml(Settings.Current.OutboxPath + invoicesQuery.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesQuery.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Consulta de pagos facturas recibidas del regimen especial de caja
        /// enviadas al SII. Devuelve un string
        /// con el xml de la AEAT en respuesta a una consulta de pagos de facturas recibidas
        /// en regimen especial de caja según los criterios facilitados.
        /// </summary>
        /// <param name="paymentsQuery"> Consulta de pagos de facturas.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de facturas emitidas recibidas.</returns>
        public static string GetFacturasRecibidasPagos(APPaymentsQuery paymentsQuery)
        {

            if (paymentsQuery.APInvoice.IssueDate == null)
                throw new ArgumentException("Couldnt't get ARPaymentsQuery without invoice IssueDate.");

            string response = Wsd.Call(Wsd.WsdSuministroLRPagosRecibidasUrl,
                Wsd.WsdConsultaLRPagosRecibidasAction,
                paymentsQuery.GetXml(Settings.Current.OutboxPath + paymentsQuery.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + paymentsQuery.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Borra un lote de facturas recibidas envíadas previamente envíadas al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de facturas recibidas a borrar.</param>
        /// <returns>Devuelve un string con el xml de respuesta de
        /// la AEAT a la operación de envío del lote de facturas recibidas.</returns>
        [Obsolete("Utilice el método SendSiiLote(Batch invoicesBatch) de la clase BatchDispatcher.")]
        public static string DeleteFacturasRecibidas(APInvoicesDeleteBatch invoicesBatch)
        {

            if (invoicesBatch.APInvoices.Count == 0)
                throw new ArgumentException("Couldnt't send APInvoicesDeleteBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLRFacturasRecibidasUrl,
                Wsd.WsdAnulacionLRFacturasRecibidasAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Envía un lote de bienes inversión al SII.
        /// </summary>
        /// <param name="assetsBatch"> Lote de bienes inversion.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de Bienes de Inversión.</returns>
        [Obsolete("Utilice el método SendSiiLote(Batch invoicesBatch) de la clase BatchDispatcher.")]
        public static string SendBienesInversion(AssetsBatch assetsBatch)
        {

            if (assetsBatch.Assets.Count == 0)
                throw new ArgumentException("Couldnt't send AssetsBatch without assets.");

            string response = Wsd.Call(Wsd.WsdSuministroLRBienesInversionUrl,
                Wsd.WsdSuministroLRBienesInversionAction,
                assetsBatch.GetXml(Settings.Current.OutboxPath + assetsBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + assetsBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Consulta de Bienes de Inversión enviadas al SII. Devuelve un string
        /// con el xml de la AEAT en respuesta a una consulta de bienes de inversión
        /// según los criterios facilitados.
        /// </summary>
        /// <param name="assetsBatch"> Consulta de Bienes Inversión.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de Bienes de Inversión.</returns>
        public static string GetBienesInversion(AssetsQuery assetsBatch)
        {

            if (assetsBatch.Assets.IssueDate == null)
                throw new ArgumentException("Couldnt't get AssetsQuery without invoice IssueDate.");

            string response = Wsd.Call(Wsd.WsdSuministroLRBienesInversionUrl,
                Wsd.WsdConsultaLRBienesInversionAction,
                assetsBatch.GetXml(Settings.Current.OutboxPath + assetsBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + assetsBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Borrar un lote de Bienes de Inversión enviadas al SII.
        /// </summary>
        /// <param name="assetsBatch"> Lote de Bienes Inversión.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de borrado de un lote de Bienes de Inversión.</returns>
        [Obsolete("Utilice el método SendSiiLote(Batch invoicesBatch) de la clase BatchDispatcher.")]
        public static string DeleteBienesInversion(AssetsDeleteBatch assetsBatch)
        {

            if (assetsBatch.Assets.Count == 0)
                throw new ArgumentException("Couldnt't send IPInvoicesDeleteBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLRBienesInversionUrl,
                Wsd.WsdAnulacionLRBienesInversionAction,
                assetsBatch.GetXml(Settings.Current.OutboxPath + assetsBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + assetsBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Envía un lote de Operaciones Intracomunitarias al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de operaciones intracomunitarias.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de Operaciones Intracomunitarias.</returns>
        public static string SendOperIntracom(ITInvoicesBatch invoicesBatch)
        {

            if (invoicesBatch.ITInvoices.Count == 0)
                throw new ArgumentException("Couldnt't send ITInvoicesBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperIntracomUrl,
                Wsd.WsdSuministroLROperIntracomAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;
        }

        /// <summary>
        /// Consulta de Operaciones Intracomunitarias enviadas al SII. Devuelve un string
        /// con el xml de la AEAT en respuesta a una consulta de Operaciones Intracomunitarias
        /// según los criterios facilitados.
        /// </summary>
        /// <param name="invoicesBatch"> Consulta de Operaciones Intracomunitarias.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de Operaciones Intracomunitarias.</returns>
        public static string GetOperIntracom(ITInvoicesQuery invoicesBatch)
        {

            if (invoicesBatch.ITInvoice.IssueDate == null)
                throw new ArgumentException("Couldnt't get ITInvoicesQuery without invoice IssueDate.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperIntracomUrl,
                Wsd.WsdConsultaLROperIntracomAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Borrar un lote de Operaciones Intracomunitarias enviadas al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de Operaciones Intracomunitarias.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de borrado de un lote de Operaciones Intracomunitarias.</returns>
        public static string DeleteOperIntracom(ITInvoicesDeleteBatch invoicesBatch)
        {

            if (invoicesBatch.ITInvoices.Count == 0)
                throw new ArgumentException("Couldnt't send ITInvoicesDeleteBatch without invoices.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperIntracomUrl,
                Wsd.WsdAnulacionLROperIntracomAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Envía un lote de Cobros en metálico al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de cobros en metálico.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de Cobros en metálico.</returns>
        public static string SendCobrosMetalico(InsurancesBatch invoicesBatch)
        {

            if (invoicesBatch.Insurances.Count == 0)
                throw new ArgumentException("Couldnt't send InsurancesBatch without insurance.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperTributariasUrl,
                Wsd.WsdSuministroLRCobrosMetalicoAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;
        }

        /// <summary>
        /// Consulta de Cobros en metálico enviados al SII. Devuelve un string
        /// con el xml de la AEAT en respuesta a una consulta de cobros en metálico
        /// según los criterios facilitados.
        /// </summary>
        /// <param name="invoicesBatch"> Consulta de cobros en metálico.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de cobros en metálico.</returns>
        public static string GetCobrosMetalico(InsurancesQuery invoicesBatch)
        {

            if (invoicesBatch.Insurance.IssueDate == null)
                throw new ArgumentException("Couldnt't get InsuranceQuery without invoice IssueDate.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperTributariasUrl,
                Wsd.WsdConsultaLRCobrosMetalicoAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Borrar un lote de cobros en metálico enviados al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de cobros en metálico.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de borrado de un lote de cobros en metálico.</returns>
        public static string DeleteCobrosMetalico(InsurancesDeleteBatch invoicesBatch)
        {

            if (invoicesBatch.Insurances.Count == 0)
                throw new ArgumentException("Couldnt't send InsurancesDeleteBatch without insurance.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperTributariasUrl,
                Wsd.WsdAnulacionLRCobrosMetalicoAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Envía un lote de operaciones seguros al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de operaciones seguros.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de operaciones seguros.</returns>
        public static string SendOperacionesSeguros(InsurancesBatch invoicesBatch)
        {

            if (invoicesBatch.Insurances.Count == 0)
                throw new ArgumentException("Couldnt't send InsurancesBatch without insureance operations.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperTributariasUrl,
                Wsd.WsdSuministroLROperacionesSegurosAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;
        }

        /// <summary>
        /// Consulta de operaciones de seguros enviados al SII. Devuelve un string
        /// con el xml de la AEAT en respuesta a una consulta de operaciones seguros
        /// según los criterios facilitados.
        /// </summary>
        /// <param name="invoicesBatch"> Consulta de operaciones seguros.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de operaciones seguros.</returns>
        public static string GetOperacionesSeguros(InsurancesQuery invoicesBatch)
        {

            if (invoicesBatch.Insurance.IssueDate == null)
                throw new ArgumentException("Couldnt't get InsurancesQuery without invoice IssueDate.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperTributariasUrl,
                Wsd.WsdConsultaLROperacionesSegurosAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Borrar un lote de operaciones seguros enviados al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de operaciones seguros.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de borrado de un lote de operaciones seguros.</returns>
        public static string DeleteOperacionesSeguros(InsurancesDeleteBatch invoicesBatch)
        {

            if (invoicesBatch.Insurances.Count == 0)
                throw new ArgumentException("Couldnt't send InsurancesDeleteBatch without insurance operations.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperTributariasUrl,
                Wsd.WsdAnulacionLROperacionesSegurosAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Envía un lote de operaciones seguros al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de agencias viajes.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de operaciones de agencias viajes.</returns>
        public static string SendAgenciasViajes(TravelAgencyBatch invoicesBatch)
        {

            if (invoicesBatch.TravelAgencyOperations.Count == 0)
                throw new ArgumentException("Couldnt't send TravelAgencyBatch without TravelAgency operations.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperTributariasUrl,
                Wsd.WsdSuministroLRAgenciasViajesAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;
        }

        /// <summary>
        /// Consulta de operaciones de seguros enviados al SII. Devuelve un string
        /// con el xml de la AEAT en respuesta a una consulta de agencias viaje
        /// según los criterios facilitados.
        /// </summary>
        /// <param name="invoicesBatch"> Consulta de operaciones seguros.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de consulta de agencias viaje.</returns>
        public static string GetAgenciasViajes(TravelAgencyQuery invoicesBatch)
        {

            if (invoicesBatch.TravelAgencyOperation.IssueDate == null)
                throw new ArgumentException("Couldnt't get TravelAgencyQuery without invoice IssueDate.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperTributariasUrl,
                Wsd.WsdConsultaLRAgenciasViajesAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }

        /// <summary>
        /// Borrar un lote de operaciones seguros enviados al SII.
        /// </summary>
        /// <param name="invoicesBatch"> Lote de agencias viajes.</param>
        /// <returns>Devuelve el xml de respuesta de la AEAT a una
        /// petición de borrado de un lote de operaciones agencias de viajes.</returns>
        public static string DeleteAgenciasViajes(TravelAgencyDeleteBatch invoicesBatch)
        {

            if (invoicesBatch.TravelAgencyOperations.Count == 0)
                throw new ArgumentException("Couldnt't send TravelAgencyDeleteBatch without TravelAgency operations.");

            string response = Wsd.Call(Wsd.WsdSuministroLROperTributariasUrl,
                Wsd.WsdAnulacionLRAgenciasViajesAction,
                invoicesBatch.GetXml(Settings.Current.OutboxPath + invoicesBatch.GetSentFileName()));

            File.WriteAllText(Settings.Current.InboxPath + invoicesBatch.GetReceivedFileName(), response);

            return response;

        }



        /// <summary>
        /// Llama a al web service de la AEAT para el SII seleccionado.
        /// </summary>
        /// <param name="url">Url destino.</param>
        /// <param name="action">Acción a ejecutar.</param>
        /// <param name="xmlDocument">Documento soap xml.</param>
        /// <returns>Devuelve la respuesta.</returns>
        protected static string Call(string url, string action, XmlDocument xmlDocument)
        {
            HttpWebRequest webRequest = CreateWebRequest(url, action);

            X509Certificate2 certificate = GetCertificate();

            if (certificate == null)
                throw new ArgumentNullException(
                    "Certificate is null. Maybe serial number in configuration was wrong.");

            webRequest.ClientCertificates.Add(certificate);

            using (Stream stream = webRequest.GetRequestStream())
            {
                xmlDocument.Save(stream);
            }

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
          
            string statusDescription = response.StatusDescription;
           
            Stream dataStream = response.GetResponseStream();

            string responseFromServer;

            using (StreamReader reader = new StreamReader(dataStream))
            {  
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }

           
            return responseFromServer;

        }

        /// <summary>
        /// Devuelve el certificado configurado.
        /// </summary>
        /// <returns>Devuelve el certificado de la 
        /// configuración para las comunicaciones.</returns>
        public static X509Certificate2 GetCertificate()
        {
            X509Store store = new X509Store();
            store.Open(OpenFlags.ReadOnly);
            foreach (X509Certificate2 cert in store.Certificates)
                if (cert.SerialNumber == Settings.Current.CertificateSerial)
                    return cert;

			// Probamos en LocalMachine
			X509Store storeLM = new X509Store(StoreLocation.LocalMachine);
			storeLM.Open(OpenFlags.ReadOnly);
			foreach (X509Certificate2 cert in storeLM.Certificates)
				if (cert.SerialNumber == Settings.Current.CertificateSerial)
					return cert;

			return null;
        }

        /// <summary>
        /// Crea la instancia WebRequest para enviar la petición
        /// al web service de la AEAT.
        /// </summary>
        /// <param name="url">Url del web service.</param>
        /// <param name="action">Acción del web service.</param>
        /// <returns></returns>
        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Headers.Add("SOAPAction", action);

            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

    }

    
}
