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
    /// Lote de Cobros en metálico a eliminar.
    /// </summary>
    public class CashDeleteBatch
    {

 
        /// <summary>
        /// Titular del lote de Operaciones Seguros.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de cobros en metálico incluidos en el lote.
        /// </summary>
        public List<OPTributaria> CashReceipts { get; private set; }

        /// <summary>
        /// Constructor clase CashDeleteBatch.
        /// </summary>
        public CashDeleteBatch()
        {
            CashReceipts = new List<OPTributaria>();
        }

        /// <summary>
        /// Devuelve el sobre soap del lote de Cobros en metálico.
        /// </summary>
        /// <returns>Devuelve un string con el xml del sobre SOAP
        /// compuesto para el envío del mensaje de lote de Cobros en metálico.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.BajaLRCobrosMetalico = new BajaLRCobrosMetalico();

            envelope.Body.BajaLRCobrosMetalico.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.BajaLRCobrosMetalico.Cabecera.Titular.NombreRazon = Titular.PartyName;

            foreach (OPTributaria invoice in CashReceipts)

                envelope.Body.BajaLRCobrosMetalico.RegistroLRCobrosMetalico.Add(invoice.ToRegistroLRBajaOpTrascendTribuSII());

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
        /// determinda de lote de Cobros en Metálico.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Cobros en Metálico.</returns>
        public string GetSentFileName()
        {

            return GetFileName("DRCM.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de Cobros en Metálico.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Cobros en Metálico.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("DRCM.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>
        /// <returns>Nombre del archivo de envío al SII del lote de Cobros en Metálico.</returns>
        public static string GetNameSent(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "DRCM.SENT.{0}.{1}.{2}.xml";

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
        /// <returns>Nombre del archivo de envío al SII del lote de Cobros en Metálico.</returns>
        public static string GetNameReceived(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "DRCM.RECEIVED.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Cobros en Metálico.</returns>
        private string GetFileName(string template)
        {

            return GetName(template, CashReceipts[0].IssueDate.ToString(),
               CashReceipts[CashReceipts.Count - 1].IssueDate.ToString(),
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
        /// <returns>Nombre del archivo de envío al SII del lote de Cobros en Metálico.</returns>
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
