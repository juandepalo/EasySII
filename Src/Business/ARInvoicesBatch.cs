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
    public class ARInvoicesBatch
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
        public List<ARInvoice> ARInvoices { get; private set; }

        /// <summary>
        /// Constructor clase ARInvoicesBatch.
        /// </summary>
        public ARInvoicesBatch()
        {
            ARInvoices = new List<ARInvoice>();
        }

        /// <summary>
        /// Constructor clase ARInvoicesBatch.
        /// </summary>
        /// <param name="suministroLRFacturasEmitidas">Objeto de serialización xml para
        /// suministro de facturas emitidas.</param>
        public ARInvoicesBatch(SuministroLRFacturasEmitidas suministroLRFacturasEmitidas)
        {
            ARInvoices = new List<ARInvoice>();

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
                ARInvoices.Add(new ARInvoice(invoice));

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

            foreach(ARInvoice invoice in ARInvoices)
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
