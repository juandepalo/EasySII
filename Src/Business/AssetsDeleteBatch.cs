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
    /// Lote de Bienes de Inversión a eliminar (Activos - Assets).
    /// </summary>
    public class AssetsDeleteBatch
    {

 
        /// <summary>
        /// Titular del lote de Bienes de Inversión.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de Bienes de Inversión incluidos en el lote.
        /// </summary>
        public List<Asset> Assets { get; private set; }

        /// <summary>
        /// Constructor clase IPInvoicesBatch.
        /// </summary>
        public AssetsDeleteBatch()
        {
            Assets = new List<Asset>();
        }

        /// <summary>
        /// Devuelve el sobre soap del lote de Bienes de Inversión
        /// </summary>
        /// <returns>Devuelve un string con el xml del sobre SOAP
        /// compuesto para el envío del mensaje de lote de Bienes de Inversión.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.BajaLRBienesInversion = new BajaLRBienesInversion();

            envelope.Body.BajaLRBienesInversion.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.BajaLRBienesInversion.Cabecera.Titular.NombreRazon = Titular.PartyName;

            foreach (Asset invoice in Assets)

                envelope.Body.BajaLRBienesInversion.RegistroLRBienesInversion.Add(invoice.ToRegistroLRBajaBienesInversionSII());

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de Bienes de Inversión como un archivo xml para soap según las
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
        /// determinda de lote de Bienes de Inversión.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión.</returns>
        public string GetSentFileName()
        {

            return GetFileName("DRBI.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de respuesta recibido para una instancia
        /// determinda de lote de Bienes de Inversión.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("DRBI.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión.</returns>
        public static string GetNameSent(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "DRBI.SENT.{0}.{1}.{2}.xml";

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
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión.</returns>
        public static string GetNameReceived(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "DRBI.RECEIVED.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión.</returns>
        private string GetFileName(string template)
        {

            return GetName(template, Assets[0].InvoiceNumber,
               Assets[Assets.Count - 1].InvoiceNumber,
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
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión.</returns>
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
