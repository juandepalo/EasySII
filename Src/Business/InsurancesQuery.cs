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
    /// Consulta de Operaciones Seguros.
    /// </summary>
    public class InsurancesQuery
    {
 

        /// <summary>
        /// Titular del lote de Operaciones Seguros.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Operación Seguro para recoger información de filtro.
        /// </summary>
        public OPTributaria Insurance { get; private set; }

        /// <summary>
        /// Constructor clase InsurancesQuery.
        /// </summary>
        public InsurancesQuery()
        {
            Insurance = new OPTributaria();
        }

        /// <summary>
        /// Devuelve el sobre soap de consulta de Operaciones Seguros.
        /// </summary>
        /// <returns> El sobre soap de consulta de Operaciones Seguros.</returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.ConsultaLROperacionesSeguros = new ConsultaLROperacionesSeguros();

            envelope.Body.ConsultaLROperacionesSeguros.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.ConsultaLROperacionesSeguros.Cabecera.Titular.NombreRazon = Titular.PartyName;

            envelope.Body.ConsultaLROperacionesSeguros.FiltroConsulta = Insurance.ToFilterSII();        

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de Operaciones Seguros como un archivo xml para soap según las
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
        /// determinda de lote de Operaciones Seguros.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        public string GetSentFileName()
        {

            return GetFileName("QROS.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de Operaciones Seguros.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("QROS.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Operaciones Seguros.</returns>
        private string GetFileName(string template)
        {

            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                (Insurance.IssueDate??new DateTime(1,1,1)).ToString("yyyyMMdd_hhmmss"))).Replace("-", "");

            numLast = "";

            return string.Format(template, Titular.TaxIdentificationNumber, numFirst, numLast);

        }
    }
}
