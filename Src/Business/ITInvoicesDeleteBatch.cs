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
    /// Lote de operaciones intracomunitarias a eliminar.
    /// </summary>
    public class ITInvoicesDeleteBatch
    {

 
        /// <summary>
        /// Titular del lote de Operaciones Intracomunitarias.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de operaciones intracomunitarias incluidas en el lote.
        /// </summary>
        public List<ITInvoice> ITInvoices { get; private set; }

        /// <summary>
        /// Constructor clase ITInvoicesDeleteBatch.
        /// </summary>
        public ITInvoicesDeleteBatch()
        {
            ITInvoices = new List<ITInvoice>();
        }

        /// <summary>
        /// Devuelve el sobre soap del lote de operaciones intracomunitarias.
        /// </summary>
        /// <returns>Devuelve un string con el xml del sobre SOAP
        /// compuesto para el envío del mensaje de lote de operaciones intracomunitarias.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.BajaLRDetOperacionIntracomunitaria = new BajaLRDetOperacionIntracomunitaria();

            envelope.Body.BajaLRDetOperacionIntracomunitaria.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.BajaLRDetOperacionIntracomunitaria.Cabecera.Titular.NombreRazon = Titular.PartyName;

            foreach(ITInvoice invoice in ITInvoices)
                envelope.Body.BajaLRDetOperacionIntracomunitaria.RegistroLRBajaDetOperacionIntracomunitaria.Add(
                    invoice.ToRegistroLRBajaOperIntracomSII());

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
        /// determinda de lote de operaciones intracomunitarias.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de operaciones intracomunitarias.</returns>
        public string GetSentFileName()
        {

            return GetFileName("DROI.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de operaciones intracomunitarias.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de operaciones intracomunitarias.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("DROI.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de operaciones intracomunitarias.</returns>
        public static string GetNameSent(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "DROI.SENT.{0}.{1}.{2}.xml";

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
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de operaciones intracomunitarias.</returns>
        public static string GetNameReceived(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "DROI.RECEIVED.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de operaciones intracomunitarias.</returns>
        private string GetFileName(string template)
        {

            return GetName(template, ITInvoices[0].InvoiceNumber,
               ITInvoices[ITInvoices.Count - 1].InvoiceNumber,
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
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de operaciones intracomunitarias.</returns>
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
