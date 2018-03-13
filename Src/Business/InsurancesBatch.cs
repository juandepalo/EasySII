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
    /// Lote de Operaciones Seguros.
    /// </summary>
    public class InsurancesBatch
    {
        /// <summary>
        /// Tipo de comunicación.
        /// </summary>
        public CommunicationType CommunicationType { get; set; }

        /// <summary>
        /// Titular del lote de operaciones seguros.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de operaciones seguros incluidas en el lote.
        /// </summary>
        public List<OPTributaria> Insurances { get; private set; }

        /// <summary>
        /// Constructor clase InsurancesBatch.
        /// </summary>
        public InsurancesBatch()
        {
            Insurances = new List<OPTributaria>();
        }

        /// <summary>
        /// Constructor clase InsurancesBatch.
        /// </summary>
        /// <param name="suministroLROperacionesSeguros">Objeto de serialización xml para
        /// suministro de Operaciones Seguros.</param>
        public InsurancesBatch(SuministroLROperacionesSeguros suministroLROperacionesSeguros)
        {
            Insurances = new List<OPTributaria>();

            CommunicationType communicationType;

            if (!Enum.TryParse<CommunicationType>(
                suministroLROperacionesSeguros.Cabecera.TipoComunicacion, out communicationType))
                throw new InvalidOperationException($"Unknown comunication type {suministroLROperacionesSeguros.Cabecera.TipoComunicacion}");

            CommunicationType = communicationType;

            Titular = new Party()
            {
                TaxIdentificationNumber = suministroLROperacionesSeguros.Cabecera.Titular.NIF,
                PartyName = suministroLROperacionesSeguros.Cabecera.Titular.NombreRazon
            };

            foreach (var invoice in suministroLROperacionesSeguros.RegistroLROperacionesSeguros)
                Insurances.Add(new OPTributaria(invoice));

        }

        /// <summary>
        /// Devuelve el sobre soap del lote de Operaciones Seguros.
        /// </summary>
        /// <returns>Devuelve un string con el xml del sobre SOAP
        /// compuesto para el envío del mensaje de lote de operaciones de seguros.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.SuministroLROperacionesSeguros = new SuministroLROperacionesSeguros();

            envelope.Body.SuministroLROperacionesSeguros.Cabecera.TipoComunicacion = CommunicationType.ToString();

            envelope.Body.SuministroLROperacionesSeguros.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.SuministroLROperacionesSeguros.Cabecera.Titular.NombreRazon = Titular.PartyName;

            foreach (OPTributaria invoice in Insurances)
            {
                envelope.Body.SuministroLROperacionesSeguros.RegistroLROperacionesSeguros.Add(invoice.ToSII());
            }

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de operaciones seguros como un archivo xml para soap según las
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
        /// determinda de lote de operaciones de seguros.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        public string GetSentFileName()
        {

            return GetFileName("LROS.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de operaciones de seguros.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("LROS.RECEIVED.{0}.{1}.{2}.xml");

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

            string template = "LROS.SENT.{0}.{1}.{2}.xml";

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

            string template = "LROS.RECEIVED.{0}.{1}.{2}.xml";

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
