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
    /// Consulta de pagos de facturas recibidas.
    /// envíados.
    /// </summary>
    public class APPaymentsQuery
    {
 

        /// <summary>
        /// Titular del lote de facturas recibidas.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Factura recibida para recoger información de filtro.
        /// </summary>
        public APInvoice APInvoice { get; set; }

        /// <summary>
        /// Constructor clase ARPaymentsQuery.
        /// </summary>
        public APPaymentsQuery()
        {
            APInvoice = new APInvoice();
        }

        /// <summary>
        /// Devuelve el sobre soap de consulta de facturas recibidas
        /// </summary>
        /// <returns> El sobre soap de consulta de facturas recibidas.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.ConsultaPagos = new ConsultaPagos();            

            envelope.Body.ConsultaPagos.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.ConsultaPagos.Cabecera.Titular.NombreRazon = Titular.PartyName;

            envelope.Body.ConsultaPagos.FiltroConsultaPagos = APInvoice.ToFilterPagosSII();        

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
            return SIIParser.GetXml(GetEnvelope(), xmlPath, SIINamespaces.con);
        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII 
        /// del lote de facturas recibidas.</returns>
        public string GetSentFileName()
        {

            return GetFileName("QCFR.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de respuesta recibido para una instancia
        /// determinda de lote de facturas.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas recibidas.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("QCFR.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de respuesta del SII 
        /// del lote de facturas recibidas.</returns>
        private string GetFileName(string template)
        {

            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                (APInvoice.IssueDate??new DateTime(1,1,1)).ToString("yyyyMMdd_hhmmss"))).Replace("-", "");

            numLast = "";

            return string.Format(template, Titular.TaxIdentificationNumber, numFirst, numLast);

        }
    }
}
