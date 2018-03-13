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
    /// Lote de facturas recibidas (Accounts payable invoices batch).
    /// </summary>
    public class APInvoicesBatch
    {

        /// <summary>
        /// Tipo de comunicación.
        /// </summary>
        public CommunicationType CommunicationType { get; set; }

        /// <summary>
        /// Titular del lote de facturas recibidas.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de facturas recibidas incluidas en el lote.
        /// </summary>
        public List<APInvoice> APInvoices { get; private set; }

        /// <summary>
        /// Constructor clase APInvoicesBatch.
        /// </summary>
        public APInvoicesBatch()
        {
            APInvoices = new List<APInvoice>();
        }

        /// <summary>
        /// Constructor clase APInvoicesBatch.
        /// </summary>
        /// <param name="suministroLRFacturasRecibidas">Objeto de serialización xml para
        /// suministro de facturas recibidas.</param>
        public APInvoicesBatch(SuministroLRFacturasRecibidas suministroLRFacturasRecibidas)
        {

            APInvoices = new List<APInvoice>();

            CommunicationType communicationType;

            if (!Enum.TryParse<CommunicationType>(
                suministroLRFacturasRecibidas.Cabecera.TipoComunicacion, out communicationType))
                throw new InvalidOperationException($"Unknown comunication type {suministroLRFacturasRecibidas.Cabecera.TipoComunicacion}");

            CommunicationType = communicationType;

            Titular = new Party()
            {
                TaxIdentificationNumber = suministroLRFacturasRecibidas.Cabecera.Titular.NIF,
                PartyName = suministroLRFacturasRecibidas.Cabecera.Titular.NombreRazon
            };

            foreach (var invoice in
                suministroLRFacturasRecibidas.RegistroLRFacturasRecibidas)
            {
                APInvoice apInvoice = new APInvoice(invoice);
                apInvoice.BuyerParty = Titular;
                APInvoices.Add(apInvoice);
            }

        }


		/// <summary>
		/// Devuelve el sobre soap del lote de facturas emitidas.
		/// </summary>
		/// <param name="skipErrors">Indica si hay que omitir las excepciones.</param>
		/// <returns>Sobre SOAP con lote de facturas recibidas.</returns>
		public Envelope GetEnvelope(bool skipErrors = false)
        {
            Envelope envelope = new Envelope();

            envelope.Body.SuministroLRFacturasRecibidas = new SuministroLRFacturasRecibidas();

            envelope.Body.SuministroLRFacturasRecibidas.Cabecera.TipoComunicacion = CommunicationType.ToString();

            envelope.Body.SuministroLRFacturasRecibidas.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.SuministroLRFacturasRecibidas.Cabecera.Titular.NombreRazon = Titular.PartyName;

            foreach(APInvoice invoice in APInvoices)
                envelope.Body.SuministroLRFacturasRecibidas.RegistroLRFacturasRecibidas.Add(invoice.ToSII(false, skipErrors));

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

            return GetFileName("LRFR.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de respuesta recibido para una instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("LRFR.RECEIVED.{0}.{1}.{2}.xml");

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

            string template = "LRFR.SENT.{0}.{1}.{2}.xml";

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

            string template = "LRFR.RECEIVED.{0}.{1}.{2}.xml";

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

            return GetName(template, APInvoices[0].InvoiceNumber,
                APInvoices[APInvoices.Count - 1].InvoiceNumber,
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
