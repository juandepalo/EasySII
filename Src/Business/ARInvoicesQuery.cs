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
    /// Consulta de facturas expedidas enviadas.
    /// </summary>
    public class ARInvoicesQuery
    {

 
        /// <summary>
        /// Titular del lote de facturas expedidas.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Factura emitida para recoger información de filtro.
        /// </summary>
        public ARInvoice ARInvoice { get; set; }

        /// <summary>
        /// Constructor clase ARInvoicesQuery.
        /// </summary>
        public ARInvoicesQuery()
        {
            ARInvoice = new ARInvoice();
        }

        /// <summary>
        /// Devuelve el sobre soap de consulta de facturas emitidas.
        /// </summary>
        /// <returns> El sobre soap de consulta de facturas emitidas.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.ConsultaLRFacturasEmitidas = new ConsultaLRFacturasEmitidas();            

            envelope.Body.ConsultaLRFacturasEmitidas.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.ConsultaLRFacturasEmitidas.Cabecera.Titular.NombreRazon = Titular.PartyName;

            envelope.Body.ConsultaLRFacturasEmitidas.FiltroConsulta = ARInvoice.ToFilterSII();        

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
            return SIIParser.GetXml(GetEnvelope(), xmlPath, SIINamespaces.con, SIINamespaces.sum);
        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de facturas emitidas.</returns>
        public string GetSentFileName()
        {

            return GetFileName("QRFE.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de respuesta recibido para una instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("QRFE.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas emitidas.</returns>
        private string GetFileName(string template)
        {

            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                (ARInvoice.IssueDate??new DateTime(1,1,1)).ToString("yyyyMMdd_hhmmss"))).Replace("-", "");

            numLast = "";

            return string.Format(template, Titular.TaxIdentificationNumber, numFirst, numLast);

        }
    }
}
