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
    /// Lote de facturas expedidas (Accounts recivable invoices batch).
    /// </summary>
    public class ARInvoicesBatchAV
    {

        /// <summary>
        /// Tipo de comunicación.
        /// </summary>
        public CommunicationType CommunicationType { get; set; }

        /// <summary>
        /// Titular del lote de facturas expedidas.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de facturas emitidas incluidas en el lote.
        /// </summary>
        public List<ARInvoiceAV> ARInvoicesAV { get; private set; }

        /// <summary>
        /// Constructor clase ARInvoicesBatch.
        /// </summary>
        public ARInvoicesBatchAV()
        {
            ARInvoicesAV = new List<ARInvoiceAV>();
        }

        /// <summary>
        /// Constructor clase ARInvoicesBatch.
        /// </summary>
        /// <param name="suministroLRFacturasEmitidas">Objeto de serialización xml para
        /// suministro de facturas emitidas.</param>
        public ARInvoicesBatchAV(SuministroLRFacturasEmitidas suministroLRFacturasEmitidas)
        {
            ARInvoicesAV = new List<ARInvoiceAV>();

            CommunicationType communicationType;

            if (!Enum.TryParse<CommunicationType>(
                suministroLRFacturasEmitidas.Cabecera.TipoComunicacion, out communicationType))
                throw new InvalidOperationException($"Unknown comunication type {suministroLRFacturasEmitidas.Cabecera.TipoComunicacion}");

            CommunicationType = communicationType;

            Titular = new Party()
            {
                TaxIdentificationNumber = suministroLRFacturasEmitidas.Cabecera.Titular.NIF,
                PartyName = suministroLRFacturasEmitidas.Cabecera.Titular.NombreRazon
            };

            foreach (var invoice in 
                suministroLRFacturasEmitidas.RegistroLRFacturasEmitidas)
                ARInvoicesAV.Add(new ARInvoiceAV(invoice));

        }

		/// <summary>
		/// Devuelve el sobre soap del lote de facturas emitidas.
		/// </summary>	
		/// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
		/// <returns>Devuelve un string con el xml del sobre SOAP
		/// compuesto para el envío del mensaje de lote de facturas
		/// emitidas.</returns>
		public Envelope GetEnvelope(bool skipErrors = false)
        {
            Envelope envelope = new Envelope();

            envelope.Body.SuministroLRFacturasEmitidas = new SuministroLRFacturasEmitidas();

            envelope.Body.SuministroLRFacturasEmitidas.Cabecera.TipoComunicacion = CommunicationType.ToString();

            envelope.Body.SuministroLRFacturasEmitidas.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.SuministroLRFacturasEmitidas.Cabecera.Titular.NombreRazon = Titular.PartyName;

            foreach(ARInvoiceAV invoice in ARInvoicesAV)
            {
                if (invoice.InvoiceNumberLastItem!=null && invoice.InvoiceType != InvoiceType.F4 && !skipErrors)
                    throw new InvalidOperationException(
                       "InvoiceNumberLastItem only valid with InvoiceType.F4");

                envelope.Body.SuministroLRFacturasEmitidas.RegistroLRFacturasEmitidas.Add(invoice.ToSII(skipErrors));
            }

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

            return GetFileName("LRFE.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de respuesta recibido para esta instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("LRFE.RECEIVED.{0}.{1}.{2}.xml");

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

            string template = "LRFE.SENT.{0}.{1}.{2}.xml";

            return  GetName(template, numFirstInvoiceNumber, 
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

            string template = "LRFE.RECEIVED.{0}.{1}.{2}.xml";

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

            return GetName(template, ARInvoicesAV[0].InvoiceNumber, 
                ARInvoicesAV[ARInvoicesAV.Count - 1].InvoiceNumber, 
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
