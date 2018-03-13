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
    /// Lote de Operacions Seguros a eliminar.
    /// </summary>
    public class InsurancesDeleteBatch
    {

 
        /// <summary>
        /// Titular del lote de Operaciones Seguros.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de Operaciones de seguros incluidas en el lote.
        /// </summary>
        public List<OPTributaria> Insurances { get; private set; }

        /// <summary>
        /// Constructor clase IPInvoicesBatch.
        /// </summary>
        public InsurancesDeleteBatch()
        {
            Insurances = new List<OPTributaria>();
        }

        /// <summary>
        /// Devuelve el sobre soap del lote de Operaciones de Seguros
        /// </summary>
        /// <returns>Devuelve un string con el xml del sobre SOAP
        /// compuesto para el envío del mensaje de lote de Operaciones de Seguros.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.BajaLROperacionesSeguros = new BajaLROperacionesSeguros();

            envelope.Body.BajaLROperacionesSeguros.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.BajaLROperacionesSeguros.Cabecera.Titular.NombreRazon = Titular.PartyName;

            foreach (OPTributaria invoice in Insurances)

                envelope.Body.BajaLROperacionesSeguros.RegistroLROperacionesSeguros.Add(invoice.ToRegistroLRBajaOpTrascendTribuSII());

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de Operaciones de Seguros como un archivo xml para soap según las
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
        /// determinda de lote de Operaciones Seguros.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        public string GetSentFileName()
        {

            return GetFileName("DROS.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de Operaciones Seguros.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("DROS.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        public static string GetNameSent(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "DROS.SENT.{0}.{1}.{2}.xml";

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
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        public static string GetNameReceived(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "DROS.RECEIVED.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        private string GetFileName(string template)
        {

            return GetName(template, Insurances[0].IssueDate.ToString(),
               Insurances[Insurances.Count - 1].IssueDate.ToString(),
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
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
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
