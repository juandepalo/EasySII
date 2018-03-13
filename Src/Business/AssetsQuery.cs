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
    /// Consulta de Bienes de Inversión enviados (Activos - Assets).
    /// </summary>
    public class AssetsQuery
    {
 

        /// <summary>
        /// Titular del lote de Bienes de Inversión.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Factura de Bienes de Inversión para recoger información de filtro.
        /// </summary>
        public Asset Assets { get; private set; }

        /// <summary>
        /// Constructor clase IPInvoicesBatch.
        /// </summary>
        public AssetsQuery()
        {
            Assets = new Asset();
        }

        /// <summary>
        /// Devuelve el sobre soap de consulta de Bienes de Inversión.
        /// </summary>
        /// <returns> El sobre soap de consulta de Bienes de Inversión.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.ConsultaLRBienesInversion = new ConsultaLRBienesInversion();            

            envelope.Body.ConsultaLRBienesInversion.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.ConsultaLRFacturasRecibidas.Cabecera.Titular.NombreRazon = Titular.PartyName;

            envelope.Body.ConsultaLRBienesInversion.FiltroConsulta = Assets.ToFilterSII();        

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
            return SIIParser.GetXml(GetEnvelope(), xmlPath, SIINamespaces.con);
        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de Bienes de Inversión.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión.</returns>
        public string GetSentFileName()
        {

            return GetFileName("QRBI.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de Bienes de Inversión.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("QRBI.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión.</returns>
        private string GetFileName(string template)
        {

            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                (Assets.IssueDate??new DateTime(1,1,1)).ToString("yyyyMMdd_hhmmss"))).Replace("-", "");

            numLast = "";

            return string.Format(template, Titular.TaxIdentificationNumber, numFirst, numLast);

        }
    }
}
