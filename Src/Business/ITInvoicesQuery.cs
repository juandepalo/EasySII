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
    /// Consulta de facturas Operaciones Intracomunitarias.
    /// </summary>
    public class ITInvoicesQuery
    {
 

        /// <summary>
        /// Titular del lote de Operaciones Intracomunitarias.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Operación Intracomunitaria para recoger información de filtro.
        /// </summary>
        public ITInvoice ITInvoice { get; set; }

        /// <summary>
        /// Constructor clase ITInvoicesQuery.
        /// </summary>
        public ITInvoicesQuery()
        {
            ITInvoice = new ITInvoice();
        }

        /// <summary>
        /// Devuelve el sobre soap de consulta de Operaciones Intracomunitarias.
        /// </summary>
        /// <returns> El sobre soap de consulta de Operaciones Intracomunitarias.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.ConsultaLRDetOperIntracomunitarias = new ConsultaLRDetOperIntracomunitarias();

            envelope.Body.ConsultaLRDetOperIntracomunitarias.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.ConsultaLRDetOperIntracomunitarias.Cabecera.Titular.NombreRazon = Titular.PartyName;

            envelope.Body.ConsultaLRDetOperIntracomunitarias.FiltroConsulta = ITInvoice.ToFilterSII();        

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de operaciones intracomunitarias como un archivo xml para soap según las
        /// especificaciones de la aeat.
        /// </summary>
        /// <param name="xmlPath">Ruta donde se guardará el archivo generado.</param>
        /// <returns>Xaml generado.</returns>
        public XmlDocument GetXml(string xmlPath)
        {
            return SIIParser.GetXml(GetEnvelope(), xmlPath, SIINamespaces.con);
        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de operaciones intracomunitarias.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de operaciones intracomunitarias.</returns>
        public string GetSentFileName()
        {

            return GetFileName("QROI.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de operaciones intracomunitarias.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de operaciones intracomunitarias.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("QROI.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de operaciones intracomunitarias.</returns>
        private string GetFileName(string template)
        {

            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                (ITInvoice.IssueDate??new DateTime(1,1,1)).ToString("yyyyMMdd_hhmmss"))).Replace("-", "");

            numLast = "";

            return string.Format(template, Titular.TaxIdentificationNumber, numFirst, numLast);

        }
    }
}
