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
    /// Consulta de Cobros en Metálico.
    /// </summary>
    public class CashQuery
    {
 

        /// <summary>
        /// Titular del lote de Cobros en Metálico.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Cobro en metálico para recoger información de filtro.
        /// </summary>
        public OPTributaria CashReceipts { get; private set; }

        /// <summary>
        /// Constructor clase CashQuery.
        /// </summary>
        public CashQuery()
        {
            CashReceipts = new OPTributaria();
        }

        /// <summary>
        /// Devuelve el sobre soap de consulta de Operaciones Seguros.
        /// </summary>
        /// <returns> El sobre soap de consulta de Operaciones Seguros.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.ConsultaLRCobrosMetalico = new ConsultaLRCobrosMetalico();

            envelope.Body.ConsultaLRCobrosMetalico.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.ConsultaLRCobrosMetalico.Cabecera.Titular.NombreRazon = Titular.PartyName;

            envelope.Body.ConsultaLRCobrosMetalico.FiltroConsulta = CashReceipts.ToFilterSII();        

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de Cobros en Metálico como un archivo xml para soap según las
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
        /// determinda de lote de Cobros en Metálico.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Cobros en Metálico.</returns>
        public string GetSentFileName()
        {

            return GetFileName("QRCM.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de Cobros en Metálico.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Cobros en Metálico.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("QRCM.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Cobros en Metálico.</returns>
        private string GetFileName(string template)
        {

            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                (CashReceipts.IssueDate??new DateTime(1,1,1)).ToString("yyyyMMdd_hhmmss"))).Replace("-", "");

            numLast = "";

            return string.Format(template, Titular.TaxIdentificationNumber, numFirst, numLast);

        }
    }
}
